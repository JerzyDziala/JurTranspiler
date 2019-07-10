using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class SyntaxTree : ISyntaxNode {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		//children
		public ImmutableList<ProgramFileSyntax> Files { get; }


		public SyntaxTree(List<(JurParser.ProgramContext context, string fileName)> files) {
			Root = this;
			Parent = null;
			Abstraction = -1;
			File = "";
			Line = -1;

			Files = files.Select(x => new ProgramFileSyntax(this, x.fileName, x.context)).ToImmutableList();

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(Files);

			AllChildren = this.GetAllChildren();

		}


		public SyntaxTree(JurParser.ProgramContext context) {
			Root = this;
			Parent = null;
			Abstraction = -1;
			File = "";
			Line = -1;

			Files = ImmutableList.Create<ProgramFileSyntax>()
			                     .Add(new ProgramFileSyntax(this, "__TEST__", context));

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(Files);
			AllChildren = this.GetAllChildren();

		}


		public string ToJs(Binder binder) {
			var std = @"
let cachedTypes$ = {
    func: {
        name: 'function'
    },
    string: {
        name: 'string'
    },
    bool: {
        name: 'bool'
    },
    num: {
        name: 'num'
    }
};

function getCachedStructType$(name, fieldsSupplier) {

    if (cachedTypes$.hasOwnProperty(name)) {
        return cachedTypes$[name];
    }

    let type = {
        name: name,
    };

    cachedTypes$[name] = type;

    type['fields'] = fieldsSupplier();
    return type;
}

function getCachedArrayType(elementType) {
    let name = elementType.name + '[]';

    if (cachedTypes$.hasOwnProperty(name)) {
        return cachedTypes$[name];
    }

    let type = {
        name: name,
        elementType: elementType
    };

    cachedTypes$[name] = type;
    return type;
}

function Array$(elementType) {
    let array = [];
    array.type$ = getCachedArrayType(elementType);
    return array;
}

function getDefaultValueOfType(type) {
    if (type.name === 'function') return undefined;
    if (type.name === 'string') return '';
    if (type.name === 'bool') return false;
    if (type.name === 'num') return 0;
    return null;
}

function getType$(value) {
    if (typeof value === 'string'
        || typeof value === 'boolean'
        || typeof value === 'number') return cachedTypes$[value];
	else if(typeof value === 'function') return cachedTypes$['func'];
    else return value.type$;
}

main$();
";
			return $"{Files.Select(x => x.ToJs(binder)).Glue("\n\n")}\n\n\n //---------STD---------//\n{std}";
		}

	}

}
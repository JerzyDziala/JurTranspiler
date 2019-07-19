using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
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

        public ImmutableArray<StructDefinitionSyntax> AllStructDefinitions { get; }
        public ImmutableArray<FunctionDefinitionSyntax> AllFunctionDefinitions { get; }
        public ImmutableArray<ITypeSyntax> AllTypeUsages { get; }
        public ImmutableArray<IExpressionSyntax> AllExpressions { get; }
        public ImmutableArray<IAssignment> AllAssignments { get; }
        public ImmutableArray<FunctionDefinitionSyntax> AllFunctions { get; }
        public ImmutableArray<MainSyntax> AllMains { get; }


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

            AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
            AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
            AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
            AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
            AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
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
            AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
            AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
            AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
            AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
            AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
        }


        public string ToJs(Knowledge knowledge) {
            return $"{Files.Select(x => x.ToJs(knowledge)).Glue("\n\n")}\n\nmain$();";
        }

    }

}
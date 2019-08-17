using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public sealed class SyntaxTree : SyntaxNode {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        //children
        public ImmutableArray<ProgramFileSyntax> Files { get; }

        public ImmutableArray<StructDefinitionSyntax> AllStructDefinitions { get; }
        public ImmutableArray<FunctionDefinitionSyntax> AllFunctionDefinitions { get; }
        public ImmutableArray<ITypeSyntax> AllTypeUsages { get; }
        public ImmutableArray<IExpressionSyntax> AllExpressions { get; }
        public ImmutableArray<IAssignment> AllAssignments { get; }
        public ImmutableArray<FunctionDefinitionSyntax> AllFunctions { get; }
        public ImmutableArray<MainSyntax> AllMains { get; }


        public SyntaxTree(List<(JurParser.ProgramContext context, string fileName)> files) : base() {

            Files = files.Select(x => new ProgramFileSyntax(this, x.fileName, x.context)).ToImmutableArray();

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .AddRange(Files);
            AllChildren = GetAllChildren();

            //TODO: optimize
            AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
            AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
            AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
            AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
            AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
        }


        public SyntaxTree(JurParser.ProgramContext context) : base() {

            Files = ImmutableArray.Create<ProgramFileSyntax>()
                                  .Add(new ProgramFileSyntax(this, "__TEST__", context));

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .AddRange(Files);
            AllChildren = GetAllChildren();

            //TODO: optimize
            AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
            AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
            AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
            AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
            AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
            AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
        }


        public override string ToJs(Knowledge knowledge) {
            return $"{Files.Select(x => x.ToJs(knowledge)).Glue("\n\n")}\n\nmain$();";
        }

    }

}
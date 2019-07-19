using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

    public class PrimitiveValueSyntax : IExpressionSyntax {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public string Value { get; }
        public bool IsNull { get; }


        public PrimitiveValueSyntax(ISyntaxNode parent, JurParser.PrimitiveValueContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Value = context.value.Text;
            IsNull = Value == "null";
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return Value;
        }

    }

}
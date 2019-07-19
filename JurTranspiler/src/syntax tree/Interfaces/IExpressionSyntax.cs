
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

    public abstract class IExpressionSyntax : ISyntaxNode {

        public abstract int Abstraction { get; }
        public abstract string File { get; }
        public abstract int Line { get; }
        public abstract ISyntaxNode Root { get; }
        public abstract ISyntaxNode Parent { get; }
        public abstract ImmutableList<ISyntaxNode> AllParents { get; }
        public abstract ImmutableList<ITreeNode> ImmediateChildren { get; }
        public abstract ImmutableList<ITreeNode> AllChildren { get; }

        public abstract string ToJs(Knowledge knowledge);
    }

}
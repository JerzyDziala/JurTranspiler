using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.src.syntax_tree.types {

    public class StructTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<StructTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public string Name { get; }
        public ImmutableArray<ITypeSyntax> TypeArguments { get; }
        public int Arity => TypeArguments.Length;
        public bool IsGeneric => Arity > 0;
        public string FullName => IsGeneric ? Name + $"<{string.Join(",", TypeArguments.Select(x => x.FullName))}>" : Name;


        public StructTypeSyntax(ISyntaxNode parent, JurParser.TypeParameterOrStructTypeContext context) : base(parent, context) {

            Name = context.ID().GetText();
            TypeArguments = ImmutableArray.Create<ITypeSyntax>();
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();

        }


        public StructTypeSyntax(ISyntaxNode parent, JurParser.GenericStructTypeContext context) : base(parent, context) {
            Name = context.ID().GetText();
            TypeArguments = ToTypes(context.type());
            ImmediateChildren = ImmutableArray.Create<ITreeNode>().AddRange(TypeArguments);

        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(StructTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(FullName, other.FullName)
                && TypeArguments.SequenceEqual(other.TypeArguments);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((StructTypeSyntax) obj!);
        }


        public override int GetHashCode() {
            var argumentsHash = TypeArguments.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
            return (Name != null ? Name.GetHashCode() : 0) ^ argumentsHash;
        }

    }

}
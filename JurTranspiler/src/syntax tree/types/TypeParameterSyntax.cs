using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.src.syntax_tree.types {

    public class TypeParameterSyntax : SyntaxNode, ITypeSyntax, IEquatable<TypeParameterSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public string Name { get; }
        public string FullName => Name;
        public IStructOrFunctionDeclarationSyntax OriginalDeclarer { get; }


        public TypeParameterSyntax(ISyntaxNode parent, JurParser.TypeParameterOrStructTypeContext context, IStructOrFunctionDeclarationSyntax originalDeclarer) : base(parent, context) {
            OriginalDeclarer = originalDeclarer;
            Name = context.ID().GetText();
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public TypeParameterSyntax(ISyntaxNode parent,
                                   string name,
                                   int line,
                                   IStructOrFunctionDeclarationSyntax originalDeclarer) : base(parent, line) {
            OriginalDeclarer = originalDeclarer;
            Name = name;
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) => Name;


        public bool Equals(TypeParameterSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(OriginalDeclarer, other.OriginalDeclarer);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeParameterSyntax) obj);
        }


        public override int GetHashCode() {
            unchecked {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (OriginalDeclarer != null ? OriginalDeclarer.GetHashCode() : 0);
            }
        }
    }

}
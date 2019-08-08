using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.src.syntax_tree.types {

    public class TypeParameterSyntax : ITypeSyntax, IEquatable<TypeParameterSyntax> {

        public virtual ISyntaxNode Root { get; }
        public virtual ISyntaxNode Parent { get; }
        public virtual ImmutableList<ISyntaxNode> AllParents { get; }
        public virtual ImmutableList<ITreeNode> ImmediateChildren { get; }
        public virtual ImmutableList<ITreeNode> AllChildren { get; }
        public virtual string File { get; }
        public virtual int Line { get; }
        public virtual int Abstraction { get; }
        public virtual string Name { get; }

        public string FullName => Name;
        public string DefaultValue => $"getDefaultValueOfType(substitutions$['{Name}'])";
        public IStructOrFunctionDeclarationSyntax OriginalDeclarer { get; }


        public TypeParameterSyntax(ISyntaxNode parent, JurParser.TypeParameterOrStructTypeContext context, IStructOrFunctionDeclarationSyntax originalDeclarer) {
            Parent = parent;
            OriginalDeclarer = originalDeclarer;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Name = context.ID().GetText();

            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();

        }


        public TypeParameterSyntax(ISyntaxNode parent,
                                   string name,
                                   int line,
                                   IStructOrFunctionDeclarationSyntax originalDeclarer) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = line;
            OriginalDeclarer = originalDeclarer;

            Name = name;

            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();

        }


        public string ToJs(Knowledge knowledge) => Name;


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
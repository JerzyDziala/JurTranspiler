using System;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using System.Xml;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.src.syntax_tree.types {

    public class StructTypeSyntax : ITypeSyntax, IEquatable<StructTypeSyntax> {

        public virtual ISyntaxNode Root { get; }
        public virtual ISyntaxNode Parent { get; }
        public virtual ImmutableList<ISyntaxNode> AllParents { get; }
        public virtual ImmutableList<ITreeNode> ImmediateChildren { get; }
        public virtual ImmutableList<ITreeNode> AllChildren { get; }
        public virtual string File { get; }
        public virtual int Line { get; }
        public virtual int Abstraction { get; }
        public virtual string Name { get; }

        public ImmutableList<ITypeSyntax> TypeArguments { get; }
        public int Arity { get; }
        public string FullName { get; }
        public string DefaultValue => "null";


        public StructTypeSyntax(ISyntaxNode parent, JurParser.TypeParameterOrStructTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Name = context.ID().GetText();
            TypeArguments = ImmutableList.Create<ITypeSyntax>();
            Arity = 0;
            FullName = Name;
            if (Arity > 0) {
                FullName += $"<{string.Join(",", TypeArguments.Select(x => x.FullName))}>";
            }
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();

        }

        public StructTypeSyntax(ISyntaxNode parent, JurParser.GenericStructTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            Name = context.ID().GetText();
            TypeArguments = context.type().Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableList();
            Arity = TypeArguments.Count;
            FullName = Name;
            if (Arity > 0) {
                FullName += $"<{string.Join(",", TypeArguments.Select(x => x.FullName))}>";
            }
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddRange(TypeArguments);
            AllChildren = this.GetAllChildren();

        }

        public string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }

        public bool Equals(StructTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StructTypeSyntax) obj);
        }


        public override int GetHashCode() {
            var argumentsHash = TypeArguments.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
            return (Name != null ? Name.GetHashCode() : 0) ^ argumentsHash;
        }

    }

}
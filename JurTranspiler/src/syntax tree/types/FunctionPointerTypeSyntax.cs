using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;

namespace JurTranspiler.src.syntax_tree.types {

    public class FunctionPointerTypeSyntax : ITypeSyntax, IEquatable<FunctionPointerTypeSyntax> {
        public virtual ISyntaxNode Root { get; }
        public virtual ISyntaxNode Parent { get; }
        public virtual ImmutableList<ISyntaxNode> AllParents { get; }
        public string ToJs(Binder binder) {
            throw new NotImplementedException();
        }


        public virtual ImmutableList<ITreeNode> ImmediateChildren { get; }
        public virtual ImmutableList<ITreeNode> AllChildren { get; }
        public virtual string File { get; }
        public virtual int Line { get; }
        public virtual int Abstraction { get; }
        public virtual string Name { get; }

        public ITypeSyntax ReturnType { get; }
        public ImmutableList<ITypeSyntax> Parameters { get; }
        public string FullName => Name;
        public string DefaultValue => "null";


        public FunctionPointerTypeSyntax(ISyntaxNode parent, JurParser.FunctionPointerTypeContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            if (context.VOID() != null) {
                ReturnType = new VoidTypeSyntax(this, Line);
                Parameters = context.type().Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableList();
            }
            else {
                ReturnType = TypeSyntaxFactory.Create(this, context.type(0));
                Parameters = context.type().Skip(1).Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableList();
            }

            Name = ReturnType.FullName + "(" + string.Join(",", Parameters.Select(x => x.FullName)) + ")";


            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(ReturnType)
                                             .AddRange(Parameters);
            AllChildren = this.GetAllChildren();
        }


        public bool Equals(FunctionPointerTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ReturnType, other.ReturnType);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FunctionPointerTypeSyntax) obj);
        }


        public override int GetHashCode() {
            unchecked {
                var parametersHash = Parameters.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0) ^ parametersHash;
            }
        }
    }

}
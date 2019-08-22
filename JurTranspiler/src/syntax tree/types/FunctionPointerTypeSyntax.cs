using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.src.syntax_tree.types {

    public class FunctionPointerTypeSyntax : SyntaxNode, ITypeSyntax, IEquatable<FunctionPointerTypeSyntax> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }


        public string Name => ReturnType.FullName + "(" + string.Join(",", Parameters.Select(x => x.FullName)) + ")";
        public ITypeSyntax ReturnType { get; }
        public ImmutableArray<ITypeSyntax> Parameters { get; }
        public string FullName => Name;


        public FunctionPointerTypeSyntax(ISyntaxNode parent, JurParser.FunctionPointerTypeContext context) : base(parent, context) {

            if (context.VOID() != null) {
                ReturnType = new VoidTypeSyntax(this, Line);
                Parameters = ToTypes(context.type());
            }
            else {
                ReturnType = ToType(context.type(0));
                Parameters = ToTypes(context.type().Skip(1).ToArray());
            }

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .Add(ReturnType)
                                              .AddRange(Parameters);

        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }


        public bool Equals(FunctionPointerTypeSyntax other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ReturnType, other.ReturnType);
        }


        public override bool Equals(object? obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj?.GetType() != GetType()) return false;
            return Equals((FunctionPointerTypeSyntax) obj!);
        }


        public override int GetHashCode() {
            unchecked {
                var parametersHash = Parameters.Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0) ^ parametersHash;
            }
        }
    }

}
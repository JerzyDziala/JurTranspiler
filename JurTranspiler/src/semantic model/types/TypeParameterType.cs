using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using JurTranspiler.compilerSource.nodes;
using Type = JurTranspiler.syntax_tree.bases.Type;

namespace JurTranspiler.compilerSource.semantic_model {

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class TypeParameterType : Type, IEquatable<TypeParameterType> {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }
        public override string Name { get; }

        public IStructOrFunctionDeclarationSyntax OriginalDeclarer { get; }
        private ImmutableArray<Lazy<TypeParameterType>> Constraints { get; }


        public TypeParameterType(string name,
                                 IStructOrFunctionDeclarationSyntax originalDeclarer,
                                 ImmutableArray<Lazy<TypeParameterType>> constraints) {
            Name = name;
            OriginalDeclarer = originalDeclarer;
            Constraints = constraints;
            ImmediateChildren = ImmutableArray.Create<ITreeNode>();
            AllChildren = GetAllChildren();
        }


        public ImmutableArray<TypeParameterType> GetAllConstraints() {
            var visited = new HashSet<TypeParameterType>();
            var allConstraints = new HashSet<TypeParameterType>();

            var queue = new Queue<TypeParameterType>();

            visited.Add(this);
            queue.Enqueue(this);

            while (queue.Any()) {
                var next = queue.Dequeue();
                allConstraints.Add(next);
                foreach (var constraint in next.Constraints) {
                    if (!visited.Contains(constraint.Value)) {
                        visited.Add(constraint.Value);
                        queue.Enqueue(constraint.Value);
                    }
                }
            }
            return allConstraints.ToImmutableArray();
        }


        public bool Equals(TypeParameterType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(OriginalDeclarer, other.OriginalDeclarer);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeParameterType) obj);
        }


        public override int GetHashCode() {
            unchecked {
                return (Name.GetHashCode() * 397) ^ OriginalDeclarer.GetHashCode();
            }
        }


        public override IType WithSubstitutedTypes(ISet<Substitution> typeMap) {
            return typeMap.First(substitution => substitution.typeParameter.Equals(this)).typeArgument;
        }

    }

}
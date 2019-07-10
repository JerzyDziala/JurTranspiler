using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.InteropServices.WindowsRuntime;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

    public class UndeclaredStructType : Type, IEquatable<UndeclaredStructType> {

        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }


        public override string Name { get; }


        public UndeclaredStructType(string name) {
            Name = name;
            ImmediateChildren = ImmutableList.Create<ITreeNode>();
            AllChildren = this.GetAllChildren();
        }


        protected override bool IsAssignableToCore(Type type, HashSet<Error> errors) {
            if (type is UndeclaredStructType undeclaredType
             && undeclaredType.Name == Name
             || type is UndefinedType) return true;
            return false;
        }


        public override bool IsAssignableToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
            if (type is UndeclaredStructType undeclaredType && undeclaredType.Name == Name) return true;
            if (type is UndefinedType) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, this));
                return true;
            }
            return false;
        }


        public override bool IsEqualToWithSubstitutions(Type type, ICollection<Substitution> substitutions, HashSet<Error> errors) {
            if (type is UndeclaredStructType undeclaredType && undeclaredType.Name == Name) return true;
            if (type is TypeParameterType t) {
                substitutions.Add(new Substitution(t, this));
                return true;
            }
            return false;
        }

        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) => this;

        public override string GetJsTypeCacheGetter() {
            throw new NotImplementedException();
        }

        public bool Equals(UndeclaredStructType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UndeclaredStructType) obj);
        }


        public override int GetHashCode() => (Name != null ? Name.GetHashCode() : 0);
    }

}
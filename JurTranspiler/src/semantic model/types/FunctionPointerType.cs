using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.semantic_model {

    public class FunctionPointerType : Type, IEquatable<FunctionPointerType> {

        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string Name { get; }

        public Type ReturnType { get; }
        public ImmutableList<Type> Parameters { get; }


        public FunctionPointerType(Type returnType, IEnumerable<Type> parameters) {
            ReturnType = returnType;
            Parameters = parameters.ToImmutableList();
            Name = ReturnType.Name + "(" + string.Join(",", Parameters.Select(x => x.Name)) + ")";
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(ReturnType)
                                             .AddRange(Parameters);
            AllChildren = this.GetAllChildren();
        }




        public override Type WithSubstitutedTypes(ISet<Substitution> typeMap) {
            return new FunctionPointerType(ReturnType.WithSubstitutedTypes(typeMap),
                                           Parameters.Select(x => x.WithSubstitutedTypes(typeMap)).ToImmutableList());
        }


        public bool Equals(FunctionPointerType other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Equals(ReturnType, other.ReturnType) && Parameters.SequenceEqual(other.Parameters);
        }


        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FunctionPointerType) obj);
        }


        public override int GetHashCode() {
            unchecked {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                foreach (var parameter in Parameters) {
                    hashCode = (hashCode * 397) ^ parameter.GetHashCode();
                }
                return hashCode;
            }
        }
    }

}
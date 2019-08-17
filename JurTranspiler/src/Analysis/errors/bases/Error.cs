using System;

namespace JurTranspiler.compilerSource.Analysis {

    public class Error : IEquatable<Error> {

        public virtual string GetMessage() => "";


        public virtual bool Equals(Error other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(GetMessage(), other.GetMessage());
        }


        public override string ToString() => GetMessage() + "\n";

        public override int GetHashCode() => GetMessage().GetHashCode();
    }

}
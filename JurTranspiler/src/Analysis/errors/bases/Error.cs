using System;

namespace JurTranspiler.Analysis.errors.bases {

	public abstract class Error : IEquatable<Error> {

		protected abstract string Localization { get; }
		protected abstract string MessageBody { get; }
		protected string Message => GetType().Name + " --- " + Localization + " --- " + MessageBody;


		public virtual bool Equals(Error other) {
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Message, other.Message);
		}


		public override string ToString() => Message + "\n";

		public override int GetHashCode() => Message.GetHashCode();

	}

}
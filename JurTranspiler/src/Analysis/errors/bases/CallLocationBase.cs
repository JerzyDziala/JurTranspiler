using System;
using JurTranspiler.syntax_tree.Interfaces;

namespace JurTranspiler.Analysis.errors.bases {

	public class Location : IEquatable<Location> {

		public string File { get; }
		public int Line { get; }


		public Location(string file, int line) {
			File = file;
			Line = line;
		}


		public Location(ISyntaxNode node) : this(node.File, node.Line) {

		}


		public bool Equals(Location other) => File == other.File && Line == other.Line;

		public override bool Equals(object? obj) => obj is Location other && Equals(other);


		public override int GetHashCode() {
			unchecked {
				return ((File != null ? File.GetHashCode() : 0) * 397) ^ Line;
			}
		}

	}

}
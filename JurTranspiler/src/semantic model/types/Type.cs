using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;

namespace JurTranspiler.compilerSource.semantic_model {

	public abstract class Type : ITreeNode {


		public abstract Type WithSubstitutedTypes(ISet<Substitution> typeMap);

		public abstract string Name { get; }

		public virtual string GetDefaultValue() => "null";

		public abstract ImmutableList<ITreeNode> ImmediateChildren { get; }

		public abstract ImmutableList<ITreeNode> AllChildren { get; }

    }

}
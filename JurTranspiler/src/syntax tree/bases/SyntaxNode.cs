using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.nodes;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.bases {

	public abstract partial class SyntaxNode : ISyntaxNode {

		public abstract ImmutableArray<ITreeNode> ImmediateChildren { get; }

		private ImmutableArray<ITreeNode>? allChildren;
		public ImmutableArray<ITreeNode> AllChildren {
			get {
				if (allChildren == null) {
					allChildren = GetAllChildren();
				}
				return allChildren.Value;
			}
		}

		public ISyntaxNode Root { get; }
		public ISyntaxNode? Parent { get; }
		public ImmutableArray<ISyntaxNode> AllParents { get; }
		public int Abstraction { get; }
		public string File { get; }
		public int Line { get; }
		public string OriginalText { get; }


		protected SyntaxNode(ISyntaxNode parent, ParserRuleContext context, ITerminalNode? lineToken = null) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = lineToken?.Symbol?.Line ?? context.Start.Line;

			var a = context.Start.StartIndex;
			var b = context.Stop.StopIndex;
			var interval = new Interval(a, b);
			OriginalText = context.Start.InputStream.GetText(interval);
		}


		protected SyntaxNode(int abstraction,
		                     ISyntaxNode parent,
		                     ParserRuleContext context,
		                     ITerminalNode? lineToken = null) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = GetAllParents();
			Abstraction = abstraction;
			File = parent.File;
			Line = lineToken?.Symbol?.Line ?? context.Start.Line;

			var a = context.Start.StartIndex;
			var b = context.Stop.StopIndex;
			var interval = new Interval(a, b);
			OriginalText = context.Start.InputStream.GetText(interval);
		}


		protected SyntaxNode(ISyntaxNode parent, int line) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = line;
			OriginalText = "";
		}


		protected SyntaxNode() {
			Root = this;
			Parent = null;
			Abstraction = -1;
			File = "";
			Line = -1;
			AllParents = ImmutableArray.Create<ISyntaxNode>();
			OriginalText = "";
		}


		protected SyntaxNode(int line,
		                     string file,
		                     int abstraction,
		                     ISyntaxNode parent,
		                     ParserRuleContext context) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = GetAllParents();
			Abstraction = abstraction;
			File = file;
			Line = line;

			var a = context.Start.StartIndex;
			var b = context.Stop.StopIndex;
			var interval = new Interval(a, b);
			OriginalText = context.Start.InputStream.GetText(interval);
		}


		protected SyntaxNode(ISyntaxNode parent, ParserRuleContext context, int parentLine) {
			Parent = parent;
			Root = Parent.Root;
			AllParents = GetAllParents();
			Abstraction = parent.Abstraction;
			File = parent.File;
			Line = parentLine;

			var a = context.Start.StartIndex;
			var b = context.Stop.StopIndex;
			var interval = new Interval(a, b);
			OriginalText = context.Start.InputStream.GetText(interval);
		}


		protected ImmutableArray<ISyntaxNode> GetAllParents() {
			ISyntaxNode parent = this;
			var builder = new List<ISyntaxNode>();

			while (parent?.Parent != null) {
				builder.Add(parent.Parent);
				parent = parent.Parent;
			}

			return builder.ToImmutableArray();
		}


		protected ImmutableArray<ITreeNode> GetAllChildren() {
			return ImmediateChildren.Concat(ImmediateChildren.SelectManyRecursive(x => x.ImmediateChildren)).ToImmutableArray();
		}


		protected bool IsInGenericFunction() {
			return AllParents
			       .FirstOrDefault(x => x is FunctionDefinitionSyntax)
			       ?.As<FunctionDefinitionSyntax>()
			       ?.IsGeneric ?? false;
		}


		public virtual string ToJs(Knowledge knowledge) => throw new NotImplementedException();

	}

}
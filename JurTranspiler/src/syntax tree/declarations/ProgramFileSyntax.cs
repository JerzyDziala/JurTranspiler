using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class ProgramFileSyntax : ISyntaxNode {

		//INode
		public ISyntaxNode Root { get; }
		public ISyntaxNode Parent { get; }
		public ImmutableList<ISyntaxNode> AllParents { get; }
		public ImmutableList<ITreeNode> ImmediateChildren { get; }
		public ImmutableList<ITreeNode> AllChildren { get; }

		public string File { get; }
		public int Line { get; }
		public int Abstraction { get; }

		public ImmutableList<AbstractionSyntax> Abstractions { get; }
		public ImmutableList<MainSyntax> Mains { get; }


		public ProgramFileSyntax(ISyntaxNode parent, string name, JurParser.ProgramContext context) {
			Parent = parent;
			Root = parent.Root;
			AllParents = this.GetAllParents();
			Abstraction = -1;
			Line = -1;
			File = name;

			Abstractions = context.abstraction().Select(x => new AbstractionSyntax(this, x)).ToImmutableList();
			Mains = context.main().Select(x => new MainSyntax(this, x)).ToImmutableList();

			ImmediateChildren = ImmutableList.Create<ITreeNode>()
			                                 .AddRange(Abstractions)
			                                 .AddRange(Mains);

			AllChildren = this.GetAllChildren();

		}


                public string ToJs(Knowledge knowledge) {
			return $@"
{Mains.Select(x => x.ToJs(knowledge)).Glue("\n")}

{Abstractions.Select(x => x.ToJs(knowledge)).Glue("\n")}
";
		}

	}

}
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class ProgramFileSyntax : SyntaxNode {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public ImmutableArray<AbstractionSyntax> Abstractions { get; }
        public ImmutableArray<MainSyntax> Mains { get; }


        public ProgramFileSyntax(ISyntaxNode parent, string name, JurParser.ProgramContext context)
            : base(line: -1,
                   file: name,
                   abstraction: -1,
                   parent: parent,
                   context: context) {

            Abstractions = ToAbstractions(context.abstraction());
            Mains = ToMains(context.main());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .AddRange(Abstractions)
                                              .AddRange(Mains);
            AllChildren = GetAllChildren();
        }


        public override string ToJs(Knowledge knowledge) {
            return $@"
{Mains.Select(x => x.ToJs(knowledge)).Glue("\n")}

{Abstractions.Select(x => x.ToJs(knowledge)).Glue("\n")}
";
        }

    }

}
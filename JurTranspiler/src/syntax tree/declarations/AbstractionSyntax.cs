using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class AbstractionSyntax : ISyntaxNode {

        //INode
        public ISyntaxNode Root { get; }
        public ISyntaxNode Parent { get; }
        public ImmutableList<ISyntaxNode> AllParents { get; }
        public ImmutableList<ITreeNode> ImmediateChildren { get; }
        public ImmutableList<ITreeNode> AllChildren { get; }

        public string File { get; }
        public int Line { get; }
        public int Abstraction { get; }

        //Children
        public ImmutableList<FunctionDefinitionSyntax> FunctionDeclarations { get; }
        public ImmutableList<StructDefinitionSyntax> StructDeclarations { get; }


        public AbstractionSyntax(ProgramFileSyntax parent, JurParser.AbstractionContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = int.Parse(context.NUMBER_VALUE().GetText());
            File = parent.File;
            Line = context.Start.Line;

            FunctionDeclarations = context.functionDeclaration().Select(x => new FunctionDefinitionSyntax(this, x)).ToImmutableList();
            StructDeclarations = context.structDeclaration().Select(x => new StructDefinitionSyntax(this, x)).ToImmutableList();

            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddRange(FunctionDeclarations)
                                             .AddRange(StructDeclarations);

            AllChildren = this.GetAllChildren();

        }


        public string ToJs(Knowledge knowledge) {
            return $@"
{FunctionDeclarations.Select(x => x.ToJs(knowledge)).Glue("\n")}";
        }

    }

}
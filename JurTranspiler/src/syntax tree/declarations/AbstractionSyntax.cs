using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public class AbstractionSyntax : SyntaxNode {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        //Children
        public ImmutableArray<FunctionDefinitionSyntax> FunctionDeclarations { get; }
        public ImmutableArray<StructDefinitionSyntax> StructDeclarations { get; }


        public AbstractionSyntax(ProgramFileSyntax parent, JurParser.AbstractionContext context)
            : base(int.Parse(context.NUMBER_VALUE().GetText()),
                   parent,
                   context) {

            FunctionDeclarations = ToFunctionDefinitions(context.functionDeclaration());
            StructDeclarations = ToStructDefinitions(context.structDeclaration());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>()
                                              .AddRange(FunctionDeclarations)
                                              .AddRange(StructDeclarations);
            AllChildren = GetAllChildren();

        }


        public override string ToJs(Knowledge knowledge) {
            return $@"
{FunctionDeclarations.Select(x => x.ToJs(knowledge)).Glue("\n")}";
        }

    }

}
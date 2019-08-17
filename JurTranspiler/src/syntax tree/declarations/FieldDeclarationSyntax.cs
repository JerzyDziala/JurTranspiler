using System;
using System.Collections.Immutable;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.syntax_tree.bases;

namespace JurTranspiler.compilerSource.nodes {

    public class FieldDeclarationSyntax : SyntaxNode, IVariableDeclarationSyntax {

        public override ImmutableArray<ITreeNode> ImmediateChildren { get; }
        public override ImmutableArray<ITreeNode> AllChildren { get; }

        public string Name { get; }
        public ITypeSyntax? Type { get; }


        public FieldDeclarationSyntax(StructDefinitionSyntax parent, JurParser.UninitializedVarDeclarationContext context) : base(parent, context) {

            Name = context.ID().GetText();
            Type = ToType(context.type());

            ImmediateChildren = ImmutableArray.Create<ITreeNode>().Add(Type);
            AllChildren = GetAllChildren();

        }


        public override string ToJs(Knowledge knowledge) {
            throw new NotImplementedException();
        }

    }

}
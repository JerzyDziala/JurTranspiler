using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.Analysis;
using JurTranspiler.syntax_tree.bases;
using JurTranspiler.syntax_tree.expressions;
using JurTranspiler.syntax_tree.Interfaces;
using UtilityLibrary;

namespace JurTranspiler.syntax_tree.declarations {

	public sealed class SyntaxTree : SyntaxNode {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public ImmutableArray<ProgramFileSyntax> Files { get; }
		public ImmutableArray<StructDefinitionSyntax> AllStructDefinitions { get; }
		public ImmutableArray<FunctionDefinitionSyntax> AllFunctionDefinitions { get; }
		public ImmutableArray<ITypeSyntax> AllTypeUsages { get; }
		public ImmutableArray<ConstructorSyntax> AllConstructors { get; }
		public ImmutableArray<IExpressionSyntax> AllExpressions { get; }
		public ImmutableArray<IAssignment> AllAssignments { get; }
		public ImmutableArray<FunctionDefinitionSyntax> AllFunctions { get; }
		public ImmutableArray<MainSyntax> AllMains { get; }
		public ImmutableArray<VariableAccessSyntax> VariableUsages { get; }
		public ImmutableArray<IVariableDeclarationSyntax> VariableDeclarations { get; }
		public ImmutableArray<AnonymousFunctionSyntax> AllLambdas { get; }
		public ImmutableArray<InitializerSyntax> AllInitializers { get; }


		public SyntaxTree(IEnumerable<(JurParser.ProgramContext context, string fileName)> files) : base() {

			Files = files.Select(x => new ProgramFileSyntax(this, x.fileName, x.context)).ToImmutableArray();

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddRange(Files);

			//TODO: optimize
			AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
			AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
			AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
			AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
			AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
			AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
			AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
			VariableDeclarations = AllChildren.OfType<IVariableDeclarationSyntax>().ToImmutableArray();
			AllConstructors = AllChildren.OfType<ConstructorSyntax>().ToImmutableArray();
			VariableUsages = AllChildren.OfType<VariableAccessSyntax>().ToImmutableArray();
			AllLambdas = AllChildren.OfType<AnonymousFunctionSyntax>().ToImmutableArray();
			AllInitializers = AllChildren.OfType<InitializerSyntax>().ToImmutableArray();
		}


		public SyntaxTree(JurParser.ProgramContext context) : base() {

			Files = ImmutableArray.Create<ProgramFileSyntax>()
			                      .Add(new ProgramFileSyntax(this, "__TEST__", context));

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddRange(Files);

			//TODO: optimize

			AllStructDefinitions = AllChildren.OfType<StructDefinitionSyntax>().ToImmutableArray();
			AllFunctionDefinitions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
			AllConstructors = AllChildren.OfType<ConstructorSyntax>().ToImmutableArray();
			AllTypeUsages = AllChildren.OfType<ITypeSyntax>().ToImmutableArray();
			AllExpressions = AllChildren.OfType<IExpressionSyntax>().ToImmutableArray();
			AllAssignments = AllChildren.OfType<IAssignment>().ToImmutableArray();
			AllFunctions = AllChildren.OfType<FunctionDefinitionSyntax>().ToImmutableArray();
			AllMains = AllChildren.OfType<MainSyntax>().ToImmutableArray();
			VariableDeclarations = AllChildren.OfType<IVariableDeclarationSyntax>().ToImmutableArray();
			VariableUsages = AllChildren.OfType<VariableAccessSyntax>().ToImmutableArray();
			AllLambdas = AllChildren.OfType<AnonymousFunctionSyntax>().ToImmutableArray();
			AllInitializers = AllChildren.OfType<InitializerSyntax>().ToImmutableArray();
		}


		public override string ToJs(Knowledge knowledge) {
			return $"{Files.Select(x => x.ToJs(knowledge)).Glue("\n\n")}\n\nmain$();";
		}

	}

}
using System;
using System.Collections.Immutable;
using System.Linq;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model.functions;
using JurTranspiler.syntax_tree.bases;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

	public class FunctionCallSyntax : SyntaxNode, IExpressionSyntax {

		public override ImmutableArray<ITreeNode> ImmediateChildren { get; }

		public string Name { get; }
		public bool IsPoly { get; }
		public ImmutableArray<ITypeSyntax> ExplicitTypeArguments { get; }
		public bool HasExplicitTypeArguments => ExplicitTypeArguments.Any();
		public ImmutableArray<IExpressionSyntax> Arguments { get; }


		public FunctionCallSyntax(ISyntaxNode parent, JurParser.FunctionCallContext context) : base(parent, context, context.ID()) {

			Name = context.ID().GetText();
			Arguments = ToExpressions(context.expression());
			ExplicitTypeArguments = ToTypes(context.type());
			IsPoly = context.POLY() != null;

			ImmediateChildren = ImmutableArray.Create<ITreeNode>()
			                                  .AddRange(Arguments)
			                                  .AddRange(ExplicitTypeArguments);

		}


		public override string ToJs(Knowledge knowledge) {

			var functionCallInfo = knowledge.FunctionCallsBindings[this];
			var boundCallable = functionCallInfo.Callable;

			if (IsPoly) throw new NotImplementedException("poly methods are not supported yet");

			var args = Arguments.Select(x => x.ToJs(knowledge)).ToList();

			//extern function calls are easy
			if (boundCallable.IsExtern) {
				if (boundCallable.IsStatic) {
					return $"{boundCallable.StaticTypeName!}.{Name}({args.Glue(", ")})";
				}
				if (boundCallable.IsMember) {
					return $"{args.First()}.{Name}({args.Skip(1).Glue(", ")})";
				}
			}

			//non extern calls sometime need passing in substitution informations as argument
			if (functionCallInfo.Substitutions.Any()) {

				Func<string, string> withSubs = s => IsInGenericFunction() ? $"{s}._wst_(_s_)" : s;
				Func<string, string> toTypeArgumentString = s => withSubs($"_t_[{s.AsString()}]");

				var subs = functionCallInfo.Substitutions
				                           .Select(x => $"new Sub(_t_[{x.typeParameter.Name.AsString()}], {toTypeArgumentString(x.typeArgument.Name)})")
				                           .AsArray();
				args.Add(subs);
			}

			var mangledName = knowledge.GetNewNameFor(this);

			return $"{mangledName}({args.Glue(", ")})";
		}

	}

}
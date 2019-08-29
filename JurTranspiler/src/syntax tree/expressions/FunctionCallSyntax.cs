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

            var boundCallableInfo = knowledge.FunctionCallsBindings[this];

            if (IsPoly) throw new NotImplementedException("poly methods are not supported yet");

            //generate arguments with or without substitutions
            var args = $"{Arguments.Select(x => x.ToJs(knowledge)).Glue(", ")}";

            if (boundCallableInfo.Substitutions.Any()) {

                Func<string, string> withSubs = s => IsInGenericFunction() ? $"{s}._wst_(_s_)" : s;
                Func<string, string> toTypeArgumentString = s => withSubs($"_t_[{s.AsString()}]");

                var subs = boundCallableInfo.Substitutions
                                            .Select(x => $"new Sub(_t_[{x.typeParameter.Name.AsString()}], {toTypeArgumentString(x.typeArgument.Name)})")
                                            .AsArray();
                if (boundCallableInfo.Callable.Arity > 0) args += ", ";
                args += subs;
            }

            string getNewName() {
                //if it is a function pointer then we have to use the new name of the variable in with it was declared
                return boundCallableInfo.Callable is FunctionPointer pointer
                           ? knowledge.GetNewNameFor(pointer.declaration!)
                           : knowledge.GetNewNameFor(this);
            }

            return $"{getNewName()}({args})";
        }

    }

}
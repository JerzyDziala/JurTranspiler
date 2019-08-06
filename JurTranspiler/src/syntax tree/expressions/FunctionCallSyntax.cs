using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Schema;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.CodeGeneration;
using JurTranspiler.compilerSource.parsing.Implementations;
using JurTranspiler.compilerSource.semantic_model;
using UtilityLibrary;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

    public class FunctionCallSyntax : IExpressionSyntax, ISyntaxNode {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public string Name { get; }
        public bool IsPoly { get; }
        public ImmutableList<ITypeSyntax> ExplicitTypeArguments { get; }
        public bool HasExplicitTypeArguments => ExplicitTypeArguments.Any();
        public ImmutableList<IExpressionSyntax> Arguments { get; }


        public FunctionCallSyntax(ISyntaxNode parent, JurParser.FunctionCallContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.ID().Symbol.Line;

            Name = context.ID().GetText();
            Arguments = context.expression().Select(x => ExpressionSyntaxFactory.CreateExpressionSyntax(this, x)).ToImmutableList();
            ExplicitTypeArguments = context.type().Select(x => TypeSyntaxFactory.Create(this, x)).ToImmutableList();
            IsPoly = context.POLY() != null;
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .AddRange(Arguments)
                                             .AddRange(ExplicitTypeArguments);
            AllChildren = this.GetAllChildren();

        }


        public override string ToJs(Knowledge knowledge) {

            var boundCallableInfo = knowledge.FunctionCallsBindings[this];

            if (IsPoly) throw new NotImplementedException("poly methods are not supported yet");

            var args = $"{Arguments.Select(x => x.ToJs(knowledge)).Glue(", ")}";

            Func<string, string> withSubs = s => this.IsInGenericFunction() ? $"{s}.withSubstitutedTypes(substitutions)" : s;
            Func<string, string> toTypeArgumentString = s => withSubs($"types[{s.AsString()}]");

            var subs = boundCallableInfo.Substitutions
                                        .Select(x => $"typeParameter: types[{x.typeParameter.Name.AsString()}], typeArgument: {toTypeArgumentString(x.typeArgument.Name)}".AsObject())
                                        .AsArray();
            if (boundCallableInfo.Callable.Arity > 0) args += ", ";
            args += subs;
            return $"{knowledge.GetNewNameFor(this)}({args})";
        }

    }

}
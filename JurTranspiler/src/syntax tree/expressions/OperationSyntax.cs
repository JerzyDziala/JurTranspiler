using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using JurTranspiler.Analysis.errors;
using JurTranspiler.compilerSource.Analysis;
using JurTranspiler.compilerSource.semantic_model;
using Type = JurTranspiler.compilerSource.semantic_model.Type;

namespace JurTranspiler.compilerSource.nodes {

    public class OperationSyntax : IExpressionSyntax, ISyntaxNode {

        public override ISyntaxNode Root { get; }
        public override ISyntaxNode Parent { get; }
        public override ImmutableList<ISyntaxNode> AllParents { get; }
        public override ImmutableList<ITreeNode> ImmediateChildren { get; }
        public override ImmutableList<ITreeNode> AllChildren { get; }
        public override string File { get; }
        public override int Line { get; }
        public override int Abstraction { get; }

        public string Operator { get; }
        public IExpressionSyntax Left { get; }
        public IExpressionSyntax Right { get; }


        public OperationSyntax(ISyntaxNode parent, JurParser.OperationContext context) {
            Parent = parent;
            Root = Parent.Root;
            AllParents = this.GetAllParents();
            Abstraction = parent.Abstraction;
            File = parent.File;
            Line = context.Start.Line;

            var op = context.@operator?.Text ?? context.AND()?.GetText() ?? context.OR()?.GetText();

            Operator = op;
            Left = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(0));
            Right = ExpressionSyntaxFactory.CreateExpressionSyntax(this, context.expression(1));
            ImmediateChildren = ImmutableList.Create<ITreeNode>()
                                             .Add(Left)
                                             .Add(Right);
            AllChildren = this.GetAllChildren();

        }


        public override Type Evaluate(HashSet<Error> errors, Binder binder) {
            var left = Left.Evaluate(errors, binder);
            var right = Right.Evaluate(errors, binder);
            if (Operator == "+") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.NUM);
                }
                if (left.Name == "num" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "string" && right.Name == "bool") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (left.Name == "bool" && right.Name == "string") {
                    return new PrimitiveType(PrimitiveKind.STRING);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: File,
                                                               line: Line,
                                                               op: Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (Operator == "-" || Operator == "*" || Operator == "/") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.NUM);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: File,
                                                               line: Line,
                                                               op: Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (Operator == ">" || Operator == "<" || Operator == ">=" || Operator == "<=") {
                if (left.Name == "num" && right.Name == "num") {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: File,
                                                               line: Line,
                                                               op: Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (Operator == "==" || Operator == "!=" || Operator == "is") {
                if (left.IsAssignableTo(right, errors) || right.IsAssignableTo(left, errors)) {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: File,
                                                               line: Line,
                                                               op: Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }
            else if (Operator == "&&" || Operator == "||" || Operator == "and" || Operator == "or") {
                if (left.Name == "bool" && right.Name == "bool") {
                    return new PrimitiveType(PrimitiveKind.BOOL);
                }
                if (!(left is UndefinedType) && !(right is UndefinedType)) {
                    errors.Add(new TypeMismatchInUseOfOperator(file: File,
                                                               line: Line,
                                                               op: Operator,
                                                               leftName: left.Name,
                                                               rightName: right.Name));
                }
                return new UndefinedType();
            }

            throw new Exception("WTF! invalid operator assigned during parsing?");

        }


        public override string ToJs(Binder binder) {
            return $"{Left.ToJs(binder)} {Operator} {Right.ToJs(binder)}";
        }

    }

}
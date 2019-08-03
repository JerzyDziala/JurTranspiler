using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UtilityLibrary;

namespace JurTranspiler.compilerSource.nodes {

    public static class INodeExtensions {

        public static ImmutableList<ITreeNode> GetAllChildren(this ITreeNode node) {
            return node.ImmediateChildren.Concat(node.ImmediateChildren.SelectManyRecursive(x => x.ImmediateChildren)).ToImmutableList();
        }


        public static ImmutableList<ISyntaxNode> GetAllParents(this ISyntaxNode node) {
            var parent = node;
            var builder = new List<ISyntaxNode>();

            while (parent?.Parent != null) {
                builder.Add(parent.Parent);
                parent = parent.Parent;
            }

            return builder.ToImmutableList();
        }


        public static bool IsInGenericFunction(this ISyntaxNode node) => node.AllParents
                                                                             .FirstOrDefault(x => x is FunctionDefinitionSyntax)
                                                                             ?.As<FunctionDefinitionSyntax>()
                                                                             ?.IsGeneric ?? false;
    }

}
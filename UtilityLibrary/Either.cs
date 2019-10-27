using System;

namespace UtilityLibrary {
    
    public class Either<L, R> where L : class where R : class {
        
        private L Left { get; }
        private R Right { get; }


        public Either(L l) {
            Left = l;
            Right = null;
        }
        public Either(R r) {
            Left = null;
            Right = r;
        }

        public Result Match<Result>(Func<L, Result> left, Func<R, Result> right) {
            return Left != null ? left(Left) : right(Right);
        }
    }
}
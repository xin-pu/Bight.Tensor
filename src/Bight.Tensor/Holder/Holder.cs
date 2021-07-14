﻿using System;

namespace Bight.Tensor.Holder
{
    public class Holder<T>
        where T : struct
    {
        public Holder()
        {
            if (typeof(T) == typeof(double))
                Operations = new DoubleWrapper() as IOperations<T>;
            else if (typeof(T) == typeof(int))
                Operations = new IntWrapper() as IOperations<T>;
            else throw new NotSupportedException();
        }

        public IOperations<T> Operations { set; get; }


        /// <returns>
        ///     0 (zero). A primitive of the same type
        /// </returns>
        public T Zero => Operations.Zero;

        /// <returns>
        ///     1 (one). A primitive of the same type
        /// </returns>
        public T One => Operations.One;

        /// <summary>
        ///     Rules of adding elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        public T Add(T a, T b)
        {
            return Operations.Add(a, b);
        }

        /// <summary>
        ///     Rules of subtracting elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        public T Subtract(T a, T b)
        {
            return Operations.Subtract(a, b);
        }

        /// <summary>
        ///     Rules of multiplying elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        public T Multiply(T a, T b)
        {
            return Operations.Multiply(a, b);
        }

        /// <summary>
        ///     Rules of multiplying an element by -1. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        public T Negate(T a)
        {
            return Operations.Negate(a);
        }

        /// <summary>
        ///     Rules of dividing elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        public T Divide(T a, T b)
        {
            return Operations.Divide(a, b);
        }

        /// <returns>
        ///     If your elements are mutable, it
        ///     might be useful to be able to copy
        ///     them as well.
        /// </returns>
        public T Copy(T a)
        {
            return Operations.Copy(a);
        }

        /// <summary>
        ///     Determines whether the instances
        ///     of your objects are equal
        /// </summary>
        public bool AreEqual(T a, T b)
        {
            return Operations.AreEqual(a, b);
        }

        /// <summary>
        ///     Whether the given instance is zero
        /// </summary>
        public bool IsZero(T a)
        {
            return Operations.IsZero(a);
        }

        /// <summary>
        ///     Get the string representation of the instance
        /// </summary>
        public string ToString(T a)
        {
            return Operations.ToString(a);
        }
    }
}
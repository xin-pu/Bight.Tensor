namespace Bight.Tensor.Holder
{
    public interface IOperations<T>
    {
        /// <returns>
        ///     1 (one). A primitive of the same type
        /// </returns>
        T One { get; }

        /// <returns>
        ///     0 (zero). A primitive of the same type
        /// </returns>
        T Zero { get; }

        /// <summary>
        ///     Rules of adding elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        T Add(T a, T b);

        /// <summary>
        ///     Rules of subtracting elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        T Subtract(T a, T b);

        /// <summary>
        ///     Rules of multiplying elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        T Multiply(T a, T b);

        /// <summary>
        ///     Rules of multiplying an element by -1. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        T Negate(T a);

        /// <summary>
        ///     Rules of dividing elements. Must return a new one.
        ///     Should not modify the old ones.
        /// </summary>
        /// <returns>
        ///     A primitive of the same type
        /// </returns>
        T Divide(T a, T b);

        /// <returns>
        ///     If your elements are mutable, it
        ///     might be useful to be able to copy
        ///     them as well.
        /// </returns>
        T Copy(T a);

        /// <summary>
        ///     Determines whether the instances
        ///     of your objects are equal
        /// </summary>
        bool AreEqual(T a, T b);

        /// <summary>
        ///     Whether the given instance is zero
        /// </summary>
        bool IsZero(T a);

        /// <summary>
        ///     Get the string representation of the instance
        /// </summary>
        string ToString(T a);
    }
}
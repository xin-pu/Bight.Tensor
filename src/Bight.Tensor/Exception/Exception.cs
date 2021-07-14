using System;
using System.Data;

namespace Bight.Tensor.Exception
{
    /// <summary>
    ///     Occurs when an axis mismatch happens
    /// </summary>
    public class InvalidShapeException : ArgumentException
    {
        internal InvalidShapeException(string msg) : base(msg)
        {
        }

        internal InvalidShapeException()
        {
        }

        internal static void NeedTensorSquareMatrix<T>(Tensor<T> m)
            where T : struct
        {
            if (m.Rank <= 2)
                throw new InvalidShapeException("Should be 3+ dimensional");
            if (m.Size.shape[m.Size.Rank - 1] != m.Size.shape[m.Size.Rank - 2])
                throw new InvalidShapeException("The last two dimensions should be equal");
        }
    }

    /// <summary>
    ///     Thrown when a wrong determinant tensor was provided
    /// </summary>
    public class InvalidDeterminantException : DataException
    {
        internal InvalidDeterminantException(string msg) : base(msg)
        {
        }

        internal InvalidDeterminantException()
        {
        }
    }
}
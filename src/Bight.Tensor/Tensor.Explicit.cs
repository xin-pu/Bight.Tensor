using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
       

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureDType(Tensor<T> tensor, Type type)
        {
            if (tensor.DType != type)
                throw new InvalidCastException($"Unable to cast scalar tensor {tensor.dtype} to {@is}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureScalar(Tensor<T> tensor)
        {
            if (tensor == null)
                throw new ArgumentNullException(nameof(tensor));

            if (tensor.Rank != 0)
                throw new ArgumentException("Tensor must have 0 dimensions in order to convert to scalar");

            if (tensor.Size != 1)
                throw new ArgumentException("Tensor must have size 1 in order to convert to scalar");
        }
    }
}

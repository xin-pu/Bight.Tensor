using System.Runtime.CompilerServices;
using Bight.Tensor.Exception;

namespace Bight.Tensor.Static
{
    public static class TensorOps<T>
        where T : struct
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> Stack(params Tensor<T>[] elements)
        {
            if (elements.Length < 1)
                throw new InvalidShapeException("Should be at least one element to stack");

            var desiredShape = elements[0].Size;

            for (var i = 1; i < elements.Length; i++)
                if (elements[i].Size != desiredShape)
                    throw new InvalidShapeException($"Tensors in {nameof(elements)} should be of the same shape");

            var newShape = new int[desiredShape.Rank + 1];
            newShape[0] = elements.Length;
            for (var i = 1; i < newShape.Length; i++)
                newShape[i] = desiredShape[i - 1];
            var res = new Tensor<T>(newShape);
            for (var i = 0; i < elements.Length; i++)
                res.SetSubTensor(elements[i], i);
            return res;
        }

        /// <summary>
        ///     Concatenates two tensors over the first axis,
        ///     for example, if you had a tensor of
        ///     [4 x 3 x 5] and a tensor of [9 x 3 x 5], their concat
        ///     result will be of shape [13 x 3 x 5]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> Concat(Tensor<T> a, Tensor<T> b)
        {
            if (a.Size.SubShape(1, 0) != b.Size.SubShape(1, 0))
                throw new InvalidShapeException("Excluding the first dimension, all others should match");


            if (a.IsVector)
            {
                var resultingVector = Tensor<T>.BuildVector(a.Size.shape[0] + b.Size.shape[0]);
                for (var i = 0; i < a.Size.shape[0]; i++)
                    resultingVector.SetValueNoCheck(a.GetValueNoCheck(i), i);

                for (var i = 0; i < b.Size.shape[0]; i++)
                    resultingVector.SetValueNoCheck(b.GetValueNoCheck(i), i + a.Size.shape[0]);

                return resultingVector;
            }

            var newShape = a.Size.Clone() as TensorSize;
            newShape.shape[0] = a.Size.shape[0] + b.Size.shape[0];

            var res = new Tensor<T>(newShape);
            for (var i = 0; i < a.Size.shape[0]; i++)
                res.SetSubTensor(a.GetSubTensor(i), i);

            for (var i = 0; i < b.Size.shape[0]; i++)
                res.SetSubTensor(b.GetSubTensor(i), i + a.Size.shape[0]);

            return res;
        }
    }
}
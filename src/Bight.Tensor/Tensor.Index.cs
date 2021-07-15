using Bight.Tensor.Exception;
using Bight.Tensor.Static;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        /// <summary>
        ///     Element-wise indexing,
        ///     for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        ///     A correct way to index it would be
        ///     t[0, 0, 1] or t[1, 2, 3],
        ///     but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x]
        {
            get => Storage[GetFlattenedIndexWithCheck(x)];
            set => Storage[GetFlattenedIndexWithCheck(x)] = value;
        }

        /// <summary>
        ///     Element-wise indexing,
        ///     for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        ///     A correct way to index it would be
        ///     t[0, 0, 1] or t[1, 2, 3],
        ///     but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z]
        {
            get => Storage[GetFlattenedIndexWithCheck(x, y, z)];
            set => Storage[GetFlattenedIndexWithCheck(x, y, z)] = value;
        }

        /// <summary>
        ///     Element-wise indexing,
        ///     for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        ///     A correct way to index it would be
        ///     t[0, 0, 1] or t[1, 2, 3],
        ///     but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y]
        {
            get => Storage[GetFlattenedIndexWithCheck(x, y)];
            set => Storage[GetFlattenedIndexWithCheck(x, y)] = value;
        }

        /// <summary>
        ///     Gets the value by an array of indices.
        /// </summary>
        public T this[params int[] indices]
        {
            get => Storage[GetFlattenedIndexWithCheck(indices)];
            set => Storage[GetFlattenedIndexWithCheck(indices)] = value;
        }


        public Tensor<T> Slice(int leftIncluding, int rightExcluding)
        {
            ReactIfBadAxesVol(leftIncluding, 0);
            ReactIfBadAxesVol(rightExcluding - 1, 0);
            if (leftIncluding >= rightExcluding)
                throw new InvalidShapeException("Slicing cannot be performed");

            var newLength = rightExcluding - leftIncluding;
            var toStack = new Tensor<T>[newLength];
            for (var i = 0; i < newLength; i++)
                toStack[i] = GetSubTensor(i + leftIncluding);
            return TensorOps<T>.Stack(toStack);
        }
    }
}
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        private bool IsMatrxi => Rank == 3;
        private bool IsVector => Rank == 2;
        private int LinOffset => 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadAxesVol(int vol, int axisId)
        {
            if (vol < 0 || vol >= Shape.shape[axisId])
                throw new IndexOutOfRangeException(
                    $"Axes[{axisId}] should be {Shape.shape[axisId]} indices, not {vol}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadRank(int rank)
        {
            if (rank != Rank)
                throw new ArgumentException($"Tensor should be {Rank} indices, not {rank}");
        }

        private int GetFlattenedIndexWithCheck(params int[] indices)
        {

            ReactIfBadRank(indices.Length);
            Enumerable.Range(0, indices.Length).ToList()
                .ForEach(axes => { ReactIfBadAxesVol(indices[axes], axes); });

            return LinOffset + indices.Zip(BlockShape, (a, b) => a * b).Sum();
        }



        private int GetFlattenedIndexWithCheck(int x)
        {
            ReactIfBadRank(1);
            ReactIfBadAxesVol(x, 0);
            return LinOffset + BlockShape[0] * x;
        }

        private int GetFlattenedIndexWithCheck(int x, int y)
        {
            ReactIfBadRank(2);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            return LinOffset + BlockShape[0] * x + BlockShape[1] * y;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z)
        {

            ReactIfBadRank(3);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            ReactIfBadAxesVol(z, 2);
            return LinOffset + BlockShape[0] * x + BlockShape[1] * y + BlockShape[2] * z;
        }


        /// <summary>
        /// Gets the value by an array of indices.
        /// </summary>
        public T this[params int[] indices]
        {
            get => Data[GetFlattenedIndexWithCheck(indices)];
            set => Data[GetFlattenedIndexWithCheck(indices)] = value;
        }


        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x]
        {
            get => Data[GetFlattenedIndexWithCheck(x)];
            set => Data[GetFlattenedIndexWithCheck(x)] = value;
        }


        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z]
        {
            get => Data[GetFlattenedIndexWithCheck(x, y, z)];
            set => Data[GetFlattenedIndexWithCheck(x, y, z)] = value;
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubtensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y]
        {
            get => Data[GetFlattenedIndexWithCheck(x, y)];
            set => Data[GetFlattenedIndexWithCheck(x, y)] = value;
        }



        #region Slice

        #endregion
    }
}

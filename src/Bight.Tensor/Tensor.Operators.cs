﻿using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        public bool IsTensor => Rank >= 3;
        public bool IsMatrix => Rank == 2;
        public bool IsVector => Rank == 1;
        public bool IsSquareMatrix => IsMatrix && Size.Reverse()[0] == Size.Reverse()[1];


        private int LinOffset { set; get; } = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReactIfBadAxesVol(int vol, int axisId)
        {
            if (vol < 0 || vol >= Size.shape[axisId])
                throw new IndexOutOfRangeException(
                    $"Axes[{axisId}] should be {Size.shape[axisId]} indices, not {vol}");
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

            return LinOffset + indices.Zip(Stride.shape, (a, b) => a * b).Sum();
        }



        private int GetFlattenedIndexWithCheck(int x)
        {
            ReactIfBadRank(1);
            ReactIfBadAxesVol(x, 0);
            return LinOffset + Stride[0] * x;
        }

        private int GetFlattenedIndexWithCheck(int x, int y)
        {
            ReactIfBadRank(2);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            return LinOffset + Stride[0] * x + Stride[1] * y;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z)
        {

            ReactIfBadRank(3);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            ReactIfBadAxesVol(z, 2);
            return LinOffset + Stride[0] * x + Stride[1] * y + Stride[2] * z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x)
            => Stride[0] * x +
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y)
            => Stride[0] * x +
               Stride[1] * y +
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z)
            => Stride[0] * x +
               Stride[1] * y +
               Stride[2] * z +
               LinOffset;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z, int[] other)
        {
            var res = GetFlattenedIndexSilent(x, y, z);
            for (int i = 0; i < other.Length; i++)
                res += other[i] * Stride[i + 3];
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetFlattenedIndexSilent(int[] other)
        {
            var res = 0;
            for (int i = 0; i < other.Length; i++)
                res += other[i] * Stride[i];
            return res + LinOffset;
        }


        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x)
        {
            return Storage[GetFlattenedIndexSilent(x)];
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y)
        {
            return Storage[GetFlattenedIndexSilent(x, y)];
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z)
        {
            return Storage[GetFlattenedIndexSilent(x, y, z)];
        }

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z, int[] indices)
            => Storage[GetFlattenedIndexSilent(x, y, z, indices)];

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int[] indices)
            => Storage[GetFlattenedIndexSilent(indices)];

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x)
            => Storage[GetFlattenedIndexSilent(x)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y)
            => Storage[GetFlattenedIndexSilent(x, y)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z)
            => Storage[GetFlattenedIndexSilent(x, y, z)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z, int[] other)
            => Storage[GetFlattenedIndexSilent(x, y, z, other)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int[] indices)
            => Storage[GetFlattenedIndexSilent(indices)] = value;

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x)
            => Storage[GetFlattenedIndexSilent(x)] = valueCreator();

        /// <summary>
        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y)
            => Storage[GetFlattenedIndexSilent(x, y)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z)
            => Storage[GetFlattenedIndexSilent(x, y, z)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z, int[] indices)
            => Storage[GetFlattenedIndexSilent(x, y, z, indices)] = valueCreator();

        /// <summary>
        /// Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int[] indices)
            => Storage[GetFlattenedIndexSilent(indices)] = valueCreator();



        /// <summary>
        /// This SubTensor is sequential SubTensor(int)
        ///
        /// O(1)
        /// </summary>
        public Tensor<T> GetSubTensor(int[] indices)
            => GetSubTensor(indices, 0);

        internal Tensor<T> GetSubTensor(int[] indices, int id)
            => id == indices.Length ? this : GetSubTensor(indices[id]).GetSubTensor(indices, id + 1);
  
        /// <summary>
        /// Get a SubTensor of a tensor
        /// If you have a t = Tensor[2 x 3 x 4],
        /// t.GetSubTensor(0) will return the proper matrix [3 x 4]
        ///
        /// O(1)
        /// </summary>
        public Tensor<T> GetSubTensor(int index)
        {

            ReactIfBadAxesVol(index, 0);
            var newLinIndexDelta = GetFlattenedIndexSilent(index);
            var newShape = Size.SubTensorShape();

            var result = new Tensor<T>(newShape, Storage)
            {
                LinOffset = newLinIndexDelta
            };
            return result;
        }

        private void SetSubTensor(Tensor<T> subTensor, params int[] indices)
        {
            if (indices.Rank >= Size.Rank)
                throw new ArgumentException(
                    $"Number of {nameof(indices)} should be less than number of {nameof(Size)}");
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] < 0 || indices[i] >= Size[i])
                    throw new ArgumentException();
            if (Size.Rank - indices.Length != subTensor.Rank)
                throw new ArgumentException(
                    $"Number of {nameof(subTensor.Rank)} + {nameof(indices.Length)} should be equal to {Size.Rank}");

        }

        /// <summary>
        /// Gets the value by an array of indices.
        /// </summary>
        public T this[params int[] indices]
        {
            get => Storage[GetFlattenedIndexWithCheck(indices)];
            set => Storage[GetFlattenedIndexWithCheck(indices)] = value;
        }


        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x]
        {
            get => Storage[GetFlattenedIndexWithCheck(x)];
            set => Storage[GetFlattenedIndexWithCheck(x)] = value;
        }


        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y, int z]
        {
            get => Storage[GetFlattenedIndexWithCheck(x, y, z)];
            set => Storage[GetFlattenedIndexWithCheck(x, y, z)] = value;
        }

        /// <summary>
        /// Element-wise indexing,
        /// for example suppose you have a t = Tensor[2 x 3 x 4] of int-s
        /// A correct way to index it would be
        /// t[0, 0, 1] or t[1, 2, 3],
        /// but neither of t[0, 1] (Use GetSubTensor for this) and t[4, 5, 6] (IndexOutOfRange)
        /// </summary>
        public T this[int x, int y]
        {
            get => Storage[GetFlattenedIndexWithCheck(x, y)];
            set => Storage[GetFlattenedIndexWithCheck(x, y)] = value;
        }


       
    }
}

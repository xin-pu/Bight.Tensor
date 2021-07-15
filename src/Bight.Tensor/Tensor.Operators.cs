using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Bight.Tensor.Exception;
using Bight.Tensor.Static;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        public bool IsVector => Rank == 1;
        public bool IsMatrix => Rank == 2;
        public bool IsTensor => Rank >= 3;
        public bool IsContiguous => GetContiguous();
        public bool IsSquareMatrix => IsMatrix && Size.Reverse()[0] == Size.Reverse()[1];
        public int Offset { set; get; } = 0;


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

            return Offset + indices.Zip(Stride.shape, (a, b) => a * b).Sum();
        }

        private int GetFlattenedIndexWithCheck(int x)
        {
            ReactIfBadRank(1);
            ReactIfBadAxesVol(x, 0);
            return Offset + Stride[0] * x;
        }

        private int GetFlattenedIndexWithCheck(int x, int y)
        {
            ReactIfBadRank(2);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            return Offset + Stride[0] * x + Stride[1] * y;
        }

        private int GetFlattenedIndexWithCheck(int x, int y, int z)
        {
            ReactIfBadRank(3);
            ReactIfBadAxesVol(x, 0);
            ReactIfBadAxesVol(y, 1);
            ReactIfBadAxesVol(z, 2);
            return Offset + Stride[0] * x + Stride[1] * y + Stride[2] * z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x)
        {
            return Stride[0] * x +
                   Offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y)
        {
            return Stride[0] * x +
                   Stride[1] * y +
                   Offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z)
        {
            return Stride[0] * x +
                   Stride[1] * y +
                   Stride[2] * z +
                   Offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal int GetFlattenedIndexSilent(int x, int y, int z, int[] other)
        {
            return GetFlattenedIndexSilent(x, y, z) + other.Select((t, i) => t * Stride[i + 3]).Sum();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetFlattenedIndexSilent(int[] other)
        {
            var res = other.Select((t, i) => t * Stride[i]).Sum();
            return res + Offset;
        }


        /// Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x)
        {
            return Storage[GetFlattenedIndexSilent(x)];
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y)
        {
            return Storage[GetFlattenedIndexSilent(x, y)];
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z)
        {
            return Storage[GetFlattenedIndexSilent(x, y, z)];
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int x, int y, int z, int[] indices)
        {
            return Storage[GetFlattenedIndexSilent(x, y, z, indices)];
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValueNoCheck(int[] indices)
        {
            return Storage[GetFlattenedIndexSilent(indices)];
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x)
        {
            Storage[GetFlattenedIndexSilent(x)] = value;
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y)
        {
            Storage[GetFlattenedIndexSilent(x, y)] = value;
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z)
        {
            Storage[GetFlattenedIndexSilent(x, y, z)] = value;
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int x, int y, int z, int[] other)
        {
            Storage[GetFlattenedIndexSilent(x, y, z, other)] = value;
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(T value, int[] indices)
        {
            Storage[GetFlattenedIndexSilent(indices)] = value;
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x)
        {
            Storage[GetFlattenedIndexSilent(x)] = valueCreator();
        }

        /// <summary>
        ///     Gets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y)
        {
            Storage[GetFlattenedIndexSilent(x, y)] = valueCreator();
        }

        /// <summary>
        ///     Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z)
        {
            Storage[GetFlattenedIndexSilent(x, y, z)] = valueCreator();
        }

        /// <summary>
        ///     Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int x, int y, int z, int[] indices)
        {
            Storage[GetFlattenedIndexSilent(x, y, z, indices)] = valueCreator();
        }

        /// <summary>
        ///     Sets the value without checking and without throwing an exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValueNoCheck(Func<T> valueCreator, int[] indices)
        {
            Storage[GetFlattenedIndexSilent(indices)] = valueCreator();
        }


        /// <summary>
        ///     This SubTensor is sequential SubTensor(int)
        ///     O(1)
        /// </summary>
        public Tensor<T> GetSubTensor(int[] indices)
        {
            return GetSubTensor(indices, 0);
        }

        internal Tensor<T> GetSubTensor(int[] indices, int id)
        {
            return id == indices.Length
                ? this
                : GetSubTensor(indices[id]).GetSubTensor(indices, id + 1);
        }

        /// <summary>
        ///     Get a SubTensor of a tensor
        ///     If you have a t = Tensor[2 x 3 x 4],
        ///     t.GetSubTensor(0) will return the proper matrix [3 x 4]
        ///     O(1)
        /// </summary>
        public Tensor<T> GetSubTensor(int index)
        {
            ReactIfBadAxesVol(index, 0);
            var newLinIndexDelta = GetFlattenedIndexSilent(index);
            var newShape = Size.SubTensorShape();
            var newBlocks = Stride.SubTensorShape();
            return new Tensor<T>(newShape, newBlocks, Storage)
            {
                Offset = newLinIndexDelta
            };
        }

        public void SetSubTensor(Tensor<T> subTensor, params int[] indices)
        {
            if (indices.Rank >= Size.Rank)
                throw new ArgumentException(
                    $"Number of {nameof(indices)} should be less than number of {nameof(Size)}");
            if (indices.Where((t, i) => t < 0 || t >= Size[i]).Any())
                throw new ArgumentException();
            if (Size.Rank - indices.Length != subTensor.Rank)
                throw new ArgumentException(
                    $"Number of {nameof(subTensor.Rank)} + {nameof(indices.Length)} should be equal to {Size.Rank}");


            var thisSub = GetSubTensor(indices);
            if (thisSub.Size != subTensor.Size)
                throw new InvalidShapeException($"{nameof(subTensor.Size)} must be equal to {nameof(Size)}");
            thisSub.Assign(subTensor);
        }

        internal void Assign(Tensor<T> subTensor)
        {
            foreach (var (index, value) in subTensor.Iterate())
                SetValueNoCheck(value, index);
        }


        public void Transpose(int axis1 = -1, int axis2 = -1)
        {
            if (!IsMatrix && !IsTensor)
            {
                throw new InvalidShapeException("this should be at least matrix");
            }

            if (axis1 == -1 && axis2 == -1)
            {
                Stride.Swap(Rank - 2, Rank - 1);
                Size.Swap(Rank - 2, Rank - 1);
               
            }
            else
            {
                Stride.Swap(axis1, axis2);
                Size.Swap(axis1, axis2);
               
            }
        }

        public Tensor<T> Stack(Tensor<T> tensor)
        {
            return TensorOps<T>.Stack(this, tensor);
        }
    }
}
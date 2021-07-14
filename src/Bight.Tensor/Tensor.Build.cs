using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Bight.Tensor.Exception;
using Bight.Tensor.Holder;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        private static (int height, int width) ExtractAndCheck(T[,] data)
        {
            var width = data.GetLength(0);

            if (width <= 0)
                throw new InvalidShapeException();

            var height = data.GetLength(1);

            if (height <= 0)
                throw new InvalidShapeException();

            return (width, height);
        }

        #region Build Matrix

        public static Tensor<T> BuildMatrix(T[,] data)
        {
            var (width, height) = ExtractAndCheck(data);
            var res = new Tensor<T>(width, height);
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                res.SetValueNoCheck(data[x, y], x, y);
            return res;
        }

        public static Tensor<T> BuildMatrix(int width, int height, Func<int, int, T> iniFunc)
        {
            var res = BuildMatrix(width, height);
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                res.SetValueNoCheck(iniFunc(x, y), x, y);
            return res;
        }

        public static Tensor<T> BuildMatrix(int width, int height)
        {
            return new Tensor<T>(width, height);
        }

        public static Tensor<T> BuildSquareMatrix(int diagLength)
        {
            return BuildMatrix(diagLength, diagLength);
        }

        #endregion

        #region Build Vector

        /// <summary>
        ///     Build Vector by Elements
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static Tensor<T> BuildVector(params T[] elements)
        {
            var tensor = new Tensor<T>(elements.Length);
            for (var i = 0; i < elements.Length; i++)
                tensor.SetValueNoCheck(elements[i], i);
            return tensor;
        }

        /// <summary>
        ///     Build Zeros Vector by Length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Tensor<T> BuildVector(int length)
        {
            var tensor = new Tensor<T>(length);
            return tensor;
        }

        /// <summary>
        ///     Build Vector according length, and pass a function return T
        /// </summary>
        /// <param name="length"></param>
        /// <param name="iniFunc"></param>
        /// <returns></returns>
        public static Tensor<T> BuildVector(int length, Func<int, T> iniFunc)
        {
            var tensor = new Tensor<T>(length);
            for (var i = 0; i < length; i++)
                tensor.SetValueNoCheck(iniFunc(i), i);
            return tensor;
        }

        #endregion

        #region Build Tensor

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> BuildTensor(T[] data)
        {
            var res = new Tensor<T>(data.GetLength(0));
            for (var x = 0; x < data.GetLength(0); x++)
                res.SetValueNoCheck(data[x], x);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> BuildTensor(T[,] data)
        {
            var res = new Tensor<T>(data.GetLength(0), data.GetLength(1));
            for (var x = 0; x < data.GetLength(0); x++)
            for (var y = 0; y < data.GetLength(1); y++)
                res.SetValueNoCheck(data[x, y], x, y);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> BuildTensor(T[,,] data)
        {
            var res = new Tensor<T>(data.GetLength(0),
                data.GetLength(1), data.GetLength(2));
            for (var x = 0; x < data.GetLength(0); x++)
            for (var y = 0; y < data.GetLength(1); y++)
            for (var z = 0; z < data.GetLength(2); z++)
                res.SetValueNoCheck(data[x, y, z], x, y, z);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tensor<T> BuildTensor(Array data)
        {
            var dimensions = new int[data.Rank];
            for (var i = 0; i < data.Rank; i++)
                dimensions[i] = data.GetLength(i);
            var res = new Tensor<T>(dimensions);

            dimensions = new int[data.Rank]; // Don't modify res
            var normalizedIndices = new int[data.Rank];
            var indices = new int[data.Rank];
            for (var i = 0; i < data.Rank; i++)
            {
                dimensions[i] = data.GetUpperBound(i);
                indices[i] = data.GetLowerBound(i);
            }

            var increment = indices.Length - 1;
            while (true)
            {
                for (var i = increment; indices[i] > dimensions[i]; i--)
                    if (i == 0)
                    {
                        return res;
                    }
                    else
                    {
                        indices[i - 1]++;
                        indices[i] = data.GetLowerBound(i);
                        normalizedIndices[i - 1]++;
                        normalizedIndices[i] = 0;
                    }

                res.SetValueNoCheck((T) data.GetValue(indices), normalizedIndices);
                indices[increment]++;
                normalizedIndices[increment]++;
            }
        }

        #endregion

        #region Build IdentityTensor

        public static Tensor<T> BuildIdentityTensor(int[] dimensions, int matrixDiag)
        {
            var dims = dimensions.ToList();
            dims.AddRange(new[] {matrixDiag, matrixDiag});
            var res = new Tensor<T>(dims.ToArray());
            foreach (var index in res.IterateOverMatrices())
            {
                var iden = BuildIdentityMatrix(matrixDiag);
                res.SetSubTensor(iden, index);
            }

            return res;
        }


        public static Tensor<T> BuildIdentityMatrix(int diag)
        {
            var wrapper = new Holder<T>();
            var tensor = BuildZeros(diag, diag);
            for (var i = 0; i < diag; i++)
                tensor.SetValueNoCheck(wrapper.One, i, i);
            return tensor;
        }

        #endregion

        #region Zeros

        public static Tensor<T> BuildZeros(params int[] shape)
        {
            return BuildZeros(new TensorSize(shape));
        }

        public static Tensor<T> BuildZeros(TensorSize size)
        {
            var wrapper = new Holder<T>();
            var tensor = new Tensor<T>(size)
            {
                Storage = Enumerable.Repeat(wrapper.Zero, size.Volume).ToArray()
            };
            return tensor;
        }

        #endregion

        #region Build Ones

        public static Tensor<T> BuildOnes(params int[] shape)
        {
            return BuildOnes(new TensorSize(shape));
        }

        public static Tensor<T> BuildOnes(TensorSize size)
        {
            var wrapper = new Holder<T>();
            var tensor = new Tensor<T>(size)
            {
                Storage = Enumerable.Repeat(wrapper.One, size.Volume).ToArray()
            };
            return tensor;
        }

        #endregion
    }
}
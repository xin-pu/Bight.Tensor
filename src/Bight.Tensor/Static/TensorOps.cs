using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Bight.Tensor.Exception;
using Bight.Tensor.Holder;

namespace Bight.Tensor.Static
{
    public static class TensorOps<T>
        where T : struct
    {
        public static Holder<T> Holder = new Holder<T>();

        /// <summary>
        ///     Creates a new axis that is put backward
        ///     and then sets all elements as children
        ///     e. g.
        ///     say you have a bunch of tensors {t1, t2, t3} with shape of [2 x 4]
        ///     Stack(t1, t2, t3) => T
        ///     where T is a tensor of shape of [3 x 2 x 4]
        ///     O(V)
        /// </summary>
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


        public static Tensor<T> Add(Tensor<T> t1, Tensor<T> t2)
        {
            ThrowExceptionIfBadSize(t1, t2);

            var newtensor = Tensor<T>.BuildZeros(t1);
            foreach (var (index, _) in newtensor.Iterate())
            {
                var addres = Holder.Add(t1[index], t2[index]);
                newtensor.SetValueNoCheck(addres, index);
            }

            return newtensor;
        }

        public static Tensor<T> AddN(params Tensor<T>[] tensors)
        {
            ThrowExceptionIfBadSize(tensors);
            var newtensor = Tensor<T>.BuildZeros(tensors[0]);
            foreach (var (index, _)in newtensor.Iterate().AsParallel())
            {
                var ini = Holder.Zero;
                foreach (var x1 in tensors.Select(a => a.GetValueNoCheck(index))) ini = Holder.Add(ini, x1);
                newtensor.SetValueNoCheck(ini, index);
            }

            return newtensor;
        }

        public static Tensor<T> ArgMax(Tensor<T> tensor)
        {
            throw new NotImplementedException();
        }

        public static Tensor<T> Round(Tensor<T> tensor)
        {
            if (tensor.DType == typeof(int))
                return tensor.Clone() as Tensor<T>;
            var newtensor = Tensor<T>.BuildZeros(tensor);
            foreach (var VARIABLE in newtensor.Iterate())
            {
                var value = double.Parse(tensor[VARIABLE.Index].ToString() ?? string.Empty);
                newtensor.SetValueNoCheck((T) (object) Math.Round(value), VARIABLE.Index);
            }

            return newtensor;
        }

        public static Tensor<T> Sqrt(Tensor<T> tensor)
        {
            var newtensor = Tensor<T>.BuildZeros(tensor);
            foreach (var VARIABLE in newtensor.Iterate())
            {
                var value = double.Parse(tensor[VARIABLE.Index].ToString() ?? string.Empty);
                newtensor.SetValueNoCheck((T) (object) Math.Sqrt(value), VARIABLE.Index);
            }

            return newtensor;
        }

        public static Tensor<T> Square(Tensor<T> tensor)
        {
            var newtensor = Tensor<T>.BuildZeros(tensor);
            foreach (var VARIABLE in newtensor.Iterate())
            {
                var value = double.Parse(tensor[VARIABLE.Index].ToString() ?? string.Empty);
                newtensor.SetValueNoCheck((T) (object) Math.Pow(value, 2), VARIABLE.Index);
            }

            return newtensor;
        }

        public static Tensor<T> Log10(Tensor<T> tensor)
        {
            var newtensor = Tensor<T>.BuildZeros(tensor);
            foreach (var VARIABLE in newtensor.Iterate())
            {
                var value = double.Parse(tensor[VARIABLE.Index].ToString() ?? string.Empty);
                newtensor.SetValueNoCheck((T) (object) Math.Log10(value), VARIABLE.Index);
            }

            return newtensor;
        }

        public static Tensor<T> Pow(Tensor<T> tensor, Tensor<T> exponent)
        {
            var newtensor = Tensor<T>.BuildZeros(tensor);
            foreach (var VARIABLE in newtensor.Iterate())
            {
                var value = double.Parse(tensor[VARIABLE.Index].ToString() ?? string.Empty);
                newtensor.SetValueNoCheck((T) (object) Math.Log10(value), VARIABLE.Index);
            }

            return newtensor;
        }

        private static void ThrowExceptionIfBadSize(Tensor<T> t1, Tensor<T> t2)
        {
            if (t1.Size != t2.Size || t1.Stride != t2.Stride)
                throw new InvalidShapeException("Need Same Size");
        }

        private static void ThrowExceptionIfBadSize(Tensor<T>[] t1)
        {
            var sizes = t1.Select(a => a.Size).Distinct();
            var strides = t1.Select(a => a.Stride).Distinct();
            if (sizes.Count() != 1 && strides.Count() != 1)
                throw new InvalidShapeException("Need Same Size");
        }
    }
}
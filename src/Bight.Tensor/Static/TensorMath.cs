using System.Linq;
using System.Threading.Tasks;
using Bight.Tensor.Exception;
using Bight.Tensor.Holder;

namespace Bight.Tensor.Static
{
    public partial class TensorMath<T>
        where T : struct
    {
        public static Holder<T> Holder = new Holder<T>();

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Abs(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Abs(value), index);
            return res;
        }


        public static Tensor<T> Negate(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Negate(value), index);
            return res;
        }

        internal static Tensor<T> Multiply(Tensor<T> a,
            Tensor<T> b)
        {
            if (!a.IsMatrix || !b.IsMatrix)
                throw new InvalidShapeException($"Both {nameof(a)} and {nameof(b)} should be matrices");
            if (a.Size[1] != b.Size[0])
                throw new InvalidShapeException($"{nameof(a)}'s height must be equal to {nameof(b)}'s width");

            var width = a.Size[0];
            var height = b.Size[1];
            var row = a.Size[1];
            var res = Tensor<T>.BuildMatrix(width, height);


            var aBlocks0 = a.Stride[0];
            var aBlocks1 = a.Stride[1];
            var bBlocks0 = b.Stride[0];
            var bBlocks1 = b.Stride[1];
            var aLinoffset = a.Offset;
            var bLinoffset = b.Offset;

            Parallel.For((long) 0, width, x =>
            {
                for (var y = 0; y < height; y++)
                {
                    var s = Holder.Zero;
                    for (var i = 0; i < row; i++)
                    {
                        var v1 = a.Storage[x * aBlocks0 + i * aBlocks1 + aLinoffset];
                        var v2 = b.Storage[i * bBlocks0 + y * bBlocks1 + bLinoffset];
                        s = Holder.Add(s, Holder.Multiply(v1, v2));
                    }

                    res.Storage[x * height + y] = s;
                }
            });

            return res;
        }

        public static Tensor<T> TensorMultiply(Tensor<T> a,
            Tensor<T> b)
        {
            if (a.Rank < 2 || b.Rank < 2)
                throw new InvalidShapeException(
                    $"Arguments should be at least matrices while their shapes are {a.Size} and {b.Size}");
            if (a.Size.SubShape(0, 2) != b.Size.SubShape(0, 2))
                throw new InvalidShapeException("Other dimensions of tensors should be equal");

            var oldShape = a.Size.SubShape(0, 2).ToArray();
            var newShape = new int[oldShape.Length + 2];
            for (var i = 0; i < oldShape.Length; i++)
                newShape[i] = oldShape[i];
            newShape[newShape.Length - 2] = a.Size[a.Size.Rank - 2];
            newShape[newShape.Length - 1] = b.Size[b.Size.Rank - 1];
            var resTensor = new Tensor<T>(newShape);


            var subdims = a.IterateOverCopy(2).ToArray();

            Parallel.For((long) 0, subdims.Length, subId =>
            {
                var subDimensions = subdims[subId];
                var product = Multiply(a.GetSubTensor(subDimensions), b.GetSubTensor(subDimensions));
                resTensor.SetSubTensor(product, subDimensions);
            });
            return resTensor;
        }
    }
}
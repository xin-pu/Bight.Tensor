using System.Linq;
using Bight.Tensor.Wrap;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        public static Tensor<T> BuildIdentityTensor(int[] dimensions, int matixDiag)
        {
            var dim = dimensions.ToList();
            dim.AddRange(new[] {matixDiag, matixDiag});
            var tensor = new Tensor<T>(dim.ToArray());
            return tensor;
        }


        public static Tensor<T> BuildIdentityMatrix(int diag)
        {
            var tensor = BuildZeros(diag, diag);
            return tensor;
        }

        public static Tensor<T> BuildOnes(params int[] shape)
        {
            return BuildOnes(new TensorShape(shape));
        }

        public static Tensor<T> BuildOnes(TensorShape shape)
        {
            var wrapper = new Wrapper<T>();
            var tensor = new Tensor<T>(shape)
            {
                Data = Enumerable.Repeat(wrapper.One, shape.Volume).ToArray()
            };
            return tensor;
        }

        public static Tensor<T> BuildZeros(params int[] shape)
        {
            return BuildZeros(new TensorShape(shape));
        }

        public static Tensor<T> BuildZeros(TensorShape shape)
        {
            var wrapper = new Wrapper<T>();
            var tensor = new Tensor<T>(shape)
            {
                Data = Enumerable.Repeat(wrapper.Zero, shape.Volume).ToArray()
            };
            return tensor;
        }
    }
}
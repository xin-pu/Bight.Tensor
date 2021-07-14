using Bight.Tensor.Static;

namespace Bight.Tensor
{
    public partial class Tensor<T>
        where T : struct
    {
        public Tensor<T> Cos()
        {
            return TensorMath<T>.Cos(this);
        }


        public Tensor<T> Sin()
        {
            return TensorMath<T>.Sin(this);
        }

        public Tensor<T> Tan()
        {
            return TensorMath<T>.Tan(this);
        }
    }
}
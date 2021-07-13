namespace Bight.Tensor.Matrix
{
    public class Matrix<T> : Tensor<T>
        where T : struct
    {
        public Matrix(int width, int height)
            : base(width, height)
        {
        }
    }
}
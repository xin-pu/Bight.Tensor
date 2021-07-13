namespace Bight.Tensor.Vector
{
    public class Vector<T> : Tensor<T>
        where T : struct
    {
        public Vector(int dims)
            : base(dims)
        {
        }
    }
}
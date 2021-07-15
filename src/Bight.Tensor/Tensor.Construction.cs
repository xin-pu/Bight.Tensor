namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        public Tensor(params int[] shape)
            : this(new TensorSize(shape))
        {
        }


        public Tensor(TensorSize size)
        {
            Size = size;
            Storage = new T[Size.Volume];
            Stride = GetDefaultlStride();
        }

        public Tensor(T scalar)
        {
            Size = new TensorSize(1);
            Storage = new[] {scalar};
            Stride = GetDefaultlStride();
        }

        public Tensor(T[] vector)
        {
            Size = new TensorSize(vector.Length);
            Storage = BuildTensor(vector).Storage;
            Stride = GetDefaultlStride();
        }

        public Tensor(T[,] matrix)
        {
            Size = new TensorSize(matrix.GetLength(0), matrix.GetLength(1));
            Storage = BuildTensor(matrix).Storage;
            Stride = GetDefaultlStride();
        }

        public Tensor(T[,,] tensor)
        {
            Size = new TensorSize(tensor.GetLength(0), tensor.GetLength(1), tensor.GetLength(2));
            Storage = BuildTensor(tensor).Storage;
            Stride = GetDefaultlStride();
        }

        private Tensor(TensorSize size, TensorSize stride, T[] storage)
        {
            Size = size;
            Storage = storage;
            Stride = stride;
        }
    }
}
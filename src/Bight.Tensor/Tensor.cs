using System;
using System.Linq;
using Bight.Tensor.Holder;
using YAXLib;

namespace Bight.Tensor
{
    /// <summary>
    ///     A PyTorch Tensor is a view over such a Storage that’s capable of indexing into that storage by using an offset and
    ///     per-dimension strides.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Tensor<T> : ICloneable
        where T : struct
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

        public Tensor(TensorSize size, TensorSize stride, T[] storage)
        {
            Size = size;
            Storage = storage;
            Stride = stride;
        }

        internal Type DType => typeof(T);


        /// <summary>
        ///     A storage is a one-dimensional array of numerical data
        ///     such as a contiguous block of memory containing numbers of
        ///     a given type, perhaps a float or int32.
        /// </summary>
        public T[] Storage { set; get; }

        public TensorSize Size { set; get; }

        public TensorSize Stride { set; get; }

        internal Holder<T> Holder => new Holder<T>();

        /// <summary>
        ///     Number of elements in tensor overall
        ///     a tensor [2,3,4] will get volume 2*3*4
        /// </summary>
        public int Volume => Size.Volume;

        /// <summary>
        ///     Rank of tensor
        ///     a tensor [2,3,4] will get rank 3
        /// </summary>
        public int Rank => Size.Rank;

        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }


        private TensorSize GetDefaultlStride()
        {
            var shapeRve = Size.shape.Append(1).Reverse();
            var blockShape = Enumerable.Range(1, Size.Rank)
                .Select(r =>
                    shapeRve.Take(r).Aggregate((a, b) => a * b))
                .Reverse()
                .ToArray();
            return new TensorSize(blockShape);
        }

        private bool GetContiguous()
        {
            return GetDefaultlStride().Equals(Stride);
        }
    }
}
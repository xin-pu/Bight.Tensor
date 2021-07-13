using System;
using System.Linq;
using Bight.Tensor.Wrap;
using YAXLib;

namespace Bight.Tensor
{
    /// <summary>
    ///     If more than 3 Rank you enter.
    ///     For example [2,3,4] Shape you input
    ///     You will get 2 Matrix with Matrix size [3,4]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Tensor<T> : ICloneable
        where T : struct
    {
        public Tensor(params int[] shape)
            : this(new TensorShape(shape))
        {
        }


        public Tensor(TensorShape shape)
        {
            Shape = shape;
            DataResume();
        }

        private Tensor(TensorShape shape, T[] data)
        {
            Shape = shape;
            Data = data;
        }

        public string TType => typeof(T).Name;

        public string Name { protected set; get; }

        public T[] Data { set; get; }

        public TensorShape Shape { set; get; }

        internal TensorShape BlockShape => GetBlockShape();

        internal Wrapper<T> Wrapper => new Wrapper<T>();

        /// <summary>
        ///     Number of elements in tensor overall
        ///     a tensor [2,3,4] will get volume 2*3*4
        /// </summary>
        public int Volume => Shape.Volume;

        /// <summary>
        ///     Rank of tensor
        ///     a tensor [2,3,4] will get rank 3
        /// </summary>
        public int Rank => Shape.Rank;

        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }


        private void DataResume()
        {
            var data = new T[Shape.Volume];
            Data = data;
        }


        private TensorShape GetBlockShape()
        {
            var shapeRve = Shape.shape.Append(1).Reverse();
            var blockShape = Enumerable.Range(1, Shape.Rank)
                .Select(r =>
                    shapeRve.Take(r).Aggregate((a, b) => a * b))
                .Reverse()
                .ToArray();
            return new TensorShape(blockShape);
        }
    }
}
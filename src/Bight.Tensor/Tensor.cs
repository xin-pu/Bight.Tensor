using System;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YAXLib;

namespace Bight.Tensor
{
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

        public string TType => typeof(T).Name;

        public string Name { protected set; get; }

        public T[] Data { set; get; }

        public TensorShape Shape { set; get; }

        internal TensorShape BlockShape => GetBlockShape();

        public int Volume => Shape.Volume;

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
            var shapeRve = Shape.Reverse().shape;
            var blockShape = Enumerable.Range(1, Shape.Rank)
                .Select(r =>
                    shapeRve.Take(r).Aggregate((a, b) => a * b))
                .ToArray();
            return new TensorShape(blockShape);
        }

        public override string ToString()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            return serializer.Serialize(this);
        }
    }
}
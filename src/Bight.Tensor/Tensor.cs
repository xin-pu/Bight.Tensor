using System;
using System.Linq;
using YAXLib;

namespace Bight.Tensor
{
    public class Tensor<T> : ICloneable
        where T : struct
    {
        private readonly int[] blocks;

        public Tensor(TShape shape)
        {
            Shape = shape;
            var len = 1;
            for (var i = 0; i < shape.Length; i++) len *= shape[i];
            var data = new T[len];
            Data = data;
            blocks = new int[shape.Length];
            BlockRecompute();
        }


        public T[] Data { protected set; get; }

        public TShape Shape { protected set; get; }

        public TShape BlockShape { protected set; get; }

        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }


        private void BlockRecompute()
        {
            var len = 1;
            foreach (var i in Enumerable.Range(0, Shape.Length))
            {
                blocks[i] = len;
                len *= Shape[i];
            }
        }

        private void BlockShapeResume()
        {
        }
    }
}
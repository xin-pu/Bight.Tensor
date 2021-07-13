using System;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YAXLib;

namespace Bight.Tensor
{
    public class TensorShape : IEquatable<TensorShape>, ICloneable
    {
        /// <summary>
        ///     Create a TensorShape for further operations
        ///     just listing necessary dimensions
        /// </summary>
        /// <param name="shape"></param>
        public TensorShape(params int[] shape)
        {
            this.shape = shape;
        }

        /// <summary>
        ///     Internal variable. Not recommended to change
        /// </summary>
        public int[] shape { set; get; }

        public int Rank => shape.Length;

        public int Volume => GetVolume();

        /// <summary>
        ///     You can only read some dimensions,
        ///     otherwise it will cause unintended behaviour
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public int this[int axisId] => shape[axisId];

        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }

        public bool Equals(TensorShape other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.Rank != Rank) return false;
            for (var i = 0; i < Rank; i++)
                if (shape[i] != other.shape[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TensorShape) obj);
        }


        internal TensorShape CutEnd()
        {
            var newShape = new int[Rank - 1];
            for (var i = 0; i < newShape.Length; i++)
                newShape[i] = shape[i + 1];
            return new TensorShape(newShape);
        }

        internal void Swap(int id1, int id2)
        {
            (shape[id1], shape[id2]) = (shape[id2], shape[id1]);
        }

        internal int GetVolume()
        {
            return Enumerable.Range(0, Rank).Aggregate(1, (current, i) => current * this[i]);
        }

        /// <summary>
        ///     Gets a sub shape as a subsequence with the given
        ///     left and right offsets
        /// </summary>
        public TensorShape SubShape(int offsetFromLeft, int offsetFromRight)
        {
            var newShape = new int[Rank - offsetFromLeft - offsetFromRight];
            for (var i = offsetFromLeft; i < Rank - offsetFromRight; i++)
                newShape[i - offsetFromLeft] = shape[i];
            return new TensorShape(newShape);
        }

        /// <summary>
        ///     Reverse TShape
        /// </summary>
        /// <returns></returns>
        public TensorShape Reverse()
        {
            return new TensorShape(shape.Reverse().ToArray());
        }

        /// <summary>
        ///     Returns the shape's internal array's copy
        /// </summary>
        public int[] ToArray()
        {
            return shape;
        }


        public static bool operator ==(TensorShape s1, TensorShape s2)
        {
            return s1 is { } && s1.Equals(s2);
        }

        public static bool operator !=(TensorShape s1, TensorShape s2)
        {
            return s1 is { } && !s1.Equals(s2);
        }


        public override int GetHashCode()
        {
            return shape != null ? shape.GetHashCode() : 0;
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
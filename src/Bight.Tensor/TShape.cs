using System;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YAXLib;

namespace Bight.Tensor
{
    public struct TShape : IEquatable<TShape>, ICloneable
    {
        /// <summary>
        ///     Internal variable. Not recommended to change
        /// </summary>
        public int[] shape { set; get; }

        /// <summary>
        ///     Create a TensorShape for further operations
        ///     just listing necessary dimensions
        /// </summary>
        /// <param name="shape"></param>
        public TShape(params int[] shape)
        {
            this.shape = shape;
        }

        public int Length => shape.Length;

        /// <summary>
        ///     Synonym for Length
        /// </summary>
        public int Dimension => shape.Length;


        /// <summary>
        ///     You can only read some dimensions,
        ///     otherwise it will cause unintended behaviour
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public int this[int axisId] => shape[axisId];


        internal TShape CutEnd()
        {
            var newShape = new int[Length - 1];
            for (var i = 0; i < newShape.Length; i++)
                newShape[i] = shape[i + 1];
            return new TShape(newShape);
        }

        internal void Swap(int id1, int id2)
        {
            (shape[id1], shape[id2]) = (shape[id2], shape[id1]);
        }


        /// <summary>
        ///     Gets a sub shape as a subsequence with the given
        ///     left and right offsets
        /// </summary>
        public TShape SubShape(int offsetFromLeft, int offsetFromRight)
        {
            var newShape = new int[Length - offsetFromLeft - offsetFromRight];
            for (var i = offsetFromLeft; i < Length - offsetFromRight; i++)
                newShape[i - offsetFromLeft] = shape[i];
            return new TShape(newShape);
        }

        /// <summary>
        ///     Reverse TShape
        /// </summary>
        /// <returns></returns>
        public TShape Reverse()
        {
            return new TShape(shape.Reverse().ToArray());
        }

        /// <summary>
        ///     Returns the shape's internal array's copy
        /// </summary>
        public int[] ToArray()
        {
            return shape;
        }

        public bool Equals(TShape other)
        {
            var len1 = shape.Length;
            var len2 = other.shape.Length;
            if (len1 != len2) return false;
            for (var i = 0; i < len1; i++)
                if (shape[i] != other.shape[i])
                    return false;
            return true;
        }

        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            return Equals((TShape) obj);
        }

        public static bool operator ==(TShape s1, TShape s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(TShape s1, TShape s2)
        {
            return !s1.Equals(s2);
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
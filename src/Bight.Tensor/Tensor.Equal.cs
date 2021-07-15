using System;

namespace Bight.Tensor
{
    public partial class Tensor<T> : IEquatable<Tensor<T>>
    {
        public bool Equals(Tensor<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Storage, other.Storage) &&
                   Equals(Size, other.Size) &&
                   Equals(Stride, other.Stride) &&
                   Offset == other.Offset;
        }

        public override int GetHashCode()
        {
            var hash = Storage.GetHashCode();
            hash ^= Size.GetHashCode();
            hash ^= Storage.GetHashCode();
            hash ^= Size.GetHashCode();
            return hash;
        }

        private static bool CompareIsSame(Tensor<T> s1, Tensor<T> s2)
        {
            var res = s1.Storage == s2.Storage;
            res = res && s1.Stride == s2.Stride;
            return res;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tensor<T>) obj);
        }


        public static bool operator ==(Tensor<T> s1, Tensor<T> s2)
        {
            return s1 is { } && CompareIsSame(s1, s2);
        }

        public static bool operator !=(Tensor<T> s1, Tensor<T> s2)
        {
            return s1 is { } && !CompareIsSame(s1, s2);
        }
    }
}
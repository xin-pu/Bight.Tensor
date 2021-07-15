using System.Linq;
using Bight.Tensor.Static;

namespace Bight.Tensor
{
    /// <summary>
    ///     all these function change self
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Tensor<T>
    {
        public void Zero()
        {
            foreach (var (index, _) in Iterate())
                SetValueNoCheck(Holder.Zero, index);
        }

        public void One()
        {
            foreach (var (index, _) in Iterate())
                SetValueNoCheck(Holder.One, index);
        }

        public void All(T t)
        {
            foreach (var (index, _) in Iterate())
                SetValueNoCheck(t, index);
        }

        public void Abs()
        {
            foreach (var (index, value) in Iterate())
                SetValueNoCheck(Holder.Abs(value), index);
        }

        public void Negate()
        {
            foreach (var (index, value) in Iterate())
                SetValueNoCheck(Holder.Negate(value), index);
        }


        public T Max()
        {
            return Storage.Max();
        }

        public T Min()
        {
            return Storage.Min();
        }

        public T Sum()
        {
            return TensorMath<T>.Sum(this);
        }

        public T Mean()
        {
            return TensorMath<T>.Sum(this);
        }
    }
}
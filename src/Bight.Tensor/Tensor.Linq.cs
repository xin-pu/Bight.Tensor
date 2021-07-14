namespace Bight.Tensor
{
    /// <summary>
    ///     all these function change self
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Tensor<T>
    {
        public void Empty()
        {
            foreach (var (index, value) in Iterate())
                SetValueNoCheck(Holder.Zero, index);
        }

        public void One()
        {
            foreach (var (index, value) in Iterate())
                SetValueNoCheck(Holder.One, index);
        }

        public void All(T t)
        {
            foreach (var (index, value) in Iterate())
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
    }
}
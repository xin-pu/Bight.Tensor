namespace Bight.Tensor.Static
{
    public partial class TensorMath<T>
        where T : struct
    {
        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Acos(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Acos(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Asin(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Asin(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Atan(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Atan(value), index);
            return res;
        }


        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Cos(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Cos(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Cosh(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Cosh(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Sin(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Sin(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Sinh(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Sinh(value), index);
            return res;
        }

        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Tan(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Tan(value), index);
            return res;
        }


        /// <summary>
        ///     Return a new Tensor
        /// </summary>
        /// <param name="tensor"></param>
        /// <returns></returns>
        public static Tensor<T> Tanh(Tensor<T> tensor)
        {
            var res = new Tensor<T>(tensor.Size);
            foreach (var (index, value) in tensor.Iterate())
                res.SetValueNoCheck(Holder.Tanh(value), index);
            return res;
        }
    }
}
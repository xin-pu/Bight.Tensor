using System;

namespace Bight.Tensor
{
    public partial class Tensor<T> : IDisposable
    {
        public void Dispose()
        {
            Storage = null;
        }
    }
}
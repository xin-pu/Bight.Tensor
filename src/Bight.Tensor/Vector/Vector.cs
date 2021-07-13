using System;

namespace Bight.Tensor
{
    public class Vector<T> : ICloneable
        where T : struct
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
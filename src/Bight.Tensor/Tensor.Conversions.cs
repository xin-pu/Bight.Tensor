using System;
using System.Collections.Generic;
using System.Linq;
using YAXLib;

namespace Bight.Tensor
{
    public partial class Tensor<T> : ICloneable
    {
        /// <summary>
        ///     Copy to new Object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var serializer = new YAXSerializer(GetType());
            var res = serializer.Serialize(this);
            return serializer.Deserialize(res);
        }

        /// <summary>
        ///     Copy to List
        /// </summary>
        /// <returns></returns>
        public IList<T> ToScalars()
        {
            return IterateOverScalars().Select(GetValueNoCheck).ToList();
        }


        public IList<Tensor<T>> ToVectors()
        {
            return IterateOverVectors().Select(a => BuildVector(a)).ToList();
        }


        ///Todo Convert to GPU Array
    }
}
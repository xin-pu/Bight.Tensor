using System.Collections.Generic;
using System.Linq;

namespace Bight.Tensor
{
    /// <summary>
    ///     Iterate.cs Make it easy to Iterate Tensor by Element, Vector, or Matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Tensor<T>
    {
        /// Iterate over array of indices and a value in TPrimitive
        /// </summary>
        public IEnumerable<(int[] Index, T Value)> Iterate()
        {
            foreach (var ind in IterateOver(0))
                yield return (ind, GetValueNoCheck(ind));
        }


        /// <summary>
        ///     IterateOver where yourTensor[index] is always an element
        /// </summary>
        public IEnumerable<int[]> IterateOverScalars()
        {
            return IterateOver(0);
        }

        /// <summary>
        ///     IterateOver where yourTensor[index] is always a vector
        /// </summary>
        public IEnumerable<int[]> IterateOverVectors()
        {
            return IterateOver(1);
        }

        /// <summary>
        ///     IterateOver where yourTensor[index] is always a matrix
        /// </summary>
        public IEnumerable<int[]> IterateOverMatrices()
        {
            return IterateOver(2);
        }


        private void NextIndex(int[] indices, int id)
        {
            if (id == -1)
                return;
            indices[id]++;
            if (indices[id] != Size[id]) return;
            indices[id] = 0;
            NextIndex(indices, id - 1);
        }

        /// <summary>
        ///     Allows to iterate on lower-dimensions,
        ///     so that, for example, in tensor of [2 x 3 x 4]
        ///     and offsetFromLeft = 1
        ///     while iterating you will get the following arrays:
        ///     {0, 0}
        ///     {0, 1}
        ///     {0, 2}
        ///     {1, 0}
        ///     {1, 1}
        ///     {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        private IEnumerable<int[]> IterateOver(int offsetFromLeft)
        {
            static bool SumIsNot0(int[] arr)
            {
                return arr.Any(a => a != 0);
            }

            var indices = new int[Size.Rank - offsetFromLeft];
            do
            {
                yield return indices;
                NextIndex(indices, indices.Length - 1);
            } while (SumIsNot0(indices));
            // for tensor 4 x 3 x 2 the first violating index would be 5  0  0 
        }

        /// <summary>
        ///     Allows to iterate on lower-dimensions,
        ///     so that, for example, in tensor of [2 x 3 x 4]
        ///     and offsetFromLeft = 1
        ///     while iterating you will get the following arrays:
        ///     {0, 0}
        ///     {0, 1}
        ///     {0, 2}
        ///     {1, 0}
        ///     {1, 1}
        ///     {1, 2}
        /// </summary>
        /// <param name="offsetFromLeft"></param>
        /// <returns></returns>
        public IEnumerable<int[]> IterateOverCopy(int offsetFromLeft)
        {
            return IterateOver(offsetFromLeft).Select(inds => inds.ToArray());
        }
    }
}
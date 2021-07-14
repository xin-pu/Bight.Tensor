using System.Collections.Generic;
using System.Linq;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        private void NextIndex(int[] indices, int id)
        {
            if (id == -1)
                return;
            indices[id]++;
            if (indices[id] == Size[id])
            {
                indices[id] = 0;
                NextIndex(indices, id - 1);
            }
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
        public IEnumerable<int[]> IterateOver(int offsetFromLeft)
        {
            static bool SumIsNot0(int[] arr)
            {
                foreach (var a in arr)
                    if (a != 0)
                        return true;
                return false;
            }

            var indices = new int[Size.Rank - offsetFromLeft];
            do
            {
                yield return indices;
                NextIndex(indices, indices.Length - 1);
            } while (SumIsNot0(indices)); // for tensor 4 x 3 x 2 the first violating index would be 5  0  0 
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
            foreach (var inds in IterateOver(offsetFromLeft))
                yield return inds.ToArray();
        }


        /// <summary>
        ///     IterateOver where yourTensor[index] is always a matrix
        /// </summary>
        public IEnumerable<int[]> IterateOverMatrices()
        {
            return IterateOver(2);
        }

        /// <summary>
        ///     IterateOver where yourTensor[index] is always a vector
        /// </summary>
        public IEnumerable<int[]> IterateOverVectors()
        {
            return IterateOver(1);
        }

        /// <summary>
        ///     IterateOver where yourTensor[index] is always an element
        /// </summary>
        public IEnumerable<int[]> IterateOverElements()
        {
            return IterateOver(0);
        }
    }
}
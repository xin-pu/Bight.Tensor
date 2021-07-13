namespace Bight.Tensor.Core
{
    //public static class Build<T>
    //    where T : struct

    //{
    //    /// <summary>
    //    ///     Creates a tensor whose all matrices are identity matrices
    //    /// </summary>
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static Tensor<T> CreateIdentityTensor(int[] dimensions, int finalMatrixDiag)
    //    {
    //        var newDims = new int[dimensions.Length + 2];
    //        for (var i = 0; i < dimensions.Length; i++)
    //            newDims[i] = dimensions[i];
    //        newDims[newDims.Length - 2] = newDims[newDims.Length - 1] = finalMatrixDiag;
    //        var res = new Tensor<T>(newDims);
    //        //foreach (var index in res.IterateOverMatrices())
    //        //{
    //        //    var iden = CreateIdentityMatrix(finalMatrixDiag);
    //        //    res.SetSubtensor(iden, index);
    //        //}

    //        return res;
    //    }
    //}
}
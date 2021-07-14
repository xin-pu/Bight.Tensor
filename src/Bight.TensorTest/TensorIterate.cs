using System.Collections.Generic;
using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorIterate
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Tensor<double> tensor = Tensor<double>.BuildOnes(2, 3, 4);

        public TensorIterate(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private void PrintIEnumInt(IEnumerable<int[]> datas)
        {
            foreach (var data in datas) _testOutputHelper.WriteLine("[" + $"{string.Join(",", data)}" + "]");
        }


        [Fact]
        public void TestIterateElements()
        {
            var res = tensor.IterateOverScalars();
            PrintIEnumInt(res);
        }

        [Fact]
        public void TestIterateVectors()
        {
            var res = tensor.IterateOverVectors();
            PrintIEnumInt(res);
        }

        [Fact]
        public void TestIterateMatrixs()
        {
            var res = tensor.IterateOverMatrices();
            PrintIEnumInt(res);
        }
    }
}
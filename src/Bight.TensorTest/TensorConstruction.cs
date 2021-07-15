using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorConstruction
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TensorConstruction(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestCons()
        {
            var scalar = new Tensor<double>(-2F);
            var vector = new Tensor<double>(new double[] {2, 3});
            var matrix = new Tensor<double>(new double[2, 3]);
            var tensor = new Tensor<double>(new double[2, 3, 4]);
            _testOutputHelper.WriteLine(scalar.ToString());
            _testOutputHelper.WriteLine(vector.ToString());
            _testOutputHelper.WriteLine(matrix.ToString());
            _testOutputHelper.WriteLine(tensor.ToString());
        }
    }
}
using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorToString
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TensorToString(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void VectorToString()
        {
            var vector = new Tensor<double>(4);
            _testOutputHelper.WriteLine(vector.ToString());
        }

        [Fact]
        public void MatrixToString()
        {
            var matrix = new Tensor<double>(3, 4) {[0, 1] = 12};
            _testOutputHelper.WriteLine(matrix.ToString());
        }

        [Fact]
        public void DenseTensorToString()
        {
            var tensor = new Tensor<double>(2, 3, 4) {[0, 1, 3] = 12};
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void DeepTensorToString()
        {
            var tensor = new Tensor<double>(2, 2, 3, 4) {[1, 1, 2, 2] = 11};
            _testOutputHelper.WriteLine(tensor.ToString());
        }
    }
}
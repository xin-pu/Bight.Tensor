using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorBuild
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TensorBuild(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void BuildVector()
        {
            var vector1 = Tensor<double>.BuildVector(1, 2, 3);
            var vector2 = Tensor<double>.BuildVector(3);
            var vector3 = Tensor<double>.BuildVector(5, a => 1.2F);
            _testOutputHelper.WriteLine(vector1.ToString());
            _testOutputHelper.WriteLine(vector2.ToString());
            _testOutputHelper.WriteLine(vector3.ToString());
        }


        [Fact]
        public void BuildMatrix()
        {
            var matrix1 = Tensor<double>.BuildMatrix(3, 4);
            var matrix2 = Tensor<double>.BuildMatrix(new double[3, 4]);
            var matrix3 = Tensor<double>.BuildMatrix(3, 4, (i, j) => 1.3F);

            _testOutputHelper.WriteLine(matrix1.ToString());
            _testOutputHelper.WriteLine(matrix2.ToString());
            _testOutputHelper.WriteLine(matrix3.ToString());
        }

        [Fact]
        public void BuildSquareMatrix()
        {
            var matrix1 = Tensor<double>.BuildSquareMatrix(3);
            _testOutputHelper.WriteLine(matrix1.ToString());
        }

        [Fact]
        public void CreateOnes()
        {
            var a = Tensor<double>.BuildOnes(20, 20);
            _testOutputHelper.WriteLine(a.ToString());
        }


        [Fact]
        public void TestBuild()
        {
            var ones = Tensor<double>.BuildOnes(2, 3, 4);
            var zeros = Tensor<double>.BuildZeros(2, 3, 4);
        }
    }
}
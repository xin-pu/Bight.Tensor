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
        public void BuildTensor()
        {
            var tensorVector = Tensor<double>.BuildTensor(new double[3]);
            var tensorMatrix = Tensor<double>.BuildTensor(new double[3, 4]);
            var tensorTensor = Tensor<double>.BuildTensor(new double[3, 4, 5]);
            _testOutputHelper.WriteLine(tensorVector.ToString());
            _testOutputHelper.WriteLine(tensorMatrix.ToString());
            _testOutputHelper.WriteLine(tensorTensor.ToString());

            var tensor1D = Tensor<double>.BuildTensor(new double[3]);
            var tensor2D = Tensor<double>.BuildTensor(new double[,] {{1, 2, 3}, {2, 3, 4}});
            var tensor3D = Tensor<double>.BuildTensor(new double[,,]
            {
                {{1, 2, 3}, {2, 3, 4}},
                {{1, 2, 3}, {2, 3, 4}}
            });
            _testOutputHelper.WriteLine(tensor1D.ToString());
            _testOutputHelper.WriteLine(tensor2D.ToString());
            _testOutputHelper.WriteLine(tensor3D.ToString());
        }


        [Fact]
        public void BuildOnes()
        {
            var a = Tensor<double>.BuildOnes(5, 5);
            _testOutputHelper.WriteLine(a.ToString());
        }


        [Fact]
        public void BuildZeros()
        {
            var zeros = Tensor<double>.BuildZeros(2, 3, 4);
            _testOutputHelper.WriteLine(zeros.ToString());
        }

        [Fact]
        public void BuildIdentityMatrix()
        {
            var identityMatrix = Tensor<double>.BuildIdentityMatrix(3);
            _testOutputHelper.WriteLine(identityMatrix.ToString());
        }
    }
}
using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensor
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private Tensor<double> tensor;

        public TestTensor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            tensor = new Tensor<double>(new TensorShape(2, 3, 4));
        }


        [Fact]
        public void CreateScale()
        {
            var vector = new Tensor<double>(3);
            _testOutputHelper.WriteLine(vector.ToString());
        }

        [Fact]
        public void CreateMatrix()
        {
            var matrix = new Tensor<double>(3, 4);
            _testOutputHelper.WriteLine(matrix.ToString());
        }

        [Fact]
        public void GetSubTensor()
        {
            var matrix = new Tensor<double>(3, 4) {[0, 1] = 12};
            var vector = matrix.GetSubTensor(0);
            _testOutputHelper.WriteLine(vector.ToString());
        }


        [Fact]
        public void TestBuild()
        {
            tensor = Tensor<double>.BuildOnes(2, 3, 4);
            tensor = Tensor<double>.BuildZeros(2, 3, 4);
        }


        [Fact]
        public void TestThis()
        {
            tensor = Tensor<double>.BuildZeros(2, 3, 4);
            tensor[0, 0, 0] = 3F;
            tensor[0, 1, 0] = 4F;
            tensor[1, 0, 0] = 5F;
            _testOutputHelper.WriteLine(tensor.ToString());
            _testOutputHelper.WriteLine(tensor[0, 1, 0].ToString());
        }


        [Fact]
        public void TestSub()
        {
            var t = Tensor<double>.BuildZeros(2, 3, 4);
            t[0, 0, 0] = 3F;
            t[0, 1, 0] = 4F;
            t[1, 0, 0] = 5F;
            var subT = t.GetSubTensor(0);
            _testOutputHelper.WriteLine(t.ToString());
            _testOutputHelper.WriteLine(subT.ToString());
        }
    }
}
using Bight.Tensor;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensor
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Tensor<double> matrix;
        private readonly Tensor<double> vector;
        private readonly Tensor<double> tensor;

        public TestTensor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            vector = new Tensor<double>(new TensorShape(3));
            matrix = new Tensor<double>(new TensorShape(3, 3));
            tensor = new Tensor<double>(new TensorShape(2, 3, 4));
        }

        [Fact]
        public void CheckIsVector()
        {
            vector.IsVector.Should().BeTrue();
            matrix.IsVector.Should().BeFalse();
            tensor.IsVector.Should().BeFalse();
        }

        [Fact]
        public void CheckIsMatrix()
        {
            vector.IsMatrix.Should().BeFalse();
            matrix.IsMatrix.Should().BeTrue();
            tensor.IsMatrix.Should().BeFalse();
        }

        [Fact]
        public void CheckIsTensor()
        {
            vector.IsTensor.Should().BeFalse();
            matrix.IsTensor.Should().BeFalse();
            tensor.IsTensor.Should().BeTrue();
        }

        [Fact]
        public void CheckIsSquareMatrix()
        {
            vector.IsSquareMatrix.Should().BeFalse();
            matrix.IsSquareMatrix.Should().BeTrue();
            tensor.IsSquareMatrix.Should().BeFalse();
        }


        [Fact]
        public void TestGetSubTensor()
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
using Bight.Tensor;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensorClone
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public Tensor<double> OnesTensor = Tensor<double>.BuildOnes(3, 4);

        public TestTensorClone(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void TestClone()
        {
            _testOutputHelper.WriteLine(OnesTensor.ToString());
            var cloneTensor = OnesTensor.Clone();
            _testOutputHelper.WriteLine(cloneTensor.ToString());
            cloneTensor.Should().BeEquivalentTo(OnesTensor);
            cloneTensor.Should().NotBe(OnesTensor);
        }
    }
}
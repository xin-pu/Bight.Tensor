using Bight.Tensor;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorConversions
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public Tensor<double> OnesTensor = Tensor<double>.BuildOnes(3, 4);

        public TensorConversions(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void TestClone()
        {
            _testOutputHelper.WriteLine(OnesTensor.ToString());
            var cloneTensor = OnesTensor.Clone() as Tensor<double>;
            _testOutputHelper.WriteLine(cloneTensor?.ToString());
            var res = cloneTensor != OnesTensor;
            res.Should().BeTrue();
            cloneTensor.Should().NotBe(OnesTensor);
        }

        [Fact]
        public void TestToScalar()
        {
            var scalars = OnesTensor.ToScalars();
            _testOutputHelper.WriteLine(string.Join(",", scalars));
        }
    }
}
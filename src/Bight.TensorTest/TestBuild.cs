using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestBuild
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestBuild(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CreateOnes()
        {
            var a = Tensor<double>.BuildOnes(20, 20);
            _testOutputHelper.WriteLine(a.ToString());
        }
    }
}
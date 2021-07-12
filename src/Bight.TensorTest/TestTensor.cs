using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensor
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TestTensor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestCreate()
        {
            var t = new Tensor<double>(new TShape(3, 3));
            _testOutputHelper.WriteLine(t.ToString());
        }
    }
}
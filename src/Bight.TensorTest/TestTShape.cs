using Bight.Tensor;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTShape
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public TShape TShape = new TShape(1, 2, 3);

        public TestTShape(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestPrintTShape()
        {
            _testOutputHelper.WriteLine(TShape.ToString());
            _testOutputHelper.WriteLine(TShape.Length.ToString());
            _testOutputHelper.WriteLine(TShape.Dimension.ToString());
            _testOutputHelper.WriteLine(TShape[0].ToString());
        }


        [Fact]
        public void TestClone()
        {
            var clone = TShape.Clone();
            _testOutputHelper.WriteLine(clone.ToString());
        }

        [Fact]
        public void TestFunction()
        {
            var shapeArr = TShape.ToArray();
            shapeArr.Should().BeEquivalentTo(new[] {1, 2, 3});

            var newShape = TShape.SubShape(0, 0);
            (newShape == TShape).Should().BeTrue();

            TShape.Equals(null).Should().BeFalse();
            TShape.Equals(new TShape(1, 2)).Should().BeFalse();
            TShape.Equals(new TShape(1, 2, 3)).Should().BeTrue();
        }

        [Theory]
        [InlineData(2, 3)]
        public void Op(params int[] shape)
        {
            var newShape = new TShape(shape);
            (newShape == TShape).Should().BeFalse();
            (newShape != TShape).Should().BeTrue();
        }
    }
}
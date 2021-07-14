using Bight.Tensor;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensorSize
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public TensorSize Size = new TensorSize(1, 2, 3);

        public TestTensorSize(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestPrintTShape()
        {
            _testOutputHelper.WriteLine(Size.ToString());
            _testOutputHelper.WriteLine(Size.Volume.ToString());
            _testOutputHelper.WriteLine(Size.Rank.ToString());
            _testOutputHelper.WriteLine(Size[0].ToString());
        }


        [Fact]
        public void TestClone()
        {
            var clone = Size.Clone();
            _testOutputHelper.WriteLine(clone.ToString());
        }

        [Fact]
        public void TestFunction()
        {
            var shapeArr = Size.ToArray();
            shapeArr.Should().BeEquivalentTo(new[] {1, 2, 3});

            var newShape = Size.SubShape(0, 0);
            (newShape == Size).Should().BeTrue();

            Size.Equals(null).Should().BeFalse();
            Size.Equals(new TensorSize(1, 2)).Should().BeFalse();
            Size.Equals(new TensorSize(1, 2, 3)).Should().BeTrue();
        }

        [Theory]
        [InlineData(2, 3)]
        public void Operators(params int[] shape)
        {
            var newShape = new TensorSize(shape);
            (newShape == Size).Should().BeFalse();
            (newShape != Size).Should().BeTrue();
        }
    }
}
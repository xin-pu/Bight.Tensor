using Bight.Tensor;
using Bight.Tensor.Static;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TestTensorMath
    {
        private readonly ITestOutputHelper _testOutputHelper;


        public TestTensorMath(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestTrigonometric()
        {
            var tensor1 = Tensor<double>.BuildTensor(new[] {-1.57079, 0.0, 1.57079});
            var tensor = TensorMath<double>.Sin(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Asin(tensor);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Sinh(tensor);
            _testOutputHelper.WriteLine(tensor + "\r");

            tensor = TensorMath<double>.Cos(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Acos(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Cosh(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");

            tensor = TensorMath<double>.Tan(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Atan(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
            tensor = TensorMath<double>.Tanh(tensor1);
            _testOutputHelper.WriteLine(tensor + "\r");
        }


        [Fact]
        public void TestLinq()
        {
            var tensor1 = Tensor<double>
                .BuildTensor(new double[,]
                {
                    {-1, 2},
                    {3, -4}
                });
            tensor1.Abs();
            _testOutputHelper.WriteLine(tensor1 + "\r");
            tensor1.Negate();
            _testOutputHelper.WriteLine(tensor1 + "\r");
            tensor1.All(0.3);
            _testOutputHelper.WriteLine(tensor1 + "\r");
            tensor1.One();
            _testOutputHelper.WriteLine(tensor1 + "\r");
            tensor1.Empty();
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }
    }
}
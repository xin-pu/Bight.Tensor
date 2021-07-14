using Bight.Tensor;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorMathFunction
    {
        private readonly ITestOutputHelper _testOutputHelper;

        //private readonly Tensor<double> tensor1 = Tensor<double>
        //    .BuildTensor(new double[,]
        //    {
        //        {-1, 2},
        //        {3, -4}
        //    });

        //private Tensor<double> tensor2 = Tensor<double>
        //    .BuildTensor(new double[,]
        //    {
        //        {0, 1},
        //        {1, 0}
        //    });

        public TensorMathFunction(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestTrigonometric()
        {
            var tensor1 = Tensor<double>.BuildTensor(new[] {-1.57079, 0.0, 1.57079});
            var tensor = tensor1.Sin();
            _testOutputHelper.WriteLine(tensor + "\r");


            tensor = tensor1.Cos();
            _testOutputHelper.WriteLine(tensor + "\r");


            tensor = tensor1.Tan();
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
using System;
using Bight.Tensor;
using Bight.Tensor.Static;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class TensorMath
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private readonly Tensor<double> tensor1 = Tensor<double>
            .BuildTensor(new[,]
            {
                {-1.6, 2.2},
                {3.2, -5.7}
            });

        public TensorMath(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestAbs()
        {
            tensor1.Abs();
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }

        [Fact]
        public void TestNegate()
        {
            tensor1.Negate();
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }

        [Fact]
        public void TestAll()
        {
            tensor1.All(0.3);
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }

        [Fact]
        public void TestOne()
        {
            tensor1.One();
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }

        [Fact]
        public void TestZero()
        {
            tensor1.Zero();
            _testOutputHelper.WriteLine(tensor1 + "\r");
        }

        [Fact]
        public void TestMin()
        {
            _testOutputHelper.WriteLine($"Min:{tensor1.Min()}");
            _testOutputHelper.WriteLine($"Max:{tensor1.Max()}");
            _testOutputHelper.WriteLine($"Sum:{tensor1.Sum()}");
        }


        [Fact]
        public void TestAdd()
        {
            var tensor = TensorOps<double>.Add(tensor1, tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }


        [Fact]
        public void TestAddN()
        {
            var tensor = TensorOps<double>.AddN(tensor1, tensor1, tensor1, tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestRound()
        {
            var tensor = TensorOps<double>.Round(tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestSqrt()
        {
            var tensor = TensorOps<double>.Sqrt(tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestSquare()
        {
            var tensor = TensorOps<double>.Square(tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestLog()
        {
            var tensorIN = Tensor<double>.BuildTensor(new[] {0, Math.E});
            var tensor = TensorOps<double>.Log(tensorIN);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestLog10()
        {
            var tensor = TensorOps<double>.Log10(tensor1);
            _testOutputHelper.WriteLine(tensor.ToString());
        }

        [Fact]
        public void TestExp()
        {
            var tensorIN = Tensor<double>.BuildTensor(new[] {0, Math.Log(2)});
            var tensor = TensorOps<double>.Exp(tensorIN);
            _testOutputHelper.WriteLine(tensor.ToString());
        }
    }
}
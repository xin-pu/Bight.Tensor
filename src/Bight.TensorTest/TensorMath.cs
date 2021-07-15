﻿using Bight.Tensor;
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
    }
}
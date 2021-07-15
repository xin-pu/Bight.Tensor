using System;
using Bight.Tensor;
using Bight.Tensor.Exception;
using Bight.Tensor.Matrix;
using Bight.Tensor.Vector;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bight.TensorTest
{
    public class Tensor
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Tensor<double> matrix;
        private readonly Tensor<double> tensor;
        private readonly Tensor<double> vector;

        public Tensor(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            vector = new Tensor<double>(new TensorSize(3));
            matrix = new Tensor<double>(new TensorSize(3, 3));
            tensor = new Tensor<double>(new TensorSize(2, 3, 4));
        }

        [Fact]
        public void CheckIsVector()
        {
            vector.IsVector.Should().BeTrue();
            matrix.IsVector.Should().BeFalse();
            tensor.IsVector.Should().BeFalse();
        }

        [Fact]
        public void CheckIsMatrix()
        {
            vector.IsMatrix.Should().BeFalse();
            matrix.IsMatrix.Should().BeTrue();
            tensor.IsMatrix.Should().BeFalse();
        }

        [Fact]
        public void CheckIsTensor()
        {
            vector.IsTensor.Should().BeFalse();
            matrix.IsTensor.Should().BeFalse();
            tensor.IsTensor.Should().BeTrue();
        }

        [Fact]
        public void CheckIsSquareMatrix()
        {
            vector.IsSquareMatrix.Should().BeFalse();
            matrix.IsSquareMatrix.Should().BeTrue();
            tensor.IsSquareMatrix.Should().BeFalse();
        }

        [Fact]
        public void TestThisSetGet()
        {
            var t1 = Tensor<double>.BuildZeros(2);
            t1[1] = 3F;
            var v1 = t1[1];
            v1.Should().Be(3);

            var t2 = Tensor<double>.BuildZeros(2, 3);
            t2[1, 0] = 5F;
            var v2 = t2[1, 0];
            v2.Should().Be(5);

            var t3 = Tensor<double>.BuildZeros(2, 3, 4);
            t3[1, 0, 0] = 5F;
            var v3 = t3[1, 0, 0];
            v3.Should().Be(5);


            t3[new[] {1, 0, 0}] = 3;
            v3 = t3[new[] {1, 0, 0}];
            v3.Should().Be(3);
        }

        [Fact]
        public void TestSlice()
        {
            var tensorSelf = Tensor<double>.BuildTensor(new double[,,]
            {
                {{1, 2}, {3, 4}, {3, 4}},
                {{0, 2}, {0, 4}, {0, 4}}
            });
            var s1 = tensorSelf.Slice(0, 1);
            _testOutputHelper.WriteLine(s1.ToString());

            var s2 = tensorSelf.Slice(0, 2);
            _testOutputHelper.WriteLine(s2.ToString());

            Action act = () => { tensorSelf.Slice(1, 1); };
            act.Should().Throw<InvalidShapeException>().WithMessage("Slicing cannot be performed");
        }


        [Fact]
        public void TestGetSubTensor()
        {
            var t = Tensor<double>.BuildZeros(2, 3, 4);
            t[0, 0, 0] = 3F;
            t[0, 1, 0] = 4F;
            t[1, 0, 0] = 5F;
            var subM = t.GetSubTensor(0);
            var sunV = subM.GetSubTensor(1);
            _testOutputHelper.WriteLine(t.ToString());
            _testOutputHelper.WriteLine(subM.ToString());
            _testOutputHelper.WriteLine(sunV.ToString());
        }


        [Fact]
        public void TestTranspose()
        {
            var t = Tensor<double>.BuildTensor(new double[,,]
            {
                {
                    {3, 1, 2, 1},
                    {4, 1, 7, 8},
                    {5, 1, 7, 8}
                },
                {
                    {3, 1, 2, 1},
                    {4, 1, 7, 8},
                    {4, 1, 7, 8}
                }
            });

            _testOutputHelper.WriteLine(t.ToString());
            t.Transpose();
            _testOutputHelper.WriteLine(t.ToString());
        }

        [Fact]
        public void TestTranspose2()
        {
            var t = Tensor<double>.BuildIdentityMatrix(3);
            t.SetValueNoCheck(2, 1);
            _testOutputHelper.WriteLine(t.ToString());
            _testOutputHelper.WriteLine(t.IsContiguous.ToString());
            t.Transpose();
            _testOutputHelper.WriteLine(t.ToString());
            _testOutputHelper.WriteLine(t.IsContiguous.ToString());
        }

        [Fact]
        public void TestMatrix()
        {
            var vector1 = new Vector<double>(4);
            _testOutputHelper.WriteLine(vector1.ToString());

            var matrix1 = new Matrix<double>(3, 3);
            _testOutputHelper.WriteLine(matrix1.ToString());
        }
    }
}
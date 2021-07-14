using System;

namespace Bight.Tensor.Holder
{
    public class DoubleWrapper : IOperations<double>
    {
        public double One => 1F;
        public double Zero => 0F;

        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Negate(double a)
        {
            return -a;
        }

        public double Divide(double a, double b)
        {
            return a / b;
        }


        public double Copy(double a)
        {
            return a;
        }

        public bool AreEqual(double a, double b)
        {
            return Math.Abs(a - b) < 1e-7;
        }

        public bool IsZero(double a)
        {
            return Math.Abs(a) < 1e-7;
        }

        public string ToString(double a)
        {
            return a.ToString("F4");
        }
    }
}
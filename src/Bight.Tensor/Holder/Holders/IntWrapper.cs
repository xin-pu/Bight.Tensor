namespace Bight.Tensor.Holder
{
    public class IntWrapper : IOperations<int>
    {
        public int One => 1;

        public int Zero => 0;

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Negate(int a)
        {
            return -a;
        }

        public int Divide(int a, int b)
        {
            return a / b;
        }

        public int Copy(int a)
        {
            return a;
        }

        public bool AreEqual(int a, int b)
        {
            return a == b;
        }

        public bool IsZero(int a)
        {
            return a == 0;
        }

        public string ToString(int a)
        {
            return a.ToString();
        }
    }
}
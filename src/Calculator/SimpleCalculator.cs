using System.Linq;

namespace Calculator
{
    public class SimpleCalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Sum(int[] values)
        {
            return values.Sum();
        }

        public int Subtract(int x, int y)
        {
            return x - y;
        }

        public int Multiply(int x, int y)
        {
            return x*y;
        }

        public int Divide(int x, int y)
        {
            return x/y;
        }
    }
}

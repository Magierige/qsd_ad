namespace AD
{
    public class Opgave4
    {
        public static int Enen(int n)
        {
            if (n < 0)
            {
                //here must come a exeption but the exception is not defined yet
                //throw new EnenNegativeValueException();
            }
            if (n <= 1)
            {
                return n;
            }
            int count = 1;
            while (n >= count * 2)
            {
                count *= 2;
            }
            return 1 + Enen(n - count);
        }

        public static void Run()
        {
            for (int i = 0; i < 20; i++)
            {
                System.Console.WriteLine("Enen({0,4}) = {1,2}", i, Enen(i));
            }
            System.Console.WriteLine("Enen(1024) = {0,2}", Enen(1024));
            System.Console.WriteLine();
        }
    }
}

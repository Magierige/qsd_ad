using System.Collections.Generic;

namespace AD
{
    public class Opgave6
    {
        public static string ForwardString(List<int> list, int from_index)
        {
            if (from_index < 0 || from_index >= list.Count)
            {
                return "";
            }
            string result = "";
            foreach (int i in list.GetRange(from_index, list.Count - from_index))
            {
                if (i >= from_index)
                {
                    result += i + " ";
                }
            }
            return result;
        }
        
        public static string BackwardString(List<int> list, int from_index)
        {
            if (from_index < 0 || from_index >= list.Count)
            {
                return "";
            }
            string result = "";
            for (int i = list.Count - 1; i >= from_index; i--)
            {
                result += list[i] + " ";
            }
            return result;
        }

        public static void Run()
        {
            List<int> list = new List<int>(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11});

            System.Console.WriteLine(ForwardString(list, 3));
            System.Console.WriteLine(ForwardString(list, 7));
            System.Console.WriteLine(BackwardString(list, 3));
            System.Console.WriteLine(BackwardString(list, 7));
        }
    }
}

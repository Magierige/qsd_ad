using System.Collections.Generic;
using System.Diagnostics;


namespace AD
{
    public partial class QuickSort : Sorter
    {
        private static int CUTOFF = 3;

        public override void Sort(List<int> list)
        {
            if (list.Count <= 1)
            {
                return;
            }

            if (list.Count <= CUTOFF)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    int current = list[i];
                    int j = i - 1;
                    while (j >= 0 && list[j] > current)
                    {
                        list[j + 1] = list[j];
                        j--;
                    }
                    list[j + 1] = current;
                }
                return;
            }

            int pivot = list.Count / 2;
            if (list[0] < list[list.Count - 1] && list[0] > list[pivot] || list[0] > list[list.Count - 1] && list[0] < list[pivot])
            {
                pivot = 0;
            }
            else if (list[list.Count - 1] < list[0] && list[list.Count - 1] > list[pivot] || list[list.Count - 1] > list[0] && list[list.Count - 1] < list[pivot])
            {
                pivot = list.Count - 1;
            }

            int pivotVal = list[pivot];
            list[pivot] = list[list.Count - 1];
            list[list.Count - 1] = pivotVal;

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            for (int x = 0; x < list.Count - 1; x++)
            {
                if (list[x] < pivotVal)
                {
                    left.Add(list[x]);
                }
                else
                {
                    right.Add(list[x]);
                }
            }

            Sort(left);
            Sort(right);

            list.Clear();
            list.AddRange(left);
            list.Add(pivotVal);
            list.AddRange(right);
        }
    }
}

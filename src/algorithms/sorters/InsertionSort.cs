using System.Collections.Generic;


namespace AD
{
    public partial class InsertionSort : Sorter
    {
        public override void Sort(List<int> list)
        {
            Sort(list, 0, list.Count - 1);
        }

        public void Sort(List<int> list, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
            {
                int key = list[i];
                int j = i - 1;
                while (j >= lo && list[j] > key)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
        }
    }
}

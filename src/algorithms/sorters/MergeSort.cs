using System.Collections.Generic;
using System.Diagnostics;


namespace AD
{
    public partial class MergeSort : Sorter
    {
        public override void Sort(List<int> list)
        {
            if (list.Count <= 1)
            {
                return;
            }
            List<int> left = new List<int>(list);
            List<int> right = new List<int>(list);
            int mid = list.Count / 2;
            left.RemoveRange(mid, list.Count - mid);
            right.RemoveRange(0, mid);

            Sort(left);
            Sort(right);

            int i = 0, j = 0, k = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i] < right[j])
                {
                    list[k++] = left[i++];
                }
                else
                {
                    list[k++] = right[j++];
                }
            }
            while (i < left.Count)
            {
                list[k++] = left[i++];
            }
            while (j < right.Count)
            {
                list[k++] = right[j++];
            }
        }

    }
}

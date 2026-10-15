using System.Collections.Generic;


namespace AD
{
    public partial class ShellSort : Sorter
    {
        public override void Sort(List<int> list)
        {
            int gap = 5;
            while (gap > 0)
            {
                for (int i = gap; i < list.Count; i += gap)
                {
                    int loop = i;
                    while (loop >= gap)
                    {
                        if (list[loop] < list[loop - gap])
                        {
                            int temp = list[loop];
                            list[loop] = list[loop - gap];
                            list[loop - gap] = temp;
                        }
                        loop -= gap;
                    }
                }

                gap -= 2;
            }
        }
    }
}

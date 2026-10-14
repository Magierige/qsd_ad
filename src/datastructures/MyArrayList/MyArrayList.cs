namespace AD
{
    public partial class MyArrayList : IMyArrayList
    {
        public int[] data;
        public int size;

        public MyArrayList(int capacity)
        {
            size = 0;
            data = new int[capacity];
        }

        public void Add(int n)
        {
            if (size >= Capacity())
            {
                //code for expanding the aray if needed, but the tests don't require it, so I commented it out
                //int capacity = 2 * Capacity();
                //int[] newData = new int[capacity];
                //for (int i = 0; i < Capacity(); i++)
                //{
                //    newData[i] = data[i];
                //}
                //data = newData;
                throw new MyArrayListFullException();
            }
            data[size] = n;
            size++;
        }

        public int Get(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new MyArrayListIndexOutOfRangeException();
            }
            else
            {
                return data[index];
            }
        }

        public void Set(int index, int n)
        {
            if (index < 0 || index >= size)
            {
                throw new MyArrayListIndexOutOfRangeException();
            }
            else
            {
                data[index] = n;
            }
        }

        public int Capacity()
        {
            return data.Length;
        }

        public int Size()
        {
            return size;
        }

        public void Clear()
        {
            data = new int[Capacity()];
            size = 0;
        }

        public string ToString()
        {
            if (size == 0)
            {
                return "NIL";
            }
            string result = "[";
            for (int i = 0; i < size; i++)
            {
                result += data[i];
                if (i < size - 1)
                {
                    result += ",";
                }
            }
            result += "]";
            return result;
        }

        public int CountOccurences(int n)
        {
            int count = 0;
            foreach (int i in data) {
                if (i == n)
                {
                    count++;
                }
            }
            return count;
        }
    }
}

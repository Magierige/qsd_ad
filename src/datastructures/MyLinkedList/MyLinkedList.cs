using System.Data.Common;

namespace AD
{
    public partial class MyLinkedList<T> : IMyLinkedList<T>
    {
        public MyLinkedListNode<T> first;
        public int size;

        public MyLinkedList()
        {
            first = null;
            size = 0;
        }

        public void AddFirst(T data)
        {
            MyLinkedListNode<T> temp = new MyLinkedListNode<T>{data = data, next = first};
            first = temp;
            size++;
        }

        public T GetFirst()
        {
            if (first == null)
            {
                throw new MyLinkedListEmptyException();
            }
            return first.data;
        }

        public void RemoveFirst()
        {
            if (first == null)
            {
                throw new MyLinkedListEmptyException();
            }
            else if (size == 1)
            {
                first = null;
            }
            else
            {
                first = first.next;
            }
            size--;
        }

        public int Size()
        {
            return size;
        }

        public void Clear()
        {
            first = null;
            size = 0;
        }

        public void Insert(int index, T data)
        {
            if (index < 0 || index > size)
            {
                throw new MyLinkedListIndexOutOfRangeException();
            }
            else
            {
                if (index == 0)
                {
                    AddFirst(data);
                }
                else
                {
                    MyLinkedListNode<T> current = first;
                    for (int i = 0; i < index - 1; i++)
                    {
                        current = current.next;
                    }
                    MyLinkedListNode<T> newNode = new MyLinkedListNode<T> { data = data, next = current.next };
                    current.next = newNode;
                    size++;
                }
            }
        }

        public override string ToString()
        {
            if (size == 0)
            {
                return "NIL";
            }
            string result = "[";
            MyLinkedListNode<T> data = first;
            for (int i = 0; i < size; i++)
            {
                result += data.data;
                if (i < size - 1)
                {
                    result += ",";
                }
                data = data.next;
            }
            result += "]";
            return result;
        }
    }
}
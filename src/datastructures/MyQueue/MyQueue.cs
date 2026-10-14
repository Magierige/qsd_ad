namespace AD
{
    public partial class MyQueue<T> : IMyQueue<T>
    {
        private MyQueueNode<T> front;
        private MyQueueNode<T> back;
        private int size = 0;

        public MyQueue()
        {
            front = null;
            back = null;
            size = 0;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Enqueue(T data)
        {
            if (data == null)
            {
                throw new System.ArgumentNullException();
            }
            MyQueueNode<T> newNode = new MyQueueNode<T> { data = data, next = null };
            if (IsEmpty())
            {
                front = newNode;
                back = newNode;
            }
            else
            {
                back.next = newNode;
                back = newNode;
            }
            size++;
        }

        public T GetFront()
        {
            if (IsEmpty())
            {
                throw new MyQueueEmptyException();
            }
            return front.data;
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new MyQueueEmptyException();
            }
            if (size == 1)
            {
                T data = front.data;
                front = null;
                back = null;
                size--;
                return data;
            }
            T result = front.data;
            front = front.next;
            size--;
            return result;
        }

        public void Clear()
        {
            front = null;
            back = null;
            size = 0;
        }

    }
}
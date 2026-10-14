namespace AD
{
    public partial class MyQueue<T> : IMyQueue<T>
    {
        private MyQueueNode<T> front;
        private MyQueueNode<T> back;
        private int size = 0;
        
        public bool IsEmpty()
        {
            throw new System.NotImplementedException();
        }

        public void Enqueue(T data)
        {
            throw new System.NotImplementedException();
        }

        public T GetFront()
        {
            throw new System.NotImplementedException();
        }

        public T Dequeue()
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

    }
}
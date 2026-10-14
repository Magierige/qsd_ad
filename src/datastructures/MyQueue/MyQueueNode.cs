namespace AD
{
    public partial class MyQueueNode<T>
    {
        public T data;
        public MyQueueNode<T> next;

        public MyQueueNode()
        {
        }

        public MyQueueNode(T data, MyQueueNode<T> next)
        {
            this.data = data;
            this.next = next;
        }
    }
}

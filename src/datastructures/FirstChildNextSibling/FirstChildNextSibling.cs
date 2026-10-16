namespace AD
{
    public partial class FirstChildNextSibling<T> : IFirstChildNextSibling<T>
    {
        public FirstChildNextSiblingNode<T> root;

        public IFirstChildNextSiblingNode<T> GetRoot()
        {
            return root;
        }

        public int Size()
        {
            int size = 1;
            if (root == null)
            {
                return 0;
            }
            if (root.firstChild != null)
            {
                size += new FirstChildNextSibling<T> { root = root.firstChild }.Size();
            }
            if (root.nextSibling != null)
            {
                size += new FirstChildNextSibling<T> { root = root.nextSibling }.Size();
            }
            return size;
        }

        public void PrintPreOrder()
        {
            //"a,FC(b,FC(e),NS(c,NS(d)))"
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            if (root == null)
            {
                return "NIL";
            }
            else
            {
                return $"{root.data}{(root.firstChild != null ? ",FC(" + new FirstChildNextSibling<T> { root = root.firstChild }.ToString() + ")" : "")}{(root.nextSibling != null ? ",NS(" + new FirstChildNextSibling<T> { root = root.nextSibling }.ToString() + ")" : "")}";
            }
        }
    }
}
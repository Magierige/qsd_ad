namespace AD
{
    public partial class BinaryTree<T> : IBinaryTree<T>
    {
        public BinaryNode<T> root;

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------

        public BinaryTree()
        {
            root = null;
        }

        public BinaryTree(T rootItem)
        {
            root = new BinaryNode<T>(rootItem, null, null);
        }


        //----------------------------------------------------------------------
        // Interface methods that have to be implemented for exam
        //----------------------------------------------------------------------

        public BinaryNode<T> GetRoot()
        {
            return root;
        }

        public int Size()
        {
            int size = 1;
            if (IsEmpty())
            {
                return 0;
            }
            if (root.left != null)
            {
                size += new BinaryTree<T>(root.left.data) { root = root.left }.Size();
            }
            if (root.right != null)
            {
                size += new BinaryTree<T>(root.right.data) { root = root.right }.Size();
            }
            return size;
        }

        public int Height()
        {
            if (IsEmpty())
            {
                return -1;
            }

            int leftHeight = 0;
            int rightHeight = 0;

            if (root.left != null)
            {
                BinaryTree<T> leftTree = new BinaryTree<T> { root = root.left };
                leftHeight = 1 +leftTree.Height();
            }

            if (root.right != null)
            {
                BinaryTree<T> rightTree = new BinaryTree<T> { root = root.right };
                rightHeight = 1 +rightTree.Height();
            }

            return  System.Math.Max(leftHeight, rightHeight);
        }

        public void MakeEmpty()
        {
            root = null;
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void Merge(T rootItem, BinaryTree<T> t1, BinaryTree<T> t2)
        {
            root = new BinaryNode<T>(rootItem, t1?.root, t2?.root);
        }

        public string ToPrefixString()
        {
            string result = "";
            if (IsEmpty())
            {
                result = "NIL";
            }
            else
            {
                result = "[ " + root.data.ToString() + " " + (root.left != null ? new BinaryTree<T> { root = root.left }.ToPrefixString() : "NIL") + " "
                    + (root.right != null ? new BinaryTree<T> { root = root.right }.ToPrefixString() : "NIL") + " ]";
            }
            return result;
        }

        public string ToInfixString()
        {
            string result = "";
            if (IsEmpty())
            {
                result = "NIL";
            }
            else
            {
                result = "[ " + (root.left != null ? new BinaryTree<T> { root = root.left }.ToInfixString() : "NIL") + " " +  root.data.ToString()  + " "
                    + (root.right != null ? new BinaryTree<T> { root = root.right }.ToInfixString() : "NIL") + " ]";
            }
            return result;
        }

        public string ToPostfixString()
        {
            string result = "";
            if (IsEmpty())
            {
                result = "NIL";
            }
            else
            {
                result = "[ " + (root.left != null ? new BinaryTree<T> { root = root.left }.ToPostfixString() : "NIL") + " "
                    + (root.right != null ? new BinaryTree<T> { root = root.right }.ToPostfixString() : "NIL") + " " +  root.data.ToString()  + " ]";
            }
            return result;
        }


        //----------------------------------------------------------------------
        // Interface methods : methods that have to be implemented for homework
        //----------------------------------------------------------------------

        public int NumberOfLeaves()
        {
            if (IsEmpty())
            {
                return 0;
            }

            int leaves = 0;

            if (root.left == null && root.right == null)
            {
                leaves++;
            }
            else
            {
                if (root.left != null)
                {
                    BinaryTree<T> leftTree = new BinaryTree<T> { root = root.left };
                    leaves += leftTree.NumberOfLeaves();
                }
                if (root.right != null)
                {
                    BinaryTree<T> rightTree = new BinaryTree<T> { root = root.right };
                    leaves += rightTree.NumberOfLeaves();
                }
            }
            return leaves;
        }

        public int NumberOfNodesWithOneChild()
        {
            if (IsEmpty())
            {
                return 0;
            }

            int singleChilds = 0;

            if (root.left == null ^ root.right == null)
            {
                singleChilds++;
            }

            if (root.left != null)
            {
                BinaryTree<T> leftTree = new BinaryTree<T> { root = root.left };
                singleChilds += leftTree.NumberOfNodesWithOneChild();
            }
            if (root.right != null)
            {
                BinaryTree<T> rightTree = new BinaryTree<T> { root = root.right };
                singleChilds += rightTree.NumberOfNodesWithOneChild();
            }

            return singleChilds;
        }

        public int NumberOfNodesWithTwoChildren()
        {
            if (IsEmpty())
            {
                return 0;
            }

            int twoChilds = 0;

            if (root.left != null && root.right != null)
            {
                twoChilds++;
            }

            if (root.left != null)
            {
                BinaryTree<T> leftTree = new BinaryTree<T> { root = root.left };
                twoChilds += leftTree.NumberOfNodesWithTwoChildren();
            }
            if (root.right != null)
            {
                BinaryTree<T> rightTree = new BinaryTree<T> { root = root.right };
                twoChilds += rightTree.NumberOfNodesWithTwoChildren();
            }

            return twoChilds;
        }
    }
}
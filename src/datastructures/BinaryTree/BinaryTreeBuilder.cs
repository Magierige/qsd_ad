namespace AD
{
    //
    // This class offers static methods to create datastructures.
    // It is called by unit tests.
    //
    public partial class DSBuilder
    {
        public static IBinaryTree<int> CreateBinaryTreeEmpty()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            return tree;
        }

        //
        //         5
        //       /   \
        //     2       6
        //    / \
        //   8   7
        //      /
        //     1
        //      \
        //       4
        public static IBinaryTree<int> CreateBinaryTreeInt()
        {
            BinaryTree<int> tq = new BinaryTree<int>(8);
            BinaryTree<int> tg = new BinaryTree<int>();
            BinaryTree<int> to = new BinaryTree<int>();
            BinaryTree<int> tp = new BinaryTree<int>(4);
            BinaryTree<int> tw = new BinaryTree<int>();
            BinaryTree<int> tt = new BinaryTree<int>();
            BinaryTree<int> ta = new BinaryTree<int>(6);

            to.Merge(1, null, tp);
            tg.Merge(7, to, null);
            tw.Merge(2, tq, tg);
            tt.Merge(5, tw, ta);
            return tt;
        }

        //
        //         t
        //       /   \
        //     w       a
        //    / \     / \
        //   q   g   o   p
        public static IBinaryTree<string> CreateBinaryTreeString()
        {
            BinaryTree<string> tq = new BinaryTree<string>("q");
            BinaryTree<string> tg = new BinaryTree<string>("g");
            BinaryTree<string> to = new BinaryTree<string>("o");
            BinaryTree<string> tp = new BinaryTree<string>("p");
            BinaryTree<string> tw = new BinaryTree<string>();
            BinaryTree<string> tt = new BinaryTree<string>();
            BinaryTree<string> ta = new BinaryTree<string>();

            tw.Merge("w", tq, tg);
            ta.Merge("a", to, tp);
            tt.Merge("t", tw, ta);

            return tt;
        }
    }
}
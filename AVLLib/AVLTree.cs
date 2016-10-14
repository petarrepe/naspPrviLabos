namespace AVLLib
{
    public class AVLTree<T> where T: System.IComparable<T>
    {
        private Node<T> root = new Node<T>();

        //parent = root , child = pivot
        internal void RightRotate(Node<T> child, Node<T> parent)
        {
            if (child.rightChild != null) parent.leftChild = child.rightChild;
            child.rightChild = parent;
        }

        internal void LeftRotate(Node<T> child, Node<T> parent)
        {
            if (child.leftChild != null) parent.rightChild = child.leftChild;
            child.leftChild = parent;
        }

        internal void Insert (T data) 
        {
            Node<T> parentNode = FindParentOfNewNode(data);

            if (data.CompareTo(parentNode.data) > 0) parentNode.rightChild = new Node<T>(data);
            else parentNode.leftChild = new Node<T>(data);

            //TODO : balansiranje
        }

        private Node<T> FindParentOfNewNode(T data)
        {
            Node<T> parentNode = root;
            Node<T> node = root;

            while (node != null)
            {
                if (data.CompareTo (node.data) > 0)
                {
                    parentNode = node;
                    node = node.rightChild;
                }
                else if (data.CompareTo(node.data) < 0)
                {
                    parentNode = node;
                    node = node.leftChild;
                }
                else return null; //već je upisan taj element
            }

            return parentNode;
        }

        private int CalculateBalanceFactor(Node<T> node)
        {
            if (node == null) return 0;

            //int balanceFactor=0;

            int rightSubtreeWeight = CalculateBalanceFactor(node.rightChild)+1;
            int leftSubtreeWeight = CalculateBalanceFactor(node.leftChild)-1;

            return rightSubtreeWeight - leftSubtreeWeight;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

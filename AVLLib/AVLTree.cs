namespace AVLLib
{
    public class AVLTree<T> where T: System.IComparable<T>
    {
        private Node<T> root = new Node<T>();

        public AVLTree(T data)
        {
            root.data = data;
        }

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

        public void Insert (T data) 
        {
            Node<T> parentNode = FindParentOfNewNode(data);
            if (parentNode == null) return; 

            if (data.CompareTo(parentNode.data) > 0) parentNode.rightChild = new Node<T>(data);
            else parentNode.leftChild = new Node<T>(data);

            //sve dobro do ovog dijela
            int balance = CalculateBalanceFactor(parentNode);
            //TODO : balansiranje
        }

        private Node<T> FindParentOfNewNode(T data)
        {
            Node<T> parentNode = null;
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

            int rightSubtreeHeight = CalculateBalanceFactor(node.rightChild)+1;
            int leftSubtreeHeight = CalculateBalanceFactor(node.leftChild)-1;

            return rightSubtreeHeight - leftSubtreeHeight;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

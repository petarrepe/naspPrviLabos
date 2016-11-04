using System;

namespace AVLLib
{
    public class AVLTree<T> where T : System.IComparable<T>
    {
        private Node<T> root = new Node<T>();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        public AVLTree(T data)
        {
            root.data = data;
        }

        //parent = root , child = pivot
        internal void RightRotate(ref Node<T> parent)
        {
            Node<T> grandParent = FindParentOfNode(parent);
            Node<T> child = parent.leftChild;

            parent.leftChild = child.rightChild;
            child.rightChild = parent;          

            if (parent == this.root)
            {
                this.root = child;
            }


            else if (grandParent != null)
            {
                if (grandParent.data.CompareTo(parent.data) > 0)
                {
                    grandParent.leftChild = child;
                }
                else
                {
                    grandParent.rightChild = child;
                }
            }

        }

        internal void LeftRotate(ref Node<T> parent)
        {
            Node<T> grandParent = FindParentOfNode(parent);
            Node<T> child = parent.rightChild;

            parent.rightChild = child.leftChild;
            child.leftChild = parent;


            if (parent == this.root)
            {
                this.root = child;
            }


            else if (grandParent != null)
            {
                if (grandParent.data.CompareTo(parent.data) > 0)
                {
                    grandParent.leftChild = child;
                }
                else
                {
                    grandParent.rightChild = child;
                }
            }


        }


        public void Insert(T data)
        {
            int balanceFactor = 0;
            int previousBalanceFactor = 0;
            Node<T> parentNode = FindParentOfNewNode(data);
            Node<T> grandParent = FindParentOfNode(parentNode);
            Node<T> previouslyVisitedNode = new Node<T>();
            Node<T> currentlyVisitedNode = parentNode;

            if (parentNode == null) return; //duplikat

            if (data.CompareTo(parentNode.data) > 0)
            {
                parentNode.rightChild = new Node<T>(data);
            }
            else
            {
                parentNode.leftChild = new Node<T>(data);
            }

            do
            {
                previousBalanceFactor = balanceFactor;
                balanceFactor = CalculateHeight(currentlyVisitedNode.rightChild) - CalculateHeight(currentlyVisitedNode.leftChild);
                if (balanceFactor == 0) return; //rani uvjet izlaska

                previouslyVisitedNode = currentlyVisitedNode;
                currentlyVisitedNode = FindParentOfNode(currentlyVisitedNode);
            }
            while ((balanceFactor != 2 && balanceFactor != -2)  &&  currentlyVisitedNode != null);

            if (balanceFactor > 1 && previousBalanceFactor == 1)
            {
                LeftRotate( ref previouslyVisitedNode);
            }
            else if (balanceFactor < -1 && previousBalanceFactor == -1)
            {
                RightRotate(ref previouslyVisitedNode);
            }

            //if (parentNode == null) return; //stablo je trenutno balansirano //zašto je ovo ovdje?

            else if (balanceFactor < -1 && previousBalanceFactor == 1)
            {
                LeftRotate(ref previouslyVisitedNode.leftChild);
                RightRotate(ref previouslyVisitedNode);
            }
            else if (balanceFactor > 1 && previousBalanceFactor == -1)
            {
                RightRotate(ref previouslyVisitedNode.rightChild);
                LeftRotate(ref previouslyVisitedNode);
            }

        }

        private Node<T> FindParentOfNewNode(T data)
        {
            Node<T> parentNode = null;
            Node<T> node = root;

            while (node != null)
            {
                if (data.CompareTo(node.data) > 0)
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

        private Node<T> FindParentOfNode(Node<T> node)
        {
            Node<T> parentNode = null;
            Node<T> nextNode = root;

            if (node == null) return null;

            while (nextNode != null)
            {
                if (nextNode.data.CompareTo(node.data) > 0)
                {
                    parentNode = nextNode;
                    nextNode = nextNode.leftChild;
                }
                else if (nextNode.data.CompareTo(node.data) < 0)
                {
                    parentNode = nextNode;
                    nextNode = nextNode.rightChild;
                }
                else return parentNode;
            }
            return parentNode;
        }

        private int CalculateHeight(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                int left = CalculateHeight(node.leftChild);
                int right = CalculateHeight(node.rightChild);
                return 1 + System.Math.Max(left, right);
            }
        }

        public override string ToString()
        {        
            int heightOfATree = CalculateHeight(root);

            for (int i = 0; i < heightOfATree; i++)
            {
                //sb.Append(' ', heightOfATree - i);
                //PrintAllNodesOnLevel(root, i);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        [Obsolete]
        private void PrintAllNodesOnLevel(Node<T> root, int level)
        {
            if (root == null) return;
            if (level == 1) sb.Append( root.data.ToString().PadLeft(5).PadRight(5));
            else if (level > 1)
            {
                PrintAllNodesOnLevel(root.leftChild, level - 1);
                PrintAllNodesOnLevel(root.rightChild, level - 1);
            }
        }

    }
}


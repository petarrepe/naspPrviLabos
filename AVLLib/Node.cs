namespace AVLLib
{
    public class Node<T> where T:System.IComparable<T>
    {
        public Node(T data)
        {
            this.data = data;
        }
        public Node()
        {
        }

        internal T data { get; set; }
        

        public Node<T> leftChild { get; set; }
        public Node<T> rightChild { get; set; }
    }

}

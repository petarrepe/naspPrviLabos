using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AVLLib
{
    public static class TreeDrawer
    {
        public static List<string> _recursivelyDrawTree<T>
       (Node<T> node, out int positionOutput, out int widthOutput)
       where T : IComparable<T>
        {
            widthOutput = 0;
            positionOutput = 0;

            if (node == null)
            {
                return new List<string>();
            }

            //
            // Variables
            int leftPosition, rightPosition, leftWidth, rightWidth;

            //
            // Start drawing
            var nodeLabel = Convert.ToString(node.data);

            // Visit the left child
            List<string> leftLines = _recursivelyDrawTree(node.leftChild, out leftPosition, out leftWidth);

            // Visit the right child
            List<string> rightLines = _recursivelyDrawTree(node.rightChild, out rightPosition, out rightWidth);

            // Calculate pads
            int middle = Math.Max(Math.Max(2, nodeLabel.Length), (rightPosition + leftWidth - leftPosition + 1));
            int position_out = leftPosition + middle / 2;
            int width_out = leftPosition + middle + rightWidth - rightPosition;

            while (leftLines.Count < rightLines.Count)
                leftLines.Add(new String(' ', leftWidth));

            while (rightLines.Count < leftLines.Count)
                rightLines.Add(new String(' ', rightWidth));

            if ((middle - nodeLabel.Length % 2 == 1) && (nodeLabel.Length < middle) /*&& (node.Parent != null && node.IsLeftChild)*/)
                nodeLabel += ".";

            // Format the node's label
            nodeLabel = nodeLabel.PadCenter(middle, '.');

            var nodeLabelChars = nodeLabel.ToCharArray();

            if (nodeLabelChars[0] == '.')
                nodeLabelChars[0] = ' ';

            if (nodeLabelChars[nodeLabelChars.Length - 1] == '.')
                nodeLabelChars[nodeLabelChars.Length - 1] = ' ';

            nodeLabel = String.Join("", nodeLabelChars);

            //
            // Construct the list of lines.
            string leftBranch = (node.leftChild != null) ? "/" : " ";
            string rightBranch = (node.rightChild != null) ? "\\" : " ";

            List<string> listOfLines = new List<string>()
            {
                // 0
                (new String(' ', leftPosition )) + nodeLabel + (new String(' ', (rightWidth - rightPosition))),

                // 1
                (new String(' ', leftPosition)) + leftBranch + (new String(' ', (middle - 2))) + rightBranch + (new String(' ', (rightWidth - rightPosition)))
            };

            //
            // Add the right lines and left lines to the final list of lines.
            listOfLines.AddRange(leftLines.Zip(rightLines, (leftLine, rightLine) =>
                            leftLine + (new String(' ', (width_out - leftWidth - rightWidth))) + rightLine));

            //
            // Return
            widthOutput = width_out;
            positionOutput = position_out;
            return listOfLines;
        }
        public static string PadCenter(this string text, int newWidth, char fillerCharacter = ' ')
        {
            if (string.IsNullOrEmpty(text))
                return text;

            int length = text.Length;
            int charactersToPad = newWidth - length;
            if (charactersToPad < 0) throw new ArgumentException("New width must be greater than string length.", "newWidth");
            int padLeft = charactersToPad / 2 + charactersToPad % 2;
            //add a space to the left if the string is an odd number
            int padRight = charactersToPad / 2;

            return new String(fillerCharacter, padLeft) + text + new String(fillerCharacter, padRight);
        }
        //private static Node<int> FindParentOfNode(Node<int> node)
        //{
        //    Node<int> parentNode = null;
        //    Node<int> nextNode = AVLLib.AVLTree<T>.

        //    if (node == null) return null;

        //    while (nextNode != null)
        //    {
        //        if (nextNode.data.CompareTo(node.data) > 0)
        //        {
        //            parentNode = nextNode;
        //            nextNode = nextNode.leftChild;
        //        }
        //        else if (nextNode.data.CompareTo(node.data) < 0)
        //        {
        //            parentNode = nextNode;
        //            nextNode = nextNode.rightChild;
        //        }
        //        else return parentNode;
        //    }
        //    return parentNode;
        //}

    }
}
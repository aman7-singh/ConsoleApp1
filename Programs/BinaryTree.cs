using System;
using System.Collections.Generic;
using System.Text;

namespace Programs
{
    public class BinaryTree
    {
        /* A binary tree node has key, pointer to
           left child and a pointer to right child */

        public class Node
        {
            public int key;
            public Node left, right;

            // constructor
            public Node(int key)
            {
                this.key = key;
                left = null;
                right = null;
            }
        }

        public static Node root;

        /* Inorder traversal of a binary tree*/

        public static void inorder(Node temp)
        {
            if (temp == null)
                return;

            inorder(temp.left);
            Console.Write(temp.key + " ");
            inorder(temp.right);
        }

        /*function to insert element in binary tree */

        public static void insert(Node temp, int key)
        {
            if (temp == null)
            {
                root = new Node(key);
                return;
            }
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(temp);

            // Do level order traversal until we find
            // an empty place.
            while (q.Count != 0)
            {
                temp = q.Peek();
                q.Dequeue();

                if (temp.left == null)
                {
                    temp.left = new Node(key);
                    break;
                }
                else
                    q.Enqueue(temp.left);

                if (temp.right == null)
                {
                    temp.right = new Node(key);
                    break;
                }
                else
                    q.Enqueue(temp.right);
            }
        }
    }
}

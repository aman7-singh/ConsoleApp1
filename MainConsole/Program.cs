using Programs;
using System;

namespace Programs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            char[][] board = new char[9][];
            board[0] = new Char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
            board[1] = new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
            board[2] = new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };

            board[3] = new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
            board[4] = new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
            board[5] = new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };

            board[6] = new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
            board[7] = new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
            board[8] = new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };

            Execute.SolveSudokus((board));
            Execute exe = new Execute();
            exe.SolveNQueens(4);

            var ratInMaze = new int[5][];
            ratInMaze[0] = new int[]{1,0,0,0,0};
            ratInMaze[1] = new int[]{1,1,1,1,1};
            ratInMaze[2] = new int[]{1,0,1,0,1};
            ratInMaze[3] = new int[]{1,1,1,0,1};
            ratInMaze[4] = new int[]{1,0,1,1,1};
            var ratPath = exe.RatInMaze(ratInMaze);

            var longetPath = new int[3][];
            longetPath[0] = new int[] { 9, 9, 4 };
            longetPath[1] = new int[] { 6, 6, 8 };
            longetPath[2] = new int[] { 2, 1, 2 };
            var lPath = exe.LongestPath(longetPath);


            var graph = GenericGraph<int>.BuildGraph();
            var graphStr = GenericGraph<int>.PrintGraph(graph);
            Console.WriteLine(graphStr);
            exe.BFS(graph.find(1), graph.find(6));

            var seq = exe.SequentialDigits(100, 300);

            var book = new int[] {2, 5, 6, 8, 10};
            exe.BookAllocation(book, 2);


            #region LinkedList

            //["MyLinkedList","addAtHead","addAtTail","addAtIndex","get","deleteAtIndex","get"]
            //[[],[1],[3],[1,2],[1],[1],[1]]

            var llist = new Execute.MyLinkedList();
            llist.AddAtHead(1);
            //llist.AddAtHead(2);
            llist.AddAtTail(3);
            //llist.AddAtTail(5);
            //llist.AddAtTail(7);
            //llist.AddAtTail(8);
            llist.AddAtIndex(1,2);
            //llist.AddAtIndex(5,6);
            //llist.AddAtIndex(1,16);
            //llist.AddAtIndex(4,12);
            llist.Get(0);
            llist.DeleteAtIndex(0);
            llist.Get(0);
            //llist.DeleteAtIndex(2);
            #endregion

            #region Tree

            var root = new BinaryTree.Node(10);
            root.left = new BinaryTree.Node(11);
            root.left.left = new BinaryTree.Node(7);
            root.right = new BinaryTree.Node(9);
            root.right.left = new BinaryTree.Node(15);
            root.right.right = new BinaryTree.Node(8);

            Console.Write("Inorder traversal before insertion:");
            BinaryTree.inorder(root);

            int key = 12;
            BinaryTree.insert(root, key);

            Console.Write("\nInorder traversal after insertion:");
            BinaryTree.inorder(root);

            #endregion
            Console.ReadLine();
        }
    }
}

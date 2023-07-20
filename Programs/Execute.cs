using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Programs
{
    public class Execute
    {
        #region Sudoku

        public static void SolveSudoku(char[][] board)
        {
            if (board == null || board.Length == 0) return;
            SolveSudokus(board);
        }
        public static bool SolveSudokus(char[][] board)
        {

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9 && c >= 0; c++)
                {
                    if (board[r][c] == '.')
                    {
                        for (char i = '1'; i <= '9'; i++)
                        {
                            if (isSafe(board, i, r, c))
                            {
                                board[r][c] = i;
                                if (SolveSudokus(board))
                                    return true;
                                else
                                    board[r][c] = '.';
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool isSafe(char[][] board, char i, int r, int c)
        {
            //col
            for (int x = 0; x < 9; x++)
            {
                if (board[r][x] == (char)i)
                {
                    return false;
                }
            }

            //row
            for (int x = 0; x < 9; x++)
            {
                if (board[x][c] == (Char)i)
                {
                    return false;
                }
            }

            //3*3
            int sx = (r / 3) * 3;
            int sy = (c / 3) * 3;

            for (int x = sx; x < sx + 3; x++)
            {
                for (int y = sy; y < sy + 3; y++)
                {
                    if (board[x][y] == (char)i)
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        #endregion

        #region Queen

        public IList<IList<string>> SolveNQueens(int n)
        {

            var res = new List<IList<string>>();
            for (int r = 0; r < n; r++)
            {
                var row = new List<string>();
                for (int c = 0; c < n; c++)
                {
                    row.Add(".");
                }
                res.Add(row);
            }

            SolveQ(n, res,0);
            return res;
        }

        public bool SolveQ(int n, List<IList<string>> qq, int r)
        {
            if (r == n)
            {
                return true;
            }
            //recursion
            for (int c = 0; c < n; c++)
            {
                if (isSafe(r, c, n, qq))
                {
                    qq[r][c] = "Q";
                    if (SolveQ(n, qq, r+1))
                    {
                        Console.WriteLine($"x:{r}, y:{c}");
                        return true;
                    }
                        
                    {
                        qq[r][c] = ".";
                    }
                }
            }
            return false;
        }

        bool isSafe(int r, int c, int n, List<IList<string>> qq)
        {
            for (int i = 0; i < r; i++)
            {
                if (qq[i][c] == "Q")
                {
                    return false;
                }
            }
            int ix = r, jx = c;
            while (ix >= 0 && jx >= 0)
            {
                if (qq[ix][jx] == "Q")
                {
                    return false;
                }
                ix--; jx--;
            }

            int iy = r;
            int jy = c;
            while (iy >= 0 && jy < n)
            {
                if (qq[iy][jy] == "Q")
                {
                    return false;
                }
                iy--; jy++;
            }

            return true;
        }

        #endregion

        #region Rat in Maze

        public List<string> RatInMaze(int[][] arr)
        {
            int len = arr.Length;
            var vis = new int[len][];
            for (int i = 0; i<len; i++)
            {
                vis[i] = new int[len];
                for (int j = 0; j < arr[0].Length; j++)
                {
                    vis[i][j] = 0;
                }
            }
            string s = "";
            var listStr = new List<string>();
            DFS(arr,len, s, 0,0,vis, listStr);

            return listStr;
        }

        void DFS(int[][] arr, int n, string s, int r, int c, int[][] vis, List<string> res)
        {
            if (r < 0 || c < 0 || r >= n || c >= n) return;
            if (arr[r][c] == 0 || vis[r][c] == 1) return;

            if (r == n-1 && c == n-1)
            {
                res.Add(s);
            }

            vis[r][c] = 1;

            //Recursion
            DFS(arr,n, s+"U", r-1, c,vis,res);
            DFS(arr,n, s+"R", r, c+1,vis,res);
            DFS(arr,n, s+"D", r+1, c,vis,res);
            DFS(arr,n, s+"L", r, c-1,vis,res);

            vis[r][c] = 0;
        }

        #endregion

        #region Longest Path

        public int LongestPath(int[][] arr)
        {
            int len = arr.Length;
            var vis = new int[len][];
            int res = 0;

            for (int i = 0; i < len; i++)
            {
                vis[i] = new int[len];
            }

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < arr[i]?.Length; j++)
                {
                    res = Math.Max(res, LIP(arr, len, i, j, vis));
                }
            }
            return res;
        }

        int LIP(int[][]arr, int n, int r, int c, int[][] mem)
        {
            if (r < 0 || c < 0 || r >= n || c >= n ) return 0;
            if (arr[r] == null) return 0;
            if (mem[r][c] > 0) return mem[r][c];
            int max=1;

            //Recursion
            if (r + 1 <  n && arr[r + 1] != null && arr[r+1][c] > arr[r][c]) max = Math.Max(max, 1 + LIP(arr, n, r + 1, c, mem));
            if (c + 1 <  n  && arr[r][c+1] > arr[r][c]) max = Math.Max(max, 1 + LIP(arr, n, r, c + 1, mem));
            if (c - 1 >= 0  && arr[r][c-1] > arr[r][c]) max = Math.Max(max, 1 + LIP(arr, n, r, c - 1, mem));
            if (r - 1 >= 0  && arr[r-1][c] > arr[r][c]) max = Math.Max(max, 1 + LIP(arr, n, r - 1, c, mem));

            mem[r][c] = max;
            return max;
        }

        #endregion

        #region 1291. Sequential Digits
        public IList<int> SequentialDigits(int low, int high)
        {
            var res = new List<int>();
            var qu = new Queue<int>();
            for (int i = 1; i < 10; i++)
            {
                qu.Enqueue(i);
            }

            while (qu.Count != 0)
            {
                var num = qu.Peek();
                qu.Dequeue();
                if (num >= low && num <= high)
                {
                    res.Add(num);
                }

                if (num > high) break;
                if (num % 10 < 9)
                {
                    int rem = num % 10;
                    qu.Enqueue(num * 10 + rem + 1);
                }
            }
            return res;
        }
        #endregion


        #region Graph BFS

        public void BFS(GraphNode<int> src, GraphNode<int> dest=null)
        {
            var qu = new Queue<GraphNode<int>>();
            var visited = new List<int>();
            var parent = new Dictionary<GraphNode<int>, GraphNode<int>>();
            var dist = new Dictionary<int,int>();
            qu.Enqueue(src);
            visited.Add(src.Value);
            parent.Add(src,src);//destination path
            dist.Add(src.Value,0);//sortest path from src
            while(qu.Count!=0)
            {
                var f = qu.Dequeue();
                Console.WriteLine(f.Value);
                foreach(var nbr in f.Neighbours)
                {
                    if(!visited.Contains(nbr.Value))
                    {
                        qu.Enqueue(nbr);
                        parent.Add(nbr,f); //destinationapath
                        dist.Add(nbr.Value, dist[f.Value] + 1);//sortest path from src
                        visited.Add(nbr.Value);
                    }
                }
            }

            foreach (var ele in dist)
            {
                Console.WriteLine($"{ele.Key} -> {ele.Value}");
            }

            if(dest !=null)
            {
                var temp = dest;
                while(temp != src)
                {
                    Console.Write(temp.Value + "--");
                    temp = parent[temp];
                }
                Console.Write(src.Value );
            }
        }

        #endregion

        #region Book allocation

        public int BookAllocation(int[] nums, int m)
        {

            int s = 0;
            int mid = 0;
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }

            int e = sum;
            int ans = -1;
            mid = s + (e - s) / 2;
            while (s <= e)
            {
                if (isPossible(nums, m, mid))
                {
                    ans = mid;
                    e = mid - 1;
                }
                else
                {
                    s = mid + 1;
                }

                mid = s + (e - s) / 2;
            }
            return ans;
        }

        public bool isPossible(int[] nums, int m, int mid)
        {
            int st = 1;
            int totalPages = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (totalPages + nums[i] <= mid)
                {
                    totalPages += nums[i];
                }
                else
                {
                    st++;
                    if (st > m || nums[i] > mid)
                    {
                        return false;
                    }
                    totalPages = nums[i];
                }
            }
            return true;
        }


        #endregion


        #region Linked List

        public class MyLinkedList
        {

            class Node
            {
                public int val;
                public Node next;
                public Node(int data)
                {
                    val = data;
                }
            }

            Node head;

            public MyLinkedList()
            {
                //head = new Node();
            }

            public int Get(int index)
            {
                var temp = head;
                int ind = 0;
                while (temp != null)
                {
                    if (ind == index)
                    {
                        return temp.val;
                    }
                    temp = temp.next;
                    ind++;
                }
                return -1;
            }

            public void AddAtHead(int val)
            {
                //var oldHead = head;
                var temp = new Node(val);
                temp.next = head;
                head = temp;
            }

            public void AddAtTail(int val)
            {
                if (head == null)
                {
                    AddAtHead(val);
                    return;
                }

                var temp = head;
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                var newNode = new Node(val);
                temp.next = newNode;
            }

            public void AddAtIndex(int index, int val)
            {
                if (index == 0)
                {
                    AddAtHead(val);
                    return;
                }
                var temp = head;
                int ind = 0;
                var newNode = new Node(val);
                while (temp != null)
                {
                    if (ind == index-1)
                    {
                        var ol = temp.next;
                        temp.next = newNode;
                        newNode.next = ol;
                        break;
                    }
                    temp = temp.next;
                    ind++;
                }
            }

            public void DeleteAtIndex(int index)
            {

                var temp = head;
                if (index == 0)
                {
                    head = head.next;
                    return;
                }
                int ind = 0;
                while (temp.next != null )
                {
                    if (ind == index-1)
                    {
                        temp.next = temp.next.next;
                        break;
                    }
                    temp = temp.next;
                    ind++;
                }

            }
        }

        #endregion

        #region  Tree
        
        public class Tree
        {
            // Driver code
            public static void Main(String[] args)
            {
                BinaryTree.root = new BinaryTree.Node(10);
                BinaryTree.root.left = new BinaryTree.Node(11);
                BinaryTree.root.left.left = new BinaryTree.Node(7);
                BinaryTree.root.right = new BinaryTree.Node(9);
                BinaryTree.root.right.left = new BinaryTree.Node(15);
                BinaryTree.root.right.right = new BinaryTree.Node(8);

                Console.Write("Inorder traversal before insertion:");
                BinaryTree.inorder(BinaryTree.root);

                int key = 12;
                BinaryTree.insert(BinaryTree.root, key);

                Console.Write("\nInorder traversal after insertion:");
                BinaryTree.inorder(BinaryTree.root);
            }
        }

        // This code is contributed by Rajput-Ji


    #endregion

}
}

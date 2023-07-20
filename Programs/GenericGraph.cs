using System;
using System.Collections.Generic;
using System.Text;

namespace Programs
{
    public class GraphNode<T>
    {
        List<GraphNode<T>> neighbours;
        T value;
        public GraphNode(T value)
        {
            this.value = value;
            this.neighbours = new List<GraphNode<T>>();
        }
        public List<GraphNode<T>> Neighbours { get { return neighbours; } }
        public T Value { get { return value; } }
        public bool AddNeighbour(GraphNode<T> neighbour)
        {
            if(Neighbours.Contains(neighbour))
            {
                return false;
            }
            else
            {
                Neighbours.Add(neighbour);
                return true;
            }
        }
        public bool RemoveNeighbour(GraphNode<T> neighbour)
        {
            if(Neighbours.Contains(neighbour))
            {
                Neighbours.Remove(neighbour);
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool RemoveAllNeighbours()
        {
            neighbours.Clear();
            return true;
        }
        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();

            nodeString.Append("[ Node value " + value + "with Neighbours: ");

            foreach(var neighbour in neighbours)
            {
                nodeString.Append(neighbour.value + " ");
            }
            nodeString.Append("]");

            return nodeString.ToString();
        }

    }
    public class GenericGraph<T>
    {
        List<GraphNode<T>> nodes ;
        public GenericGraph()
        {
            nodes = new List<GraphNode<T>>();
        }
        public List<GraphNode<T>> Nodes { get { return nodes; } }
        public int Count { get; }
        public bool AddNode(T value)
        {
            if(find(value)!=null)
            {
                return false;
            }
            else
            {
                Nodes.Add(new GraphNode<T>(value));
                return true;
            }
        }
        public bool AddEdges(T value1, T value2)
        {
            var fNode = find(value1);
            var sNode = find(value2);
            if(fNode == null)
            {
                return false;
            }
            else if(sNode == null)
            {
                return false;
            }
            else
            {
                //biDirectional Graph
                fNode.AddNeighbour(sNode);
                sNode.AddNeighbour(fNode);
            }
            return true;
        }
        public bool RemoveNode(T value)
        {
            var removeNode = find(value);
            if(removeNode == null)
            {
                return false;
            }
            else
            {
                nodes.Remove(removeNode);
                foreach(var node in nodes)
                {
                    node.RemoveNeighbour(removeNode);
                }
            }
            return true;
        }
        public bool RemoveEdge(T value1, T value2)
        {
            var node1 = find(value1);
            var node2 = find(value2);

            if(node1==null || node2 == null)
            {
                return false;
            }
            else if(!node1.Neighbours.Contains(node2))
            {
                return false;
            }
            else
            {
                node1.RemoveNeighbour(node2);
                node2.RemoveNeighbour(node1);
            }
            return true;
        }
        public void Clear()
        {
            foreach(var node in Nodes)
            {
                node.RemoveAllNeighbours();
            }

            nodes.Clear();
        }
        public override string ToString()
        {

            StringBuilder nodeString = new StringBuilder();

            foreach (var node in nodes)
            {
                nodeString.Append(node.ToString());
                nodeString.Append("\n");
            }

            return nodeString.ToString();
        }
        public GraphNode<T> find(T value)
        {
            foreach(var node in Nodes)
            {
                if(node.Value.Equals(value))
                {
                    return node;
                }
            }
            return null;
        }


        public static string PrintGraph(GenericGraph<int> graph)
        {
            return graph.ToString();
        }

        public static GenericGraph<int> BuildGraph()
        {
            GenericGraph<int> graph = new GenericGraph<int>();
            graph.AddNode(0);
            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);
            graph.AddNode(4);
            graph.AddNode(5);
            graph.AddNode(6);

            graph.AddEdges(1, 2);
            graph.AddEdges(2, 3);
            graph.AddEdges(3, 4);
            graph.AddEdges(3, 5);
            graph.AddEdges(5, 6);
            graph.AddEdges(4, 5);
            graph.AddEdges(0, 4);
            graph.AddEdges(0, 1);


            return graph;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLibrary
{
    public class Tree<T> : Node<T> where T : IComparable
    {
        public enum WalkType {UpDown, LeftRight, DownUp, ByLevels};

        public Tree(T  value)
        {
            this.Value = value;
            this.Parent = null;
            this.Tree = this;
        }


        public void ForEach(WalkType walkType, Action<Node<T>> action)
        {
            switch (walkType)
            {
                case WalkType.UpDown: UpDown(this, action); break;
                case WalkType.LeftRight: LeftRight(this, action); break;
                case WalkType.DownUp: DownUp(this, action); break;
                case WalkType.ByLevels: ByLevels(this,action); break;

            }
           
        }

        public void ForEach(WalkType walkType, Action<T> action)
        {
            ForEach(walkType, new Action<Node<T>>(x => action(x.Value)));
        }

        private void UpDown(Node<T> node, Action<Node<T>> action)
        {
            if (node != null)
            {
                action(node);
                
                for (int j = 0; j < node.ChildCount; j++)
                {
                    UpDown(node.ChildNodes[j], action);
                }

                
            }

        }

        private void DownUp(Node<T> node, Action<Node<T>> action)
        {
            if (node != null)
            {
                    for (int j =0;  j <node.ChildCount; j++)
                    {
                        DownUp(node.ChildNodes[j], action);
                    }
                action(node);

               
            }
        }

        private void LeftRight(Node<T> node, Action<Node<T>> action) 
        {
            if (node != null)
            {
                if (node.ChildCount >= 1)
                {
                    foreach (var child in node.ChildNodes)
                    {
                        if (child.Value.CompareTo(node.Value) > 0) LeftRight(child, action);
                    }
                }
                //action(node);

                if (node.ChildCount >= 1)
                {
                    foreach (var child in node.ChildNodes)
                    {
                        if (child.Value.CompareTo(node.Value) <= 0) LeftRight(child, action);
                    }
                }
                action(node);

            }
        }

        private void ByLevels(Node<T> node, Action<Node<T>> action) 
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(node);

            while (queue.Count != 0)
            {
                Node<T> curNode = queue.Dequeue();
                action(curNode);

                if (curNode.ChildCount >= 1)
                {
                    foreach (var child in curNode.ChildNodes)
                    {
                        queue.Enqueue(child);
                    }
                }

            }




        }


    }
}

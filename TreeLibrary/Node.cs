using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLibrary
{
    public class Node<T> where T : IComparable
    {
        public Node()
        {

        }
        public Node(T value)
        {
            this.Value = value;
        }

        private T value;
        private List<Node<T>> childNodes;
        private Node<T> parent;
        private Tree<T> tree;


        public int ChildCount
        {
            get
            {
                if (childNodes == null) return 0;
                else return childNodes.Count;
            }
        }
        public Node<T> Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (Cycle()) throw new TreeException("Cycles are not allowed!");
                else this.parent = value;
            }

        }
        public Tree<T> Tree
        {
            get
            {
                return tree;
            }
            set
            {
                this.tree = value;
            }
        }
        public List<Node<T>> ChildNodes
        {
            get
            {
                return childNodes;
            }
        }
        public T Value
        {
            get
            {
                return value;
            }

            set
            {
                if (value != null) this.value = value;
                else throw new TreeException("Nulll-value is not allowed!");
            }
        }



        public void Add(Node<T> node)
        {
            if (node != null)
            {
                node.Parent = this;
                node.Tree = this.Tree;
                if (childNodes == null)
                    childNodes = new List<Node<T>>();
                childNodes.Add(node);
            }
            else throw new TreeException("Null nodes are not allowed!");
            
        }

        public void Add(T value)
        {
            if (value != null)
            {
                Node<T> node = new Node<T>();
                node.value = value;


                Add(node);
            }
            else throw new TreeException("Null-value  nodes are not allowed!");
        }

        private void CountRemovedNodes(Node<T> node, ref int count)
        {

            if (ChildCount >= 1)
            {

                count += node.ChildCount;
                for (int i = 0; i < node.ChildCount; i++)
                { CountRemovedNodes(node.childNodes[i], ref count); }
            }
            
        }

        public int Remove()
        {
                int countRemovedNodes = 1;
                CountRemovedNodes(this, ref countRemovedNodes);
            if (ChildCount>=1)
                this.childNodes.Clear();

                if (this.Parent != null)// если не корень
                {
                    for (int i = 0; i < this.parent.childNodes.Count; i++)
                    {
                        if (this.Equals(this.parent.childNodes[i]))
                        {
                        this.parent.childNodes.Remove(this.parent.childNodes[i]);
                            break;
                        }
                    }
                }
                else //удаляется корень
                {
                   throw new TreeException("Cannot delete tree");
                }
                return countRemovedNodes;
           
        }

        public int RemoveAllChildren()
        {
            childNodes.Clear();
            return childNodes.Count;
        }

        public void SortNodes(IComparer<T> comparer)
        {
            childNodes.Sort(new Comparison<Node<T>>((x, y) => comparer.Compare(x.Value, y.Value)));
            
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this.Value.Equals(((Node<T>)obj).Value)) return false; //по значению
            else
                if (this.ChildCount != ((Node<T>)obj).ChildCount) return false; // по кол-ву потомков
            else
            {
                for (int i = 0; i < ((Node<T>)obj).ChildCount; i++) // Equals потомки?
                {
                    if (!((Node<T>)obj).childNodes[i].Equals(this.childNodes[i])) return false;

                }
            }
            return true;
        }

        public void ToStr(ref string resultStr)
        {
            
                if (ChildCount >= 1)// если есть потомки
                {
                    resultStr += value.ToString() + ": "; // узел-родитель

                    for (int i = 0; i < ChildCount; i++) //и его потомков
                    {
                        resultStr += childNodes[i].value.ToString() + ",  ";
                    }

                    resultStr += Environment.NewLine;

                    for (int i = 0; i < childNodes.Count; i++) // и вызвать метод печати для потомков узла
                    {
                        childNodes[i].ToStr(ref resultStr);
                    }
                }
                else if (parent == null) resultStr += value.ToString();
            



        }

        private bool Cycle()
        {
            if (parent != null ) return true;
            return false;
        }

    }
}

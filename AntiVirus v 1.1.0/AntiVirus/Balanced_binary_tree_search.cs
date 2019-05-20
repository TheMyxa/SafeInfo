using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiVirus
{
    /*
    * Сбалансированное бинарное дерево поиска 
    * Balanced_binary_tree_search  BBTS
    */

    class Balanced_binary_tree_search
    {
        //Node_Write NW = new Node_Write();
        class Node
        {
            public Node_Write.Virus data;
            public Node left;
            public Node right;
            public Node(Node_Write.Virus data) { this.data = data; }
        }

        Node root;

        public void Add(Node_Write.Virus data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }


        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (String.Compare(n.data.hash8, current.data.hash8, true) < 0)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (String.Compare(n.data.hash8, current.data.hash8, true) > 0)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }


        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }


        public void Delete(Node_Write.Virus target)
        {//and here
            root = Delete(root, target.hash8);
        }


        private Node Delete(Node current, string target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (String.Compare(target, current.data.hash8, true) < 0)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (String.Compare(target, current.data.hash8, true) > 0)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data.hash);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        // Поиск тела 8 байт
        public Node_Write.Virus? Find_body(string key)
        {
            Node_Write.Virus a = Find_body(key, root).data;
            if (String.Compare(a.byts_body, key, true) == 0)
            {
                return a;
            }
            else
            {
                return null;
            }
        }
        private Node Find_body(string target, Node current)
        {
            if (String.Compare(target, current.data.byts_body, true) < 0)
            {
                if (target == current.data.byts_body)
                {
                    return current;
                }
                else
                {
                    if (current.left != null) return Find_body(target, current.left);
                    else return current;
                }
            }
            else
            {
                if (target == current.data.byts_body)
                {
                    return current;
                }
                else
                {
                    if (current.right != null) return Find_body(target, current.right);
                    else return current;
                }
            }

        }

        public Node_Write.Virus? Find(string key)
        {
            Node_Write.Virus a = Find(key, root).data;
            if (String.Compare(a.hash8, key, true) == 0)
            {
                return a;
            }
            else
            {
                return null;
            }
        }

        private Node Find(string target, Node current)
        {
            if (String.Compare(target, current.data.hash8, true) < 0)
            {
                if (target == current.data.hash8)
                {
                    return current;
                }
                else
                {
                    if (current.left != null) return Find(target, current.left);
                    else return current;
                }
            }
            else
            {
                if (target == current.data.hash8)
                {
                    return current;
                }
                else
                {
                    if (current.right != null) return Find(target, current.right);
                    else return current;
                }
            }

        }


        private int max(int l, int r)
        {
            return l > r ? l : r;
        }


        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }


        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }


        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }


        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }


        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }


        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordTracker
{
    [Serializable]
    public class BSTree<Word>{
        private BSTreeNode<Word>? root;

        private int size;

        public BSTree(){
            root = null;
            size = 0;
        }

        public BSTreeNode<Word> getRoot(){
            if(root == null){
                throw new TreeException("Tree is empty 1");
            }
            return root;
        }

        public int getHeight(){
            return getHeight(root);
        }

        private int getHeight(BSTreeNode<Word> node){
            if(node == null){
                return 0;
            }
            return 1 + Math.Max(getHeight(node.left), getHeight(node.right));
        }

        public int treeSize(){
            return this.size;
        }

        public Boolean isEmpty(){
            return size == 0;
        }

        public void clear(){
            root = null;
            size = 0;
        }

        public BSTreeNode<Word> search(Word entry){
            if(root == null){
                throw new TreeException("Value not found");
            }
            return search(root, entry);
        }

        private BSTreeNode<Word> search(BSTreeNode<Word> root, Word entry){
            if(root == null){
                return null;
            }
            if(String.Compare(root.data.ToString(), entry.ToString()) == 0){
                return root;
            }
            if(String.Compare(root.data.ToString(), entry.ToString()) > 0){
                return search(root.left, entry);
            }
            return search(root.right, entry);
        }

        public Boolean contains(Word entry){
            if(root == null){
                throw new TreeException("Root is currently null");
            }
            return contains(root, entry);
        }

        private Boolean contains(BSTreeNode<Word> root, Word entry){
            if(root == null){
                return false;
            }
            if(String.Compare(root.data.ToString(), entry.ToString()) == 0){
                return true;
            }
            if(String.Compare(root.data.ToString(), entry.ToString()) > 0){
                return contains(root.left, entry);
            }
            return contains(root.right, entry);
        }

        public Boolean add(Word entry){
            if(entry == null){
                throw new NullReferenceException("Entry cannot be null");
            }
            try
            {
                if(contains(entry)){
                    return false;
                }
            }
            catch (TreeException)
            {
                //Do Nothing
            }
            root = add(root, entry);
            size++;
            return true;
        }

        private BSTreeNode<Word> add(BSTreeNode<Word> node, Word entry){
            if(node == null){
                return new BSTreeNode<Word>(entry);
            }
            if(String.Compare(node.data.ToString(), entry.ToString())> 0){
                node.left = add(node.left, entry);
            } else {
                node.right = add(node.right, entry);
            }
            return node;
        }

        public Iterator<Word> inorderIterator()
        {
            return new InorderIterator(root);
        }

        public Iterator<Word> preorderIterator(){
            return new PreorderIterator(root);
        }
        
        public Iterator<Word> postorderIterator(){
            return new PostorderIterator(root);
        }

        private class InorderIterator : Iterator<Word>
        {
            private Stack<BSTreeNode<Word>> stack;

            public InorderIterator(BSTreeNode<Word> root){
                stack = new Stack<BSTreeNode<Word>>();
                BSTreeNode<Word> current = root;
                while(current != null){
                    stack.Push(current);
                    current = current.left;
                }
            }
            public bool hasNext()
            {
                return stack.Any();
            }

            public Word next()
            {
                if(!hasNext()){
                    throw new Exception("No more elements in stack");
                }
                BSTreeNode<Word> current = stack.Pop();
                Word result = current.data;
                if(current.right != null){
                    current = current.right;
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.left;
                    }
                }
                return result;
            }
        }

        private class PreorderIterator : Iterator<Word>{
            private Stack<BSTreeNode<Word>> stack;

            public PreorderIterator(BSTreeNode<Word> root){
                stack = new Stack<BSTreeNode<Word>>();
                if (root != null)
                {
                    stack.Push(root);
                }
            }

            public bool hasNext(){
                return stack.Any();
            }

            public Word next(){
                if(!hasNext()){
                    throw new Exception("No more elements in stack");
                }

                BSTreeNode<Word> current = stack.Pop();
                Word result = current.data;
                if(current.right != null){
                    stack.Push(current.right);
                }
                if(current.left != null){
                    stack.Push(current.left);
                }
                return result;
            }
        }

        private class PostorderIterator : Iterator<Word>{
            private Stack<BSTreeNode<Word>> stack;

            private BSTreeNode<Word> lastVisited;

            public PostorderIterator(BSTreeNode<Word> root){
                stack = new Stack<BSTreeNode<Word>>();
                lastVisited = null;
                BSTreeNode<Word> current = root;
                while (current != null)
                {
                    stack.Push(current);
                    current = current.left;   
                }
            }

            public bool hasNext(){
                return stack.Any();
            }

            public Word next(){
                if(!hasNext()){
                    throw new Exception("No more elements in stack");
                }
                BSTreeNode<Word> current = stack.Peek();
                if(current.right == null || current.right == lastVisited){
                    lastVisited = stack.Pop();
                    return  lastVisited.data;
                }
                BSTreeNode<Word> temp = current.right;
                while (temp != null)
                {
                    stack.Push(temp);
                    temp = temp.left;
                }
                return next();
            }
        }

        public void printTree(int maxHeight){
            List<List<String>> tree = new List<List<String>>();
            int height = Math.Min(getHeight(root), maxHeight);
            int width = (int) Math.Pow(2, height) - 1;
            for(int i = 0; i < height; i++){
                List<String> level = new List<String>();
                for(int j = 0; j < width; j++){
                    level.Add(" ");
                }
                tree.Add(level);
            }
            populateTree(tree, root, 0, 0, width -1);
            foreach(List<String> level in tree){
                foreach (String s in level)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }
            if(getHeight() > maxHeight){
                Console.WriteLine("...");
                Console.WriteLine("Full tree height: " + getHeight());
            }
        }

        private void populateTree(List<List<String>> tree, BSTreeNode<Word> node, int level, int left, int right){
            if(node == null || level >= tree.Count){
                return;
            }
            int mid = (left + right) / 2;
            tree.ElementAt(level).Insert(mid, node.data.ToString());

            populateTree(tree, node.left, level + 1, left, mid -1);
            populateTree(tree, node.right, level + 1, mid + 1, right);
        }
    }
}
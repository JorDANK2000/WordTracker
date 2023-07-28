using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordTracker
{
    [Serializable]
    public class BSTreeNode<Word>{
        public Word data;
        public BSTreeNode<Word>? left;
        public BSTreeNode<Word>? right;
        public BSTreeNode(Word data){
            this.data = data;
            left = null;
            right = null;
        }
    }
}
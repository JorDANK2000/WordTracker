using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordTracker
{
    public interface Iterator<Word>
    {
        public Boolean hasNext();

        public Word next();
    }
}
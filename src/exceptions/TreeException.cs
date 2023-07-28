using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordTracker
{
    public class TreeException : Exception
    {
        /**
        * Constructor that takes in the error message
        * @param message - the error message
        */
        public TreeException(String message) {
            Console.WriteLine(message);
        }
    }
}
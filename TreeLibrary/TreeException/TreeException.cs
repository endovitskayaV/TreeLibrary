using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeLibrary
{
    public class TreeException : Exception
    {
        const string excMessage = "TreeException: ";

        public TreeException(string message)
            : base(String.Format("{0}{1}", excMessage, message))
        {
        }
    }
}


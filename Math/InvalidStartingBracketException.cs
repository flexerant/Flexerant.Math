using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class InvalidStartingBracketException : Exception
    {
        public InvalidStartingBracketException(string message) : base(message) { }
    }
}

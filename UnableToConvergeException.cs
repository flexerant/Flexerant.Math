using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class UnableToConvergeException : Exception
    {
        public UnableToConvergeException(string message) : base(message) { }
    }
}

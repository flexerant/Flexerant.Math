using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class ToleranceException : Exception
    {
        public ToleranceException(string message) : base(message)
        { }
    }
}

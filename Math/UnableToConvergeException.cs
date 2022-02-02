using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class UnableToConvergeException : Exception
    {
        public UnableToConvergeException(int iterationCount) : base($"Unable to converge within {iterationCount} iterations.")
        { }

        public UnableToConvergeException(uint iterationCount) : this(Convert.ToInt32(iterationCount))
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class RetryUnableToConvergeException : UnableToConvergeException
    {
        public RetryUnableToConvergeException(int iterationCount) : base(iterationCount)
        { }

        public RetryUnableToConvergeException(uint iterationCount) : base(Convert.ToInt32(iterationCount))
        { }
    }
}

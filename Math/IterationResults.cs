using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public class IterationResults<T> where T : struct
    {
        public int IterationCount { get; private set; } = 0;
        public Exception Exception { get; private set; } = null;
        public bool Converged { get; private set; } = false;
        public bool HasException => this.Exception != null;
        public int FuctionCallCount { get; set; } = 0;
        public T Value { get; private set; }

        public IterationResults(T value, int iterationCount, int functionCallCount, bool converged)
        {
            this.Value = value;
            this.IterationCount = iterationCount;
            this.FuctionCallCount = functionCallCount;
            this.Converged = converged;
        }

        public IterationResults(Exception exception)
        {
            this.Exception = exception;
        }
    }
}

using Flexerant.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class ExceptionTests
    {
        [Fact]
        public void RetryUnableToConvergeExceptionTest_Int()
        {
            int iteratations = 5;
            var ex = new RetryUnableToConvergeException(iteratations);

            Assert.Equal($"Unable to converge within {iteratations} iterations.", ex.Message);
        }

        [Fact]
        public void RetryUnableToConvergeExceptionTest_Uint()
        {
            uint iteratations = 3;
            var ex = new RetryUnableToConvergeException(iteratations);

            Assert.Equal($"Unable to converge within {iteratations} iterations.", ex.Message);
        }

        [Fact]
        public void UnableToConvergeExceptionTest_Int()
        {
            int iteratations = 5;
            var ex = new UnableToConvergeException(iteratations);

            Assert.Equal($"Unable to converge within {iteratations} iterations.", ex.Message);
        }

        [Fact]
        public void UnableToConvergeExceptionTest_Uint()
        {
            uint iteratations = 3;
            var ex = new UnableToConvergeException(iteratations);

            Assert.Equal($"Unable to converge within {iteratations} iterations.", ex.Message);
        }
    }
}

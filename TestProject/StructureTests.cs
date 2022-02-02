using Flexerant.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class StructureTests
    {
        [Fact]
        public void PointTests()
        {
            AssertPoint(0d, 0d, new Point());
            AssertPoint(1.0d, 2.0d, new Point(1.0d, 2.0d));
            AssertPoint(3.0m, 4.0m, new Point(3.0m, 4.0m));
            AssertPoint(5.0d, 6.0m, new Point(5.0d, 6.0m));
            AssertPoint(7.0m, 8.0d, new Point(7.0m, 8.0d));
        }

        private static void AssertPoint(double expectedX, double expectedY, Point point)
        {
            Assert.Equal(expectedX, point.X);
            Assert.Equal(expectedY, point.Y);
        }

        private static void AssertPoint(decimal expectedX, decimal expectedY, Point point)
        {
            Assert.Equal(Convert.ToDouble(expectedX), point.X);
            Assert.Equal(Convert.ToDouble(expectedY), point.Y);
        }

        private static void AssertPoint(double expectedX, decimal expectedY, Point point)
        {
            Assert.Equal(expectedX, point.X);
            Assert.Equal(Convert.ToDouble(expectedY), point.Y);
        }

        private static void AssertPoint(decimal expectedX, double expectedY, Point point)
        {
            Assert.Equal(Convert.ToDouble(expectedX), point.X);
            Assert.Equal(expectedY, point.Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Flexerant.Math;

namespace TestProject
{
    public class NumberExtensionTests
    {
        [Fact]
        public void ToDecimalNullable_In_Range()
        {
            var input = Math.PI;
            var output = input.ToDecimalNullable();

            Assert.Equal(3.14m, output.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void ToDecimalNullable_Too_High()
        {
            var input = double.MaxValue;
            var output = input.ToDecimalNullable();

            Assert.Null(output);
        }

        [Fact]
        public void ToDecimalNullable_Too_Low()
        {
            var input = double.MinValue;
            var output = input.ToDecimalNullable();

            Assert.Null(output);
        }

        [Fact]
        public void ToDecimal_In_Range()
        {
            var input = Math.PI;
            var expected = 3.14m;
            var actual = input.ToDecimal();

            Assert.Equal(expected, actual.WithDecimalPlaces(2));
        }

        [Fact]
        public void ToDecimal_Max()
        {
            var input = Convert.ToDouble(decimal.MaxValue);
            var expected = decimal.MaxValue;
            var actual = input.ToDecimal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDecimal_Min()
        {
            var input = Convert.ToDouble(decimal.MinValue);
            var expected = decimal.MinValue;
            var actual = input.ToDecimal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDecimal_Too_High()
        {
            var input = double.MaxValue;
            var expected = decimal.MaxValue;
            var actual = input.ToDecimal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDecimal_Too_Low()
        {
            var input = double.MinValue;
            var expected = decimal.MinValue;
            var actual = input.ToDecimal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDouble()
        {
            var input = Convert.ToDecimal(Math.PI);
            var output = input.ToDouble();

            Assert.Equal(3.14, output.WithDecimalPlaces(2));
        }

        [Fact]
        public void ToDouble_Max()
        {
            var input = decimal.MaxValue;
            var expected = Convert.ToDouble(decimal.MaxValue);
            var actual = input.ToDouble();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToDouble_Min()
        {
            var input = decimal.MinValue;
            var expected = Convert.ToDouble(decimal.MinValue);
            var actual = input.ToDouble();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToShort_In_Range()
        {
            var input = Math.PI;
            var expected = Convert.ToInt16(Math.PI);
            var actual = input.ToShort();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToShort_Max()
        {
            var input = Convert.ToDouble(short.MaxValue);
            var expected = short.MaxValue;
            var actual = input.ToShort();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToShort_Min()
        {
            var input = Convert.ToDouble(short.MinValue);
            var expected = short.MinValue;
            var actual = input.ToShort();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void WithDecimalPlaces_Double(int decimalPlaces)
        {
            var input = Math.PI;
            var expected = Math.Round(Math.PI, decimalPlaces);
            var actual = input.WithDecimalPlaces(decimalPlaces);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        public void WithDecimalPlaces_Decimal(int decimalPlaces)
        {
            var input = Convert.ToDecimal(Math.PI);
            var expected = Convert.ToDecimal(Math.Round(Math.PI, decimalPlaces));
            var actual = input.WithDecimalPlaces(decimalPlaces);

            Assert.Equal(expected, actual);
        }
    }
}

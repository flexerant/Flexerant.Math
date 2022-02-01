using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    public static class NumberExtensions
    {
        private readonly static double _maxDecimalValue = Convert.ToDouble(decimal.MaxValue);
        private readonly static double _minDecimalValue = Convert.ToDouble(decimal.MinValue);
        private readonly static double _maxShortValue = Convert.ToDouble(short.MaxValue);
        private readonly static double _minShortlValue = Convert.ToDouble(short.MinValue);

        public static decimal? ToDecimalNullable(this double value)
        {
            double v;

            if (TryGetValue(value, _maxDecimalValue, _minDecimalValue, out v))
            {
                return Convert.ToDecimal(v);
            }

            return null;
        }

        public static double ToDouble(this decimal value)
        {
            decimal v;

            if (TryGetValue(value, decimal.MaxValue, decimal.MinValue, out v))
            {
                return Convert.ToDouble(v);
            }

            throw new ArgumentOutOfRangeException($"The value {value} could not be converted to double.");
        }

        public static decimal ToDecimal(this double value)
        {
            double v;

            if (value >= _maxDecimalValue)
            {
                return decimal.MaxValue;
            }
            else if (value <= _minDecimalValue)
            {
                return decimal.MinValue;
            }

            if (TryGetValue(value, _maxDecimalValue, _minDecimalValue, out v))
            {
                return Convert.ToDecimal(v);
            }

            throw new ArgumentOutOfRangeException($"The value {value} could not be converted to decimal.");
        }

        public static short ToShort(this double value)
        {
            double v;

            if (TryGetValue(value, _maxShortValue, _minShortlValue, out v))
            {
                return Convert.ToInt16(v);
            }

            throw new ArgumentOutOfRangeException($"The value {value} could not be converted to short.");
        }

        private static bool TryGetValue(double value, double maxValue, double minValue, out double parsedValue)
        {
            parsedValue = default;

            if ((value <= maxValue) && (value >= minValue))
            {
                parsedValue = value;
                return true;
            }

            return false;
        }

        private static bool TryGetValue(decimal value, decimal maxValue, decimal minValue, out decimal parsedValue)
        {
            parsedValue = default;

            if ((value <= maxValue) && (value >= minValue))
            {
                parsedValue = value;
                return true;
            }

            return false;
        }

        public static decimal WithDecimalPlaces(this decimal value, int decimalPlaces)
        {
            return System.Math.Round(value, decimalPlaces);
        }

        public static double WithDecimalPlaces(this double value, int decimalPlaces)
        {
            return System.Math.Round(value, decimalPlaces);
        }
    }
}

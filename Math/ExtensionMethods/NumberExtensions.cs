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

        /// <summary>
        /// Converts to a nullable decimal
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(this double value)
        {            
            if (TryGetValue(value, _maxDecimalValue, _minDecimalValue, out double v))
            {
                return Convert.ToDecimal(v);
            }

            return null;
        }

        /// <summary>
        /// Coverts to double
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns></returns>
        public static double ToDouble(this decimal value)
        {            
            if (TryGetValue(value, decimal.MaxValue, decimal.MinValue, out decimal v))
            {
                return Convert.ToDouble(v);
            }

            throw new ArgumentOutOfRangeException($"The value {value} could not be converted to double.");
        }

        /// <summary>
        /// Converts to decimal
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns></returns>
        public static decimal ToDecimal(this double value)
        {            
            if (value >= _maxDecimalValue)
            {
                return decimal.MaxValue;
            }
            else if (value <= _minDecimalValue)
            {
                return decimal.MinValue;
            }

            if (TryGetValue(value, _maxDecimalValue, _minDecimalValue, out double v))
            {
                return Convert.ToDecimal(v);
            }

            throw new ArgumentOutOfRangeException($"The value {value} could not be converted to decimal.");
        }

        /// <summary>
        /// Converst to short
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns></returns>
        public static short ToShort(this double value)
        {            
            if (TryGetValue(value, _maxShortValue, _minShortlValue, out double v))
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

        /// <summary>
        /// Rounds the value to the nearst number of decimal places
        /// </summary>
        /// <param name="value">value to round</param>
        /// <param name="decimalPlaces">number of decimal places to round to</param>
        /// <returns></returns>
        public static decimal WithDecimalPlaces(this decimal value, int decimalPlaces)
        {
            return System.Math.Round(value, decimalPlaces);
        }

        /// <summary>
        /// Rounds the value to the nearst number of decimal places
        /// </summary>
        /// <param name="value">value to round</param>
        /// <param name="decimalPlaces">number of decimal places to round to</param>
        /// <returns></returns>
        public static double WithDecimalPlaces(this double value, int decimalPlaces)
        {
            return System.Math.Round(value, decimalPlaces);
        }
    }
}

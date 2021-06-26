using System;
using System.Collections.Generic;
using System.Text;

namespace Flexerant.Math
{
    static class ExtensionMethods
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

        public static decimal ToDecimal(this double value)
        {
            double v;

            if (TryGetValue(value, _maxDecimalValue, _minDecimalValue, out v))
            {
                return Convert.ToDecimal(v);
            }

            if (value > _maxDecimalValue) return decimal.MaxValue;

            return decimal.MinValue;
        }

        public static short ToShort(this double value)
        {
            double v;

            if (TryGetValue(value, _maxShortValue, _minShortlValue, out v))
            {
                return Convert.ToInt16(v);
            }

            return default;
        }

        private static bool TryGetValue(double value, double maxValue, double minValue, out double parsedValue)
        {
            parsedValue = default;

            if ((value < maxValue) && (value > minValue))
            {
                parsedValue = value;
                return true;
            }

            return false;
        }
    }
}

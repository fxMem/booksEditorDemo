using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public static class ValidationHelper
    {
        public static void CheckRequiredAndThrow(this string value, int maxLength = int.MaxValue, [CallerMemberName]string callerName = null)
        {
            if (!CheckRequired(value, maxLength))
            {
                throw new ArgumentException($"{callerName ?? "Property"} is requered and cannot exceed {maxLength} symbols!");
            }
        }

        public static void CheckOptionalAndThrow(this string value, int maxLength = int.MaxValue, [CallerMemberName]string callerName = null)
        {
            if (!CheckOptional(value, maxLength))
            {
                throw new ArgumentException($"{callerName ?? "Property"} cannot exceed {maxLength} symbols!");
            }
        }

        public static void CheckRangeAndThrow(this int value, int minValue = int.MinValue, int maxValue = int.MaxValue, [CallerMemberName]string callerName = null)
        {
            if (!CheckInRange(value, minValue, maxValue))
            {
                throw new ArgumentException($"Provided value {value} for {callerName ?? "Property"} is not in range {minValue}-{maxValue}!");
            }
        }

        public static bool CheckRequired(this string value, int maxLength = int.MaxValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return value.Length <= maxLength;
        }

        public static bool CheckOptional(this string value, int maxLength = int.MaxValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            return value.Length <= maxLength;
        }

        public static bool CheckInRange(this int value, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            return minValue <= value && value <= maxValue;
        }

        public static bool CheckRegex(this string value, string regex)
        {
            return Regex.IsMatch(value, regex);
        }
    }
}

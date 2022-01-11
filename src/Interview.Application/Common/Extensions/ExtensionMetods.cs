using Ardalis.GuardClauses;
using Interview.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common
{
   public static class ExtensionMetods
    {
        public static string RemoveUnit(this string str)
        {
            if (str.ToLower().EndsWith("km".ToLower())) return str.Replace("km", "");
            if (str.ToLower().EndsWith("mi".ToLower())) return str.Replace("mi", "");
            if (str.ToLower().EndsWith("%".ToLower())) return str.Replace("%", "");

            return str;
        }

        public static ValidationResult IsNullOrEmpty(this IGuardClause guardClause, string input, string paramName, string message)
        {
            if (string.IsNullOrEmpty(input))
                return ValidationResult.Fail(message);
            else
                return  ValidationResult.Success;
        }
    }
}

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
        public static bool IsNullOrEmpty(this IGuardClause guardClause, string input, string paramName, string message,out ValidationResult result)
        {
            result = default;
            if (string.IsNullOrEmpty(input))
            {
                result = ValidationResult.Fail(message);
                return true;
            }
            return false;

        }
    }
}

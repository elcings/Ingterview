﻿using Ardalis.GuardClauses;
using Interview.Application.Validations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common
{
    public static class ExtensionMetods
    {
        public static bool IsNullOrEmpty(this IGuardClause guardClause, string input, string paramName, string message, out ValidationResult result)
        {
            result = default;
            if (string.IsNullOrEmpty(input))
            {
                result = ValidationResult.Fail(message);
                return true;
            }
            return false;

        }


        public static string GetUserName(this IIdentity identity)
        {
            try
            {
                return identity.GetDetail<string>("UserName");
            }
            catch (Exception e)
            {
                // e.HandleException();
                return string.Empty;
            }
        }

        #region helper methods

        private static T GetDetail<T>(this IIdentity identity, string claimType) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (identity is ClaimsIdentity ci)
            {
                var id = ci.Claims.FirstOrDefault(x => x.Type == claimType);
                if (id != null)
                {
                    return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }

            return default(T);
        }

        #endregion

    }
}

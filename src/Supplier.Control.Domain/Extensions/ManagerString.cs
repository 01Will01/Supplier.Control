using Supplier.Control.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Supplier.Control.Domain.Extensions
{
    public static class ManagerString
    {
        public static string StandardizeSeparator(this string valueInput, string separator)
        {
            CheckParamterValidator.CheckIsNotNull(valueInput, nameof(valueInput));
            CheckParamterValidator.CheckIsNotNull(separator, nameof(separator));

            return valueInput
                         .Replace(" ", separator)
                         .Replace("_", separator)
                         .Replace(";", separator)
                         .Replace(",", separator)
                         .Replace(".", separator)
                         .Replace("{", separator)
                         .Replace("}", separator)
                         .Replace("[", separator)
                         .Replace("]", separator)
                         .Replace("=", separator)
                         .Replace("+", separator)
                         .Replace("-", separator)
                         .Replace("(", separator)
                         .Replace(")", separator)
                         .Replace("@", separator)
                         .Replace("\\", separator)
                         .Replace("/", separator)
                         .Replace("*", separator)
                         .Replace("&", separator)
                         .Replace(":", separator)
                         .Replace("|", separator);
        }

        public static string PatterCPF(this string value)
        {
            CheckParamterValidator.CheckIsNotNull(value, nameof(value));


            if (value.Length != 11)
                return value;

            return Convert.ToUInt64(value).ToString(@"000\.000\.000\-00");

        }

        public static string PatterCNPJ(this string value)
        {
            CheckParamterValidator.CheckIsNotNull(value, nameof(value));

            if (value.Length != 14)
                return value;

            return Convert.ToUInt64(value).ToString(@"00\.000\.000\.0000\-00");

        }
    }
}

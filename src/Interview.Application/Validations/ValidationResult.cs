using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Validations
{
    public class ValidationResult
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; }

        public static ValidationResult Success => new ValidationResult();
        public static ValidationResult Fail(string error) => new ValidationResult() { IsSuccess=false,ErrorMessage=error};
    }
}

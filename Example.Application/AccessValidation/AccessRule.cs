using System;
using System.Threading.Tasks;

namespace Example.Application.AccessValidation
{
    public class AccessRule
    {
        public Func<bool> RuleFunction { get; set; } = null!;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class AccessRuleAsync
    {
        public Func<Task<bool>> RuleFunction { get; set; } = null!;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

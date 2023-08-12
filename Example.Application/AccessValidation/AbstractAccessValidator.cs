using Example.Application.AccessValidation;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.AccessValidation
{
    public class AbstractAccessValidator<T> : IAccessValidator<T> where T : class
    {
        /// <summary>
        /// Сообщение об ошибке в случае непрохождения валидатора
        /// </summary>
        public string ErrorMessage { get; set; } = "У Вас недостаточно прав для выполнения данной команды";
        private List<AccessRule> Rules { get; set; } = new List<AccessRule>();
        private List<AccessRuleAsync> RulesAsunc { get; set; } = new List<AccessRuleAsync>();

        /// <summary>
        /// Команда CQRS
        /// </summary>
        public T Command { get; set; } = null!;
        public CancellationToken CancellationToken { get; set; }


        /// <summary>
        /// Валидация добавленных правил
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Valiadate(T command, CancellationToken cancellationToken)
        {
            Command = command;
            CancellationToken = cancellationToken;

            foreach (var rule in Rules)
            {
                if (!rule.RuleFunction.Invoke())
                {
                    ErrorMessage = rule.ErrorMessage;
                    return false;
                }
            }

            foreach (var rule in RulesAsunc)
            {
                if (cancellationToken.IsCancellationRequested) break;
                if (!await rule.RuleFunction.Invoke())
                {
                    ErrorMessage = rule.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Добавление правила валидации
        /// </summary>
        /// <param name="func">Функция валидации</param>
        /// <param name="errorMessage">Сообщение в случае НЕ прохождения валидации</param>
        protected void AddRule(Func<bool> func, string errorMessage)
        {
            AccessRule accessRule = new()
            {
                RuleFunction = func,
                ErrorMessage = errorMessage
            };
            Rules.Add(accessRule);
        }

        protected void AddRule(Func<Task<bool>> func, string errorMessage)
        {
            AccessRuleAsync accessRule = new()
            {
                RuleFunction = func,
                ErrorMessage = errorMessage
            };
            RulesAsunc.Add(accessRule);
        }

        /// <summary>
        /// Добавление правила валидации
        /// </summary>
        /// <param name="accessRule">Правило валидации</param>
        protected void AddRule(AccessRule accessRule)
        {
            Rules.Add(accessRule);
        }

    }
}

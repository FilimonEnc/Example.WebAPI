using System.Threading;
using System.Threading.Tasks;

namespace Example.Application.AccessValidation
{
    public interface IAccessValidator<T>
    {
        /// <summary>
        /// Сообщение в случае НЕ прохождения валидации
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Валидация данных
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Valiadate(T command, CancellationToken cancellationToken);
    }

}

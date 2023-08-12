using Example.Application.CQRS.Users.Commands.DeleteUser;
using Example.Application.CQRS.Users.Commands.RegistrationUser;
using Example.Application.CQRS.Users.Commands.UpdateUser;
using Example.Application.CQRS.Users.Queries.Authenticate;
using Example.Application.CQRS.Users.Queries.GetAll;
using Example.Application.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Example.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер работы с пользователями
    /// </summary>
    /// <response code="401">Пользователь не авторизаван</response>
    /// <response code="403">Недостаточно прав для выполнения этой команды</response>
    public class UsersController : BaseController
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UsersController(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Данные авторизованного пользователя</returns>
        /// <response code="200">Success</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CurrentUser>> Login(string login, string password)
        {
            var user = await Authenticate(login, password);
            user.Token = Generate(user);
            return user;
        }

        /// <summary>
        /// Получить хэш любой строки
        /// </summary>
        /// <param name="text"></param>
        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetMd5(string text)
        {
            using MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Регистрация нового сотрудника
        /// </summary>
        /// <param name="command">Данные запроса</param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<UserModel> RegistrationUser(RegistrationUserCommand command)
        {
            if (command.Password.Length < 8)
                throw new ValidationException("В пароле меньше 8 символов");

            using MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(command.Password));

            command.Password = Convert.ToBase64String(hashBytes);


            return await Mediator.Send(command);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<UserModel> DeleteUser(int UserId)
        {
            DeleteUserCommand command = new()
            {
                UserId = UserId
            };

            return await Mediator.Send(command);
        }

        /// <summary>
        /// получить всех работников
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            GetUsersQuery command = new();
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Изменить работника
        /// </summary>
        /// <param name="command">Данные команды</param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<UserModel> UpdateUser(UpdateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<CurrentUser> Authenticate(string login, string password)
        {
            using MD5 md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            string psw = Convert.ToBase64String(hashBytes);

            AuthenticateQuery command = new()
            {
                Login = login,
                Password = psw
            };
            return await Mediator.Send(command);
        }
    }
}


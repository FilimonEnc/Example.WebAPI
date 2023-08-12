using System;

namespace Example.Core.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class ExampleEntity : BaseEntity
    {
        /// <summary>
        /// Номер заказа
        /// </summary>
        public string ExampleName { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User
        {
            get => _user ?? throw new InvalidOperationException("Uninitialized property: " + nameof(User));
            set => _user = value;
        }
        private User? _user;

    }
}

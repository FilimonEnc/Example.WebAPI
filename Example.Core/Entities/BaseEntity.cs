using System;

namespace Example.Core.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}

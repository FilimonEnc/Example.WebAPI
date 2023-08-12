using System;

namespace Example.Application.Models.Base
{
    /// <summary>
    /// Базовый класс модели
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}

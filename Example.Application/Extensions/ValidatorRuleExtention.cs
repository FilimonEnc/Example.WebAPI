using Example.Core.Entities;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Example.Application.Extensions
{
    public static class ValidatorRuleExtention
    {

        public static IRuleBuilderOptions<T, TProperty> EntityExist<T, TProperty, TEntity>(this IRuleBuilder<T, TProperty> ruleBuilder, DbSet<TEntity> entities)
         where TEntity : BaseEntity
        {
            return ruleBuilder.MustAsync(async (id, cancellation) =>
            {
                if (id is not int entityId) return true;
                var entity = await entities.AsNoTracking().Where(x => x.Id == entityId).FirstOrDefaultAsync(cancellationToken: cancellation);
                if (entity == null) return false;

                return true;

            }).WithMessage((command, id) =>
            {
                if (id is not int) return "Необходимо передать числовое значение Id";
                return $"Сущность {typeof(TEntity)} с Id {id} не найдена в базе данных";
            });
        }
    }
}

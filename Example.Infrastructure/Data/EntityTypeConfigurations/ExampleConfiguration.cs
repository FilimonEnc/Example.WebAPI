using Example.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Infrastructure.Data.EntityTypeConfigurations
{
    public class ExampleConfiguration : IEntityTypeConfiguration<ExampleEntity>
    {
        public void Configure(EntityTypeBuilder<ExampleEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Examples)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

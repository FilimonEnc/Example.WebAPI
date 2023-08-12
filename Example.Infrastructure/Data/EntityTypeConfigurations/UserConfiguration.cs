using Example.Application.Models;
using Example.Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Infrastructure.Data.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Login);

            builder.Property(x => x.Role)
                .HasDefaultValue(UserRoles.User);

            builder.HasData(new User()
            {
                Id = 1,
                Login = "admin",
                Password = "ISMvKXpXpadDiUoOSoAfww==", //admin
                Role = "Admin",
            });



        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.API.Models;
namespace Movies.API.Mappings;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
           .IsRequired()
           .HasColumnName("Id")
           .ValueGeneratedOnAdd();
        builder.Property(x => x.Username)
              .IsRequired()
              .HasColumnType("VARCHAR")
              .HasColumnName("Username")
              .HasMaxLength(50);
        builder.Property(x => x.Password)
              .IsRequired()
              .HasColumnType("VARCHAR")
              .HasColumnName("Password")
              .HasMaxLength(100);
        builder.Property(x => x.Role)
              .IsRequired()
              .HasColumnType("VARCHAR")
              .HasColumnName("Role")
              .HasMaxLength(20)
              .HasDefaultValue("User");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.API.Models;

namespace Movies.API.Mappings;

public class MovieMap : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasColumnName("Title")
            .HasMaxLength(100);

        builder.Property(x => x.PosterUrl)
            .IsRequired() //nao pode ser nulo
            .HasColumnType("VARCHAR")
            .HasColumnName("PosterUrl")
            .HasMaxLength(255);

        builder.Property(x => x.Overview)
            .IsRequired() //nao pode ser nulo
            .HasColumnType("TEXT")
            .HasColumnName("Overview");


    }
}

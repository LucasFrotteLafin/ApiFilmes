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
            .IsRequired() 
            .HasColumnType("VARCHAR")
            .HasColumnName("PosterUrl")
            .HasMaxLength(255);
        builder.Property(x => x.Overview)
            .IsRequired()
            .HasColumnType("TEXT")
            .HasColumnName("Overview");
        builder.Property(x => x.Genre)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasColumnName("Genre")
            .HasMaxLength(50)
            .HasDefaultValue(string.Empty);
        builder.Property(x => x.Rating)
            .IsRequired()
            .HasColumnName("Rating")
            .HasDefaultValue(0.0);
        builder.Property(x => x.TrailerUrl)
            .HasColumnType("VARCHAR")
            .HasColumnName("TrailerUrl")
            .HasMaxLength(255)
            .HasDefaultValue(string.Empty);
    }
}
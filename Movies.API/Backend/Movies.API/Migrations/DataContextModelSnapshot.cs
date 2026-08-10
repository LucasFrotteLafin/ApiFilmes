using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.API.DatabaseContext;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
#nullable disable
namespace Movies.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);
            modelBuilder.Entity("Movies.API.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");
                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));
                    b.Property<int>("MovieId")
                        .HasColumnType("integer");
                    b.Property<int>("UserId")
                        .HasColumnType("integer");
                    b.HasKey("Id");
                    b.HasIndex("MovieId");
                    b.ToTable("Favorites", (string)null);
                });
            modelBuilder.Entity("Movies.API.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");
                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));
                    b.Property<string>("Genre")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasDefaultValue("")
                        .HasColumnName("Genre");
                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Overview");
                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("PosterUrl");
                    b.Property<double>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0)
                        .HasColumnName("Rating");
                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Title");
                    b.Property<string>("TrailerUrl")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasDefaultValue("")
                        .HasColumnName("TrailerUrl");
                    b.HasKey("Id");
                    b.ToTable("Movies", (string)null);
                });
            modelBuilder.Entity("Movies.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");
                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));
                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Password");
                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR")
                        .HasDefaultValue("User")
                        .HasColumnName("Role");
                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Username");
                    b.HasKey("Id");
                    b.ToTable("users", (string)null);
                });
            modelBuilder.Entity("Movies.API.Models.Favorite", b =>
                {
                    b.HasOne("Movies.API.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    b.Navigation("Movie");
                });
#pragma warning restore 612, 618
        }
    }
}
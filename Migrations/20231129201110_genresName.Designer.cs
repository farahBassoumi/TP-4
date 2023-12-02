﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tp.Data;

#nullable disable

namespace tp.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231129201110_genresName")]
    partial class genresName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CustomersMovies", b =>
                {
                    b.Property<Guid>("CustomersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CustomersId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CustomersMovies");
                });

            modelBuilder.Entity("tp.Models.Customers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("membershipTypeIdId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("membershipTypeIdId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("tp.Models.Genres", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("tp.Models.Membershiptype", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("DiscountRate")
                        .HasColumnType("real");

                    b.Property<DateTime>("DurationInMonth")
                        .HasColumnType("datetime2");

                    b.Property<int>("SignUpFee")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Membershiptype");
                });

            modelBuilder.Entity("tp.Models.Movies", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Genre_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Genre_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("CustomersMovies", b =>
                {
                    b.HasOne("tp.Models.Customers", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tp.Models.Movies", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tp.Models.Customers", b =>
                {
                    b.HasOne("tp.Models.Membershiptype", "membershipTypeId")
                        .WithMany("Customers")
                        .HasForeignKey("membershipTypeIdId");

                    b.Navigation("membershipTypeId");
                });

            modelBuilder.Entity("tp.Models.Movies", b =>
                {
                    b.HasOne("tp.Models.Genres", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("tp.Models.Genres", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("tp.Models.Membershiptype", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}

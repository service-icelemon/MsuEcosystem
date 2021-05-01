﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

namespace Persistence.Migrations.MsuLibrary
{
    [DbContext(typeof(MsuLibraryContext))]
    [Migration("20210429144020_added edition image")]
    partial class addededitionimage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entitties.Library.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Domain.Entitties.Library.BorrowedEdition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EditionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReaderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EditionId");

                    b.ToTable("BorrowedEditions");
                });

            modelBuilder.Entity("Domain.Entitties.Library.Edition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvaibleAmount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GenreId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishingHouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PublishingYear")
                        .HasColumnType("int");

                    b.Property<string>("TypeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.HasIndex("PublishingHouseId");

                    b.HasIndex("TypeId");

                    b.ToTable("Editions");
                });

            modelBuilder.Entity("Domain.Entitties.Library.EditionType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EditionTypes");
                });

            modelBuilder.Entity("Domain.Entitties.Library.Genre", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Domain.Entitties.Library.PublishingHouse", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PublishingHouses");
                });

            modelBuilder.Entity("Domain.Entitties.Library.BorrowedEdition", b =>
                {
                    b.HasOne("Domain.Entitties.Library.Edition", "Edition")
                        .WithMany()
                        .HasForeignKey("EditionId");

                    b.Navigation("Edition");
                });

            modelBuilder.Entity("Domain.Entitties.Library.Edition", b =>
                {
                    b.HasOne("Domain.Entitties.Library.Author", "Author")
                        .WithMany("Editions")
                        .HasForeignKey("AuthorId");

                    b.HasOne("Domain.Entitties.Library.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.HasOne("Domain.Entitties.Library.PublishingHouse", "PublishingHouse")
                        .WithMany()
                        .HasForeignKey("PublishingHouseId");

                    b.HasOne("Domain.Entitties.Library.EditionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Author");

                    b.Navigation("Genre");

                    b.Navigation("PublishingHouse");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.Entitties.Library.Author", b =>
                {
                    b.Navigation("Editions");
                });
#pragma warning restore 612, 618
        }
    }
}

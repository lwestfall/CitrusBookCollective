﻿// <auto-generated />
using System;
using Cbc.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cbc.WebApi.Migrations
{
    [DbContext(typeof(CbcDbContext))]
    [Migration("20231215154822_IncreaseDescriptionMaxLength")]
    partial class IncreaseDescriptionMaxLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<string>("Isbn")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int?>("PageCount")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbnailLink")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("UserEmail");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.BookRecommendation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MeetingId")
                        .HasColumnType("uuid");

                    b.Property<string>("MemberEmail")
                        .IsRequired()
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("MemberEmail");

                    b.ToTable("BookRecommendation", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.BookVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MeetingId")
                        .HasColumnType("uuid");

                    b.Property<string>("MemberEmail")
                        .IsRequired()
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("MeetingId");

                    b.HasIndex("MemberEmail");

                    b.ToTable("BookVote", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.Meeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("WinningBookId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WinningBookId")
                        .IsUnique();

                    b.ToTable("Meeting", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.User", b =>
                {
                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("EmailAddress");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.UserRole", b =>
                {
                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("EmailAddress", "Role");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.Book", b =>
                {
                    b.HasOne("Cbc.WebApi.Models.Entities.User", "User")
                        .WithMany("Books")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.BookRecommendation", b =>
                {
                    b.HasOne("Cbc.WebApi.Models.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cbc.WebApi.Models.Entities.User", "RecommendedBy")
                        .WithMany()
                        .HasForeignKey("MemberEmail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("RecommendedBy");
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.BookVote", b =>
                {
                    b.HasOne("Cbc.WebApi.Models.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cbc.WebApi.Models.Entities.Meeting", null)
                        .WithMany("Votes")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cbc.WebApi.Models.Entities.User", "VoteBy")
                        .WithMany()
                        .HasForeignKey("MemberEmail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("VoteBy");
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.Meeting", b =>
                {
                    b.HasOne("Cbc.WebApi.Models.Entities.Book", "WinningBook")
                        .WithOne()
                        .HasForeignKey("Cbc.WebApi.Models.Entities.Meeting", "WinningBookId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("WinningBook");
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.UserRole", b =>
                {
                    b.HasOne("Cbc.WebApi.Models.Entities.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("EmailAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.Meeting", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("Cbc.WebApi.Models.Entities.User", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}

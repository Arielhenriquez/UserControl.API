﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UserControl.Model.Context;

#nullable disable

namespace UserControl.Model.Migrations
{
    [DbContext(typeof(UserContactDbContext))]
    [Migration("20250323040422_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UserControl.Model.Entities.PhoneEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CityCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("UserControl.Model.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastLogin")
                        .HasColumnType("timestamptz");

                    b.Property<DateTimeOffset?>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserControl.Model.Entities.PhoneEntity", b =>
                {
                    b.HasOne("UserControl.Model.Entities.UserEntity", "UserEntity")
                        .WithMany("Phones")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("UserControl.Model.Entities.UserEntity", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}

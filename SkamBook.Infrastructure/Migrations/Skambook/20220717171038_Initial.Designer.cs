﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkamBook.Infrastructure.Context;

#nullable disable

namespace SkamBook.Infrastructure.Migrations.Skambook
{
    [DbContext(typeof(SkamBookContext))]
    [Migration("20220717171038_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SkamBook.Core.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Lat")
                        .HasColumnType("bigint");

                    b.Property<long>("Lon")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxDistance")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SkamBook.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SkamBook.Core.Entities.User", b =>
                {
                    b.HasOne("SkamBook.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SkamBook.Core.Entities.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Email")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

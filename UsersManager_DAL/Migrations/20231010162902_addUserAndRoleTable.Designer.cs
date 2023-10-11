﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsersManager_DAL.Context;

#nullable disable

namespace UsersManager_DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231010162902_addUserAndRoleTable")]
    partial class addUserAndRoleTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UsersManager_DAL.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("331a054c-85d3-4a8e-a8e9-0cc677a9041c"),
                            Name = "User"
                        },
                        new
                        {
                            Id = new Guid("6ff039b9-b373-4443-a7ed-0cea1a341f2f"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("94ba79e1-ca12-4669-a1bd-94a8a2e5c8da"),
                            Name = "Support"
                        },
                        new
                        {
                            Id = new Guid("b4200fb1-be7c-48bb-adfa-dc8abead32d9"),
                            Name = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("UsersManager_DAL.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("UsersManager_DAL.Domain.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("UsersManager_DAL.Domain.UserRole", b =>
                {
                    b.HasOne("UsersManager_DAL.Domain.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UsersManager_DAL.Domain.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UsersManager_DAL.Domain.Role", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("UsersManager_DAL.Domain.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

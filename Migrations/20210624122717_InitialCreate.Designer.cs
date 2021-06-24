﻿// <auto-generated />
using System;
using EfSqlForeignKeyBug;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EfSqlForeignKeyBug.Migrations
{
    [DbContext(typeof(ReproContext))]
    [Migration("20210624122717_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("EfSqlForeignKeyBug.AccessCodeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("code");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<Guid?>("ParentAccessCodeId")
                        .HasColumnType("TEXT")
                        .HasColumnName("parentAccessCodeId");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("ParentAccessCodeId");

                    b.ToTable("access_code");
                });

            modelBuilder.Entity("EfSqlForeignKeyBug.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AccessCodeId")
                        .HasColumnType("TEXT")
                        .HasColumnName("accessCodeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("AccessCodeId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("EfSqlForeignKeyBug.AccessCodeEntity", b =>
                {
                    b.HasOne("EfSqlForeignKeyBug.AccessCodeEntity", "ParentAccessCode")
                        .WithMany("ChildAccessCodes")
                        .HasForeignKey("ParentAccessCodeId");

                    b.Navigation("ParentAccessCode");
                });

            modelBuilder.Entity("EfSqlForeignKeyBug.UserEntity", b =>
                {
                    b.HasOne("EfSqlForeignKeyBug.AccessCodeEntity", "AccessCode")
                        .WithMany("Users")
                        .HasForeignKey("AccessCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessCode");
                });

            modelBuilder.Entity("EfSqlForeignKeyBug.AccessCodeEntity", b =>
                {
                    b.Navigation("ChildAccessCodes");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

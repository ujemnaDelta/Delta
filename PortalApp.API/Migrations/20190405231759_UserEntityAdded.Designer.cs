﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalApp.API.Data;

namespace PortalApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190405231759_UserEntityAdded")]
    partial class UserEntityAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("PortalApp.API.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserName");

                    b.Property<byte[]>("UserPasswordHash");

                    b.Property<byte[]>("UserPasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortalApp.API.Models.ValueModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });
#pragma warning restore 612, 618
        }
    }
}

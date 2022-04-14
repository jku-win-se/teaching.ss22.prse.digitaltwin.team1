﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartRoom.TransDataService.Persistence;

#nullable disable

namespace SmartRoom.TransDataService.Migrations
{
    [DbContext(typeof(TransDataDBContext))]
    partial class TransDataDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartRoom.CommonBase.Core.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EntityRefID")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("State");

                    b.HasDiscriminator<string>("Discriminator").IsComplete(false).HasValue("State");
                });

            modelBuilder.Entity("SmartRoom.CommonBase.Core.Entities.BinaryState", b =>
                {
                    b.HasBaseType("SmartRoom.CommonBase.Core.Entities.State");

                    b.Property<bool>("BinaryValue")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("BinaryState");
                });

            modelBuilder.Entity("SmartRoom.CommonBase.Core.Entities.MeasureState", b =>
                {
                    b.HasBaseType("SmartRoom.CommonBase.Core.Entities.State");

                    b.Property<double>("MeasureValue")
                        .HasColumnType("double precision");

                    b.HasDiscriminator().HasValue("MeasureState");
                });
#pragma warning restore 612, 618
        }
    }
}

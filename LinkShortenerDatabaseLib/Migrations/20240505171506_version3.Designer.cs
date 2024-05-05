﻿// <auto-generated />
using System;
using LinkShortenerDatabaseLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LinkShortenerDatabaseLib.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240505171506_version3")]
    partial class version3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LinkShortenerDatabaseLib.Entities.IPStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ip");

                    b.Property<DateTime>("LastRequest")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_request");

                    b.Property<int>("RequestsCount")
                        .HasColumnType("integer")
                        .HasColumnName("requests_count");

                    b.HasKey("Id");

                    b.HasIndex("Ip")
                        .IsUnique();

                    b.ToTable("ip_stats");
                });

            modelBuilder.Entity("LinkShortenerDatabaseLib.Entities.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_at");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date");

                    b.Property<int>("MaximumTransitionsCount")
                        .HasColumnType("integer")
                        .HasColumnName("maximum_transitions_count");

                    b.Property<string>("NewLink")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("new_link");

                    b.Property<int>("NumberOfTransitions")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_transitions");

                    b.Property<string>("OldLink")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("old_link");

                    b.HasKey("Id");

                    b.HasIndex("NewLink")
                        .IsUnique();

                    b.ToTable("links");
                });

            modelBuilder.Entity("LinkShortenerDatabaseLib.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ip");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time");

                    b.HasKey("Id");

                    b.ToTable("requests");
                });
#pragma warning restore 612, 618
        }
    }
}
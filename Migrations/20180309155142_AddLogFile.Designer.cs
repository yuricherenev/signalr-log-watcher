﻿// <auto-generated />
using LogWatcher.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LogWatcher.Migrations
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20180309155142_AddLogFile")]
    partial class AddLogFile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LogWatcher.Models.LogFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.HasKey("Id");

                    b.ToTable("LogFiles");
                });

            modelBuilder.Entity("LogWatcher.Models.LogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Header")
                        .HasMaxLength(255);

                    b.Property<Guid?>("LogFileId");

                    b.HasKey("Id");

                    b.HasIndex("LogFileId");

                    b.ToTable("LogItem");
                });

            modelBuilder.Entity("LogWatcher.Models.LogItem", b =>
                {
                    b.HasOne("LogWatcher.Models.LogFile")
                        .WithMany("Logs")
                        .HasForeignKey("LogFileId");
                });
#pragma warning restore 612, 618
        }
    }
}

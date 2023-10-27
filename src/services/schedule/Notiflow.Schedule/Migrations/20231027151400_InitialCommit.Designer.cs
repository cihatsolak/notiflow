﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notiflow.Schedule.Infrastructure.Data;

#nullable disable

namespace Notiflow.Schedule.Migrations
{
    [DbContext(typeof(ScheduledDbContext))]
    [Migration("20231027151400_InitialCommit")]
    partial class InitialCommit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Notiflow.Schedule.Infrastructure.Entities.ScheduledEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastAttemptDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SuccessDeliveryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ScheduledEmail", null, t =>
                        {
                            t.HasCheckConstraint("CK_FailedAttempts_Max", "[FailedAttempts] <= 3");

                            t.HasCheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate", "[LastAttemptDate] > [PlannedDeliveryDate]");

                            t.HasCheckConstraint("CK_PlannedDeliveryDate_Min", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())");
                        });
                });

            modelBuilder.Entity("Notiflow.Schedule.Infrastructure.Entities.ScheduledNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastAttemptDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SuccessDeliveryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ScheduledNotification", null, t =>
                        {
                            t.HasCheckConstraint("CK_FailedAttempts_Max", "[FailedAttempts] <= 3")
                                .HasName("CK_FailedAttempts_Max1");

                            t.HasCheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate", "[LastAttemptDate] > [PlannedDeliveryDate]")
                                .HasName("CK_LastAttemptDate_PlannedDeliveryDate1");

                            t.HasCheckConstraint("CK_PlannedDeliveryDate_Min", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())")
                                .HasName("CK_PlannedDeliveryDate_Min1");
                        });
                });

            modelBuilder.Entity("Notiflow.Schedule.Infrastructure.Entities.ScheduledTextMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastAttemptDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SuccessDeliveryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ScheduledTextMessage", null, t =>
                        {
                            t.HasCheckConstraint("CK_FailedAttempts_Max", "[FailedAttempts] <= 3")
                                .HasName("CK_FailedAttempts_Max2");

                            t.HasCheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate", "[LastAttemptDate] > [PlannedDeliveryDate]")
                                .HasName("CK_LastAttemptDate_PlannedDeliveryDate2");

                            t.HasCheckConstraint("CK_PlannedDeliveryDate_Min", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())")
                                .HasName("CK_PlannedDeliveryDate_Min2");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

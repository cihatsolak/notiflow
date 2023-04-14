﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notiflow.Backoffice.Persistence.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notiflow.Backoffice.Persistence.Migrations
{
    [DbContext(typeof(NotiflowDbContext))]
    [Migration("20230414202820_InitialCommit")]
    partial class InitialCommit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<bool>("IsBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("MarriageStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("character(10)")
                        .IsFixedLength();

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Devices.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CloudMessagePlatform")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("OSVersion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(180)
                        .IsUnicode(false)
                        .HasColumnType("character varying(180)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("device", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.EmailHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bcc")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("Cc")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Recipients")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("emailhistory", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.NotificationHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("notificationhistory", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.TextMessageHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<bool>("IsSent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Message")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("textmessagehistory", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AppId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("uuid")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("tenant", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.TenantApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("FirebaseSenderId")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("character(11)")
                        .IsFixedLength();

                    b.Property<string>("FirebaseServerKey")
                        .IsRequired()
                        .HasMaxLength(152)
                        .IsUnicode(false)
                        .HasColumnType("character(152)")
                        .IsFixedLength();

                    b.Property<string>("HuaweiSenderId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("character(12)")
                        .IsFixedLength();

                    b.Property<string>("HuaweiServerKey")
                        .IsRequired()
                        .HasMaxLength(44)
                        .IsUnicode(false)
                        .HasColumnType("character(44)")
                        .IsFixedLength();

                    b.Property<string>("MailFromAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("MailFromName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("MailReplyAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("tenantapplication", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.TenantPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("IsSendEmailPermission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSendMessagePermission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSendNotificationPermission")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("tenantpermission", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("character varying(25)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Customers.Customer", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Customers")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Devices.Device", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithOne("Device")
                        .HasForeignKey("Notiflow.Backoffice.Domain.Entities.Devices.Device", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.EmailHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("EmailHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.NotificationHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("NotificationHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.TextMessageHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("TextMessageHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.TenantApplication", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithOne("TenantApplication")
                        .HasForeignKey("Notiflow.Backoffice.Domain.Entities.Tenants.TenantApplication", "TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.TenantPermission", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithOne("TenantPermission")
                        .HasForeignKey("Notiflow.Backoffice.Domain.Entities.Tenants.TenantPermission", "TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Users.User", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Customers.Customer", b =>
                {
                    b.Navigation("Device");

                    b.Navigation("EmailHistories");

                    b.Navigation("NotificationHistories");

                    b.Navigation("TextMessageHistories");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Tenants.Tenant", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("TenantApplication");

                    b.Navigation("TenantPermission");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

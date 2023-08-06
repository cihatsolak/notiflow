﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notiflow.Backoffice.Persistence.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notiflow.Backoffice.Persistence.Migrations
{
    [DbContext(typeof(NotiflowDbContext))]
    partial class NotiflowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birth_date")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(130)
                        .IsUnicode(false)
                        .HasColumnType("character varying(130)")
                        .HasColumnName("email");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_blocked");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("MarriageStatus")
                        .HasColumnType("integer")
                        .HasColumnName("marriage_status");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("character(10)")
                        .HasColumnName("phone_number")
                        .IsFixedLength();

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(75)
                        .IsUnicode(false)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("surname");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.HasIndex("Email", "CreatedDate", "TenantId")
                        .IsDescending(false, true, false)
                        .HasDatabaseName("ix_customer_email_created_date_tenant_id");

                    b.HasIndex("PhoneNumber", "CreatedDate", "TenantId")
                        .IsDescending(false, true, false)
                        .HasDatabaseName("ix_customer_phone_number_created_date_tenant_id");

                    b.ToTable("customer", null, t =>
                        {
                            t.HasCheckConstraint("chk_email_format", "email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$'");

                            t.HasCheckConstraint("chk_gender_value_limitation", "gender IN (1,2)");

                            t.HasCheckConstraint("chk_marriage_status_value_limitation", "marriage_status IN (1,2)");

                            t.HasCheckConstraint("chk_minimum_age_restriction", "birth_date >= '1950-01-01'");

                            t.HasCheckConstraint("chk_phone_number_turkey", "phone_number ~ '^50|53|54|55|56\\d{8}$'");

                            t.HasCheckConstraint("chk_tenant_id_restriction", "tenant_id > 0");
                        });
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Devices.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CloudMessagePlatform")
                        .HasColumnType("integer")
                        .HasColumnName("cloud_message_platform");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<int>("OSVersion")
                        .HasColumnType("integer")
                        .HasColumnName("os_version");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("token");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id")
                        .HasName("pk_device");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasDatabaseName("ix_device_customer_id");

                    b.ToTable("device", null, t =>
                        {
                            t.HasCheckConstraint("chk_cloud_message_platform_value_limitation", "cloud_message_platform IN (1,2)");

                            t.HasCheckConstraint("chk_os_version_value_limitation", "os_version IN (1,2, 3, 4)");
                        });
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.EmailHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bcc")
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("bcc");

                    b.Property<string>("Body")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<string>("Cc")
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("cc");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("error_message");

                    b.Property<bool>("IsBodyHtml")
                        .HasColumnType("boolean")
                        .HasColumnName("is_body_html");

                    b.Property<bool>("IsSent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sent");

                    b.Property<string>("Recipients")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("recipients");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("sent_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("subject");

                    b.HasKey("Id")
                        .HasName("pk_emailhistory");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_emailhistory_customer_id");

                    b.ToTable("emailhistory", null, t =>
                        {
                            t.HasCheckConstraint("chk_emailhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");

                            t.HasCheckConstraint("chk_sent_date", "sent_date >= NOW() - INTERVAL '30 minutes'");
                        });
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.NotificationHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("error_message");

                    b.Property<bool>("IsSent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sent");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("message");

                    b.Property<Guid>("SenderIdentity")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("uuid")
                        .HasColumnName("sender_identity")
                        .IsFixedLength();

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("sent_date")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(true)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_notificationhistory");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_notificationhistory_customer_id");

                    b.ToTable("notificationhistory", null, t =>
                        {
                            t.HasCheckConstraint("chk_notificationhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
                        });
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.TextMessageHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<string>("ErrorMessage")
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("error_message");

                    b.Property<bool>("IsSent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_sent");

                    b.Property<string>("Message")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text")
                        .HasColumnName("message");

                    b.Property<DateTime>("SentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("sent_date")
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id")
                        .HasName("pk_textmessagehistory");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_textmessagehistory_customer_id");

                    b.ToTable("textmessagehistory", null, t =>
                        {
                            t.HasCheckConstraint("chk_textmessagehistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
                        });
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Devices.Device", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithOne("Device")
                        .HasForeignKey("Notiflow.Backoffice.Domain.Entities.Devices.Device", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_device_customer_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.EmailHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("EmailHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_emailhistory_customer_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.NotificationHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("NotificationHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_notificationhistory_customer_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Histories.TextMessageHistory", b =>
                {
                    b.HasOne("Notiflow.Backoffice.Domain.Entities.Customers.Customer", "Customer")
                        .WithMany("TextMessageHistories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_textmessagehistory_customer_customer_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Customers.Customer", b =>
                {
                    b.Navigation("Device");

                    b.Navigation("EmailHistories");

                    b.Navigation("NotificationHistories");

                    b.Navigation("TextMessageHistories");
                });
#pragma warning restore 612, 618
        }
    }
}

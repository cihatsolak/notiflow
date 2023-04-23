﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notiflow.IdentityServer.Infrastructure.Data;

#nullable disable

namespace Notiflow.IdentityServer.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ApplicationId")
                        .HasMaxLength(36)
                        .IsUnicode(false)
                        .HasColumnType("uniqueidentifier")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("Tenant", (string)null);
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.TenantApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("FirebaseSenderId")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("char(11)")
                        .IsFixedLength();

                    b.Property<string>("FirebaseServerKey")
                        .IsRequired()
                        .HasMaxLength(152)
                        .IsUnicode(false)
                        .HasColumnType("char(152)")
                        .IsFixedLength();

                    b.Property<string>("HuaweiSenderId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)")
                        .IsFixedLength();

                    b.Property<string>("HuaweiServerKey")
                        .IsRequired()
                        .HasMaxLength(44)
                        .IsUnicode(false)
                        .HasColumnType("char(44)")
                        .IsFixedLength();

                    b.Property<string>("MailFromAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("MailFromName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("MailReplyAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("TenantApplication", (string)null);
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.TenantPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("IsSendEmailPermission")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendMessagePermission")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSendNotificationPermission")
                        .HasColumnType("bit");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("TenantPermission", (string)null);
                });

            modelBuilder.Entity("Notiflow.Backoffice.Domain.Entities.Users.User", b =>
                {
                    b.HasOne("Notiflow.IdentityServer.Core.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.TenantApplication", b =>
                {
                    b.HasOne("Notiflow.IdentityServer.Core.Entities.Tenants.Tenant", "Tenant")
                        .WithOne("TenantApplication")
                        .HasForeignKey("Notiflow.IdentityServer.Core.Entities.Tenants.TenantApplication", "TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.TenantPermission", b =>
                {
                    b.HasOne("Notiflow.IdentityServer.Core.Entities.Tenants.Tenant", "Tenant")
                        .WithOne("TenantPermission")
                        .HasForeignKey("Notiflow.IdentityServer.Core.Entities.Tenants.TenantPermission", "TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Notiflow.IdentityServer.Core.Entities.Tenants.Tenant", b =>
                {
                    b.Navigation("TenantApplication");

                    b.Navigation("TenantPermission");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notiflow.Backoffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Definition = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    AppId = table.Column<Guid>(type: "uuid", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Gender = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MarriageStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenantapplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirebaseServerKey = table.Column<string>(type: "character(152)", unicode: false, fixedLength: true, maxLength: 152, nullable: false),
                    FirebaseSenderId = table.Column<string>(type: "character(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    HuaweiServerKey = table.Column<string>(type: "character(44)", unicode: false, fixedLength: true, maxLength: 44, nullable: false),
                    HuaweiSenderId = table.Column<string>(type: "character(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    MailFromAddress = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    MailFromName = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    MailReplyAddress = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenantapplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenantapplication_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tenantpermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsSendMessagePermission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsSendNotificationPermission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsSendEmailPermission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenantpermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenantpermission_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OSVersion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Code = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Token = table.Column<string>(type: "character varying(180)", unicode: false, maxLength: 180, nullable: false),
                    CloudMessagePlatform = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_device_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emailhistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Recipients = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Cc = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Bcc = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Subject = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ErrorMessage = table.Column<string>(type: "text", unicode: false, nullable: true),
                    SentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailhistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emailhistory_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notificationhistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ErrorMessage = table.Column<string>(type: "text", unicode: false, nullable: true),
                    SentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificationhistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificationhistory_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "textmessagehistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", unicode: false, nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    ErrorMessage = table.Column<string>(type: "text", unicode: false, nullable: true),
                    SentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_textmessagehistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_textmessagehistory_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_TenantId",
                table: "customer",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_device_CustomerId",
                table: "device",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_emailhistory_CustomerId",
                table: "emailhistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_notificationhistory_CustomerId",
                table: "notificationhistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tenantapplication_TenantId",
                table: "tenantapplication",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenantpermission_TenantId",
                table: "tenantpermission",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_textmessagehistory_CustomerId",
                table: "textmessagehistory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_user_TenantId",
                table: "user",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device");

            migrationBuilder.DropTable(
                name: "emailhistory");

            migrationBuilder.DropTable(
                name: "notificationhistory");

            migrationBuilder.DropTable(
                name: "tenantapplication");

            migrationBuilder.DropTable(
                name: "tenantpermission");

            migrationBuilder.DropTable(
                name: "textmessagehistory");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "tenant");
        }
    }
}

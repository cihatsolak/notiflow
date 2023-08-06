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
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(75)", unicode: false, maxLength: 75, nullable: false),
                    phone_number = table.Column<string>(type: "character(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "character varying(130)", unicode: false, maxLength: 130, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    marriage_status = table.Column<int>(type: "integer", nullable: false),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                    table.CheckConstraint("chk_email_format", "email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$'");
                    table.CheckConstraint("chk_gender_value_limitation", "gender IN (1,2)");
                    table.CheckConstraint("chk_marriage_status_value_limitation", "marriage_status IN (1,2)");
                    table.CheckConstraint("chk_minimum_age_restriction", "birth_date >= '1950-01-01'");
                    table.CheckConstraint("chk_phone_number_turkey", "phone_number ~ '^50|53|54|55|56\\d{8}$'");
                    table.CheckConstraint("chk_tenant_id_restriction", "tenant_id > 0");
                });

            migrationBuilder.CreateTable(
                name: "device",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    os_version = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    token = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: false),
                    cloud_message_platform = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device", x => x.id);
                    table.CheckConstraint("chk_cloud_message_platform_value_limitation", "cloud_message_platform IN (1,2)");
                    table.CheckConstraint("chk_os_version_value_limitation", "os_version IN (1,2, 3, 4)");
                    table.ForeignKey(
                        name: "fk_device_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emailhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipients = table.Column<string>(type: "text", unicode: false, nullable: false),
                    cc = table.Column<string>(type: "text", unicode: false, nullable: true),
                    bcc = table.Column<string>(type: "text", unicode: false, nullable: true),
                    subject = table.Column<string>(type: "character varying(300)", unicode: false, maxLength: 300, nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false),
                    is_body_html = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailhistory", x => x.id);
                    table.CheckConstraint("chk_emailhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
                    table.CheckConstraint("chk_sent_date", "sent_date >= NOW() - INTERVAL '30 minutes'");
                    table.ForeignKey(
                        name: "fk_emailhistory_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notificationhistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    sender_identity = table.Column<Guid>(type: "uuid", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificationhistory", x => x.id);
                    table.CheckConstraint("chk_notificationhistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
                    table.ForeignKey(
                        name: "fk_notificationhistory_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "textmessagehistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    message = table.Column<string>(type: "text", unicode: false, nullable: false),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_textmessagehistory", x => x.id);
                    table.CheckConstraint("chk_textmessagehistory_transaction_check", "is_sent = false AND error_message IS NOT NULL OR is_sent = true AND error_message IS NULL");
                    table.ForeignKey(
                        name: "fk_textmessagehistory_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_email_created_date_tenant_id",
                table: "customer",
                columns: new[] { "email", "created_date", "tenant_id" },
                descending: new[] { false, true, false });

            migrationBuilder.CreateIndex(
                name: "ix_customer_phone_number_created_date_tenant_id",
                table: "customer",
                columns: new[] { "phone_number", "created_date", "tenant_id" },
                descending: new[] { false, true, false });

            migrationBuilder.CreateIndex(
                name: "ix_device_customer_id",
                table: "device",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_emailhistory_customer_id",
                table: "emailhistory",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_notificationhistory_customer_id",
                table: "notificationhistory",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_textmessagehistory_customer_id",
                table: "textmessagehistory",
                column: "customer_id");
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
                name: "textmessagehistory");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}

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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    definition = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    app_id = table.Column<Guid>(type: "uuid", unicode: false, fixedLength: true, maxLength: 36, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    marriage_status = table.Column<int>(type: "integer", nullable: false),
                    is_blocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                    table.CheckConstraint("chk_gender_value_limitation", "gender IN (1,2)");
                    table.CheckConstraint("chk_marriage_status_value_limitation", "marriage_status IN (1,2)");
                    table.CheckConstraint("chk_minimum_age_restriction", "birth_date >= '1950-01-01'");
                    table.ForeignKey(
                        name: "fk_customer_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenantapplication",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firebase_server_key = table.Column<string>(type: "character(152)", unicode: false, fixedLength: true, maxLength: 152, nullable: false),
                    firebase_sender_id = table.Column<string>(type: "character(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    huawei_server_key = table.Column<string>(type: "character(44)", unicode: false, fixedLength: true, maxLength: 44, nullable: false),
                    huawei_sender_id = table.Column<string>(type: "character(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: false),
                    mail_from_address = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    mail_from_name = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    mail_reply_address = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenantapplication", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenantapplication_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tenantpermission",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_send_message_permission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_send_notification_permission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_send_email_permission = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenantpermission", x => x.id);
                    table.ForeignKey(
                        name: "fk_tenantpermission_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(25)", unicode: false, maxLength: 25, nullable: false),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tenant_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "device",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    os_version = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    token = table.Column<string>(type: "character varying(180)", unicode: false, maxLength: 180, nullable: false),
                    cloud_message_platform = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
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
                    subject = table.Column<string>(type: "text", unicode: false, nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailhistory", x => x.id);
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
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificationhistory", x => x.id);
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
                    is_sent = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    error_message = table.Column<string>(type: "text", unicode: false, nullable: true),
                    sent_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_textmessagehistory", x => x.id);
                    table.ForeignKey(
                        name: "fk_textmessagehistory_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_tenant_id",
                table: "customer",
                column: "tenant_id");

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
                name: "ix_tenantapplication_tenant_id",
                table: "tenantapplication",
                column: "tenant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tenantpermission_tenant_id",
                table: "tenantpermission",
                column: "tenant_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_textmessagehistory_customer_id",
                table: "textmessagehistory",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_tenant_id",
                table: "user",
                column: "tenant_id");
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

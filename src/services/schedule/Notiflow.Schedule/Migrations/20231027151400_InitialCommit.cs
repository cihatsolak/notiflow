using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notiflow.Schedule.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PlannedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedAttempts = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledEmail", x => x.Id);
                    table.CheckConstraint("CK_FailedAttempts_Max", "[FailedAttempts] <= 3");
                    table.CheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate", "[LastAttemptDate] > [PlannedDeliveryDate]");
                    table.CheckConstraint("CK_PlannedDeliveryDate_Min", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PlannedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedAttempts = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledNotification", x => x.Id);
                    table.CheckConstraint("CK_FailedAttempts_Max1", "[FailedAttempts] <= 3");
                    table.CheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate1", "[LastAttemptDate] > [PlannedDeliveryDate]");
                    table.CheckConstraint("CK_PlannedDeliveryDate_Min1", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())");
                });

            migrationBuilder.CreateTable(
                name: "ScheduledTextMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PlannedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastAttemptDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedAttempts = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledTextMessage", x => x.Id);
                    table.CheckConstraint("CK_FailedAttempts_Max2", "[FailedAttempts] <= 3");
                    table.CheckConstraint("CK_LastAttemptDate_PlannedDeliveryDate2", "[LastAttemptDate] > [PlannedDeliveryDate]");
                    table.CheckConstraint("CK_PlannedDeliveryDate_Min2", "[PlannedDeliveryDate] >= DATEADD(MINUTE, 5, GETDATE())");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledEmail");

            migrationBuilder.DropTable(
                name: "ScheduledNotification");

            migrationBuilder.DropTable(
                name: "ScheduledTextMessage");
        }
    }
}

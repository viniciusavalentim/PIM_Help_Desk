using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIM_Help_Desk.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criação da tabela 'users'
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            // Criação da tabela 'administrator'
            migrationBuilder.CreateTable(
                name: "administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administrator_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Mantido como Cascade
                });

            // Criação da tabela 'attendant'
            migrationBuilder.CreateTable(
                name: "attendant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ramal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendant_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Mantido como Cascade
                });

            // Criação da tabela 'log'
            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_log_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Mantido como Cascade
                });

            // Criação da tabela 'requester'
            migrationBuilder.CreateTable(
                name: "requester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requester", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requester_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Mantido como Cascade
                });

            // Criação da tabela 'ticket'
            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    AttendantId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Evaluation = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_attendant_AttendantId",
                        column: x => x.AttendantId,
                        principalTable: "attendant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Alterado para Restrict
                    table.ForeignKey(
                        name: "FK_ticket_requester_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "requester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Alterado para Restrict
                });

            // Criação da tabela 'ticket_responses'
            migrationBuilder.CreateTable(
                name: "ticket_responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    AttendantId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_responses_attendant_AttendantId",
                        column: x => x.AttendantId,
                        principalTable: "attendant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Alterado para Restrict
                    table.ForeignKey(
                        name: "FK_ticket_responses_requester_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "requester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Alterado para Restrict
                    table.ForeignKey(
                        name: "FK_ticket_responses_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Alterado para Restrict
                });

            // Criação dos índices
            migrationBuilder.CreateIndex(
                name: "IX_administrator_UserId",
                table: "administrator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_attendant_UserId",
                table: "attendant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_log_UserId",
                table: "log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_requester_UserId",
                table: "requester",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_AttendantId",
                table: "ticket",
                column: "AttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_RequesterId",
                table: "ticket",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_responses_AttendantId",
                table: "ticket_responses",
                column: "AttendantId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_responses_RequesterId",
                table: "ticket_responses",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_responses_TicketId",
                table: "ticket_responses",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrator");

            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "ticket_responses");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "attendant");

            migrationBuilder.DropTable(
                name: "requester");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Emison.Data.Migrations
{
    public partial class InvitationCodeToEventAndEventToGreeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Greetings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvitationCode",
                table: "Events",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Greetings_EventId",
                table: "Greetings",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Greetings_Events_EventId",
                table: "Greetings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greetings_Events_EventId",
                table: "Greetings");

            migrationBuilder.DropIndex(
                name: "IX_Greetings_EventId",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "InvitationCode",
                table: "Events");
        }
    }
}

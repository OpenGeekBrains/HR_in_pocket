using Microsoft.EntityFrameworkCore.Migrations;

namespace HRInPocket.DAL.Migrations
{
    public partial class Add_Onetomany_TarifApplicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TarifId",
                table: "Applicants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TarifId",
                table: "Applicants",
                column: "TarifId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Tarifs_TarifId",
                table: "Applicants",
                column: "TarifId",
                principalTable: "Tarifs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Tarifs_TarifId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TarifId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "TarifId",
                table: "Applicants");
        }
    }
}

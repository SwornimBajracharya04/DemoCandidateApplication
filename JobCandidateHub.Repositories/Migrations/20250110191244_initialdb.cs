using Microsoft.EntityFrameworkCore.Migrations;

namespace JobCandidateHub.Repositories.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(1000)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(1000)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Email = table.Column<string>(type: "varchar(1000)", nullable: false),
                    TimeIntervalToCall = table.Column<string>(type: "varchar(1000)", nullable: true),
                    LinkedinURL = table.Column<string>(type: "varchar(1000)", nullable: true),
                    GitHubURL = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Comment = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidate");
        }
    }
}

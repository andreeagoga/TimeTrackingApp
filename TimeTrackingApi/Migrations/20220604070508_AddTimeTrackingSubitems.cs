using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingApi.Migrations
{
    public partial class AddTimeTrackingSubitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeTrackingSubitems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumarOre = table.Column<int>(type: "int", nullable: false),
                    Dificultate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTrackingSubitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTrackingSubitems_TimeTrackingItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TimeTrackingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTrackingSubitems_ParentId",
                table: "TimeTrackingSubitems",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTrackingSubitems");
        }
    }
}

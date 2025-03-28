using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactorApi.Migrations
{
    /// <inheritdoc />
    public partial class addedReactorDataReactorValuesandUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReactorDatas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reactorId = table.Column<int>(type: "int", nullable: false),
                    temperature = table.Column<int>(type: "int", nullable: false),
                    fieldStrength = table.Column<int>(type: "int", nullable: false),
                    energySaturation = table.Column<int>(type: "int", nullable: false),
                    fuelExhaustion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactorDatas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ReactorsValues",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reactorId = table.Column<int>(type: "int", nullable: false),
                    inputEnergy = table.Column<int>(type: "int", nullable: false),
                    outputEnergy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactorsValues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reactorId = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    minecraftUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactorDatas");

            migrationBuilder.DropTable(
                name: "ReactorsValues");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

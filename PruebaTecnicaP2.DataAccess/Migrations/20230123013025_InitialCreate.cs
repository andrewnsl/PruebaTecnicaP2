using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaP2.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Puntos",
                columns: table => new
                {
                    PuntosId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    uid = table.Column<string>(type: "TEXT", nullable: false),
                    valor = table.Column<int>(type: "INTEGER", nullable: false),
                    factura = table.Column<string>(type: "TEXT", nullable: false),
                    uidcms = table.Column<string>(name: "uid_cms", type: "TEXT", nullable: false),
                    idsucursal = table.Column<int>(name: "id_sucursal", type: "INTEGER", nullable: false),
                    idcomercio = table.Column<int>(name: "id_comercio", type: "INTEGER", nullable: false),
                    idtransaccion = table.Column<int>(name: "id_transaccion", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puntos", x => x.PuntosId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puntos");
        }
    }
}

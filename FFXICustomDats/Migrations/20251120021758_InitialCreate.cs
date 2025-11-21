using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFXICustomDats.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    itemid = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    flags = table.Column<uint>(type: "int(8) unsigned", nullable: false),
                    stackSize = table.Column<byte>(type: "tinyint(2) unsigned", nullable: false, defaultValueSql: "'1'"),
                    type = table.Column<byte>(type: "tinyint(1) unsigned", nullable: false, defaultValueSql: "@`GENERAL_TYPE`"),
                    resource_id = table.Column<int>(type: "int(11)", nullable: false),
                    validTargets = table.Column<ushort>(type: "smallint(3) unsigned", nullable: false),
                    dat_file = table.Column<string>(type: "tinytext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon_bytes = table.Column<byte[]>(type: "blob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_equipment",
                columns: table => new
                {
                    itemId = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    level = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    slot = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    races = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    jobs = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    superior_level = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    shieldSize = table.Column<byte>(type: "tinyint(1) unsigned", nullable: false),
                    maxCharges = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    castingTime = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    useDelay = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    reuseDelay = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    ilevel = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    unknown1 = table.Column<int>(type: "int(11)", nullable: false),
                    unknown2 = table.Column<int>(type: "int(11)", nullable: false),
                    unknown3 = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_furnishing",
                columns: table => new
                {
                    itemid = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    storageSlots = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    element = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    unknown3 = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_puppet",
                columns: table => new
                {
                    itemid = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    slot = table.Column<byte>(type: "tinyint(2) unsigned", nullable: false),
                    fire = table.Column<int>(type: "int(11)", nullable: false),
                    ice = table.Column<int>(type: "int(11)", nullable: false),
                    wind = table.Column<int>(type: "int(11)", nullable: false),
                    earth = table.Column<int>(type: "int(11)", nullable: false),
                    lightning = table.Column<int>(type: "int(11)", nullable: false),
                    water = table.Column<int>(type: "int(11)", nullable: false),
                    light = table.Column<int>(type: "int(11)", nullable: false),
                    dark = table.Column<int>(type: "int(11)", nullable: false),
                    unknown1 = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_string",
                columns: table => new
                {
                    itemid = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    name = table.Column<string>(type: "tinytext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    articleType = table.Column<ushort>(type: "smallint(3) unsigned", nullable: false),
                    singular_name = table.Column<string>(type: "tinytext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    plural_name = table.Column<string>(type: "tinytext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemid);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_usable",
                columns: table => new
                {
                    itemId = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    activationTime = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    unknown1 = table.Column<int>(type: "int(11)", nullable: false),
                    unknown2 = table.Column<int>(type: "int(11)", nullable: false),
                    unknown3 = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "item_weapon",
                columns: table => new
                {
                    itemId = table.Column<ushort>(type: "smallint(5) unsigned", nullable: false),
                    dmg = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    delay = table.Column<int>(type: "int(10)", nullable: false),
                    dps = table.Column<int>(type: "int(10)", nullable: false),
                    skillType = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    jugSize = table.Column<int>(type: "int(10)", nullable: false),
                    unknown1 = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.itemId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "item_equipment");

            migrationBuilder.DropTable(
                name: "item_furnishing");

            migrationBuilder.DropTable(
                name: "item_puppet");

            migrationBuilder.DropTable(
                name: "item_string");

            migrationBuilder.DropTable(
                name: "item_usable");

            migrationBuilder.DropTable(
                name: "item_weapon");
        }
    }
}

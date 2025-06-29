using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FutbolBase.Catalog.Api.App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ShieldUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClub",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClub", x => new { x.UserId, x.ClubId });
                    table.ForeignKey(
                        name: "FK_UserClub_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClub_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AF", "Afganistán" },
                    { 2, "AL", "Albania" },
                    { 3, "DE", "Alemania" },
                    { 4, "AD", "Andorra" },
                    { 5, "AO", "Angola" },
                    { 6, "AG", "Antigua y Barbuda" },
                    { 7, "SA", "Arabia Saudita" },
                    { 8, "DZ", "Argelia" },
                    { 9, "AR", "Argentina" },
                    { 10, "AM", "Armenia" },
                    { 11, "AU", "Australia" },
                    { 12, "AT", "Austria" },
                    { 13, "AZ", "Azerbaiyán" },
                    { 14, "BS", "Bahamas" },
                    { 15, "BD", "Bangladés" },
                    { 16, "BB", "Barbados" },
                    { 17, "BH", "Baréin" },
                    { 18, "BE", "Bélgica" },
                    { 19, "BZ", "Belice" },
                    { 20, "BJ", "Benín" },
                    { 21, "BY", "Bielorrusia" },
                    { 22, "MM", "Birmania (Myanmar)" },
                    { 23, "BO", "Bolivia" },
                    { 24, "BA", "Bosnia y Herzegovina" },
                    { 25, "BW", "Botsuana" },
                    { 26, "BR", "Brasil" },
                    { 27, "BN", "Brunéi" },
                    { 28, "BG", "Bulgaria" },
                    { 29, "BF", "Burkina Faso" },
                    { 30, "BI", "Burundi" },
                    { 31, "BT", "Bután" },
                    { 32, "CV", "Cabo Verde" },
                    { 33, "KH", "Camboya" },
                    { 34, "CM", "Camerún" },
                    { 35, "CA", "Canadá" },
                    { 36, "QA", "Catar" },
                    { 37, "TD", "Chad" },
                    { 38, "CL", "Chile" },
                    { 39, "CN", "China" },
                    { 40, "CY", "Chipre" },
                    { 41, "CO", "Colombia" },
                    { 42, "KM", "Comoras" },
                    { 43, "KP", "Corea del Norte" },
                    { 44, "KR", "Corea del Sur" },
                    { 45, "CI", "Costa de Marfil" },
                    { 46, "CR", "Costa Rica" },
                    { 47, "HR", "Croacia" },
                    { 48, "CU", "Cuba" },
                    { 49, "DK", "Dinamarca" },
                    { 50, "DM", "Dominica" },
                    { 51, "EC", "Ecuador" },
                    { 52, "EG", "Egipto" },
                    { 53, "SV", "El Salvador" },
                    { 54, "AE", "Emiratos Árabes Unidos" },
                    { 55, "ER", "Eritrea" },
                    { 56, "SK", "Eslovaquia" },
                    { 57, "SI", "Eslovenia" },
                    { 58, "ES", "España" },
                    { 59, "US", "Estados Unidos" },
                    { 60, "EE", "Estonia" },
                    { 61, "SZ", "Esuatini" },
                    { 62, "ET", "Etiopía" },
                    { 63, "PH", "Filipinas" },
                    { 64, "FI", "Finlandia" },
                    { 65, "FJ", "Fiyi" },
                    { 66, "FR", "Francia" },
                    { 67, "GA", "Gabón" },
                    { 68, "GM", "Gambia" },
                    { 69, "GE", "Georgia" },
                    { 70, "GH", "Ghana" },
                    { 71, "GD", "Granada" },
                    { 72, "GR", "Grecia" },
                    { 73, "GT", "Guatemala" },
                    { 74, "GN", "Guinea" },
                    { 75, "GW", "Guinea-Bisáu" },
                    { 76, "GY", "Guyana" },
                    { 77, "HT", "Haití" },
                    { 78, "HN", "Honduras" },
                    { 79, "HU", "Hungría" },
                    { 80, "IN", "India" },
                    { 81, "ID", "Indonesia" },
                    { 82, "IQ", "Irak" },
                    { 83, "IR", "Irán" },
                    { 84, "IE", "Irlanda" },
                    { 85, "IS", "Islandia" },
                    { 86, "IL", "Israel" },
                    { 87, "IT", "Italia" },
                    { 88, "JM", "Jamaica" },
                    { 89, "JP", "Japón" },
                    { 90, "JO", "Jordania" },
                    { 91, "KZ", "Kazajistán" },
                    { 92, "KE", "Kenia" },
                    { 93, "KG", "Kirguistán" },
                    { 94, "KI", "Kiribati" },
                    { 95, "XK", "Kosovo" },
                    { 96, "KW", "Kuwait" },
                    { 97, "LA", "Laos" },
                    { 98, "LV", "Letonia" },
                    { 99, "LB", "Líbano" },
                    { 100, "LR", "Liberia" },
                    { 101, "LY", "Libia" },
                    { 102, "LI", "Liechtenstein" },
                    { 103, "LT", "Lituania" },
                    { 104, "LU", "Luxemburgo" },
                    { 105, "MG", "Madagascar" },
                    { 106, "MY", "Malasia" },
                    { 107, "MW", "Malaui" },
                    { 108, "MV", "Maldivas" },
                    { 109, "ML", "Malí" },
                    { 110, "MT", "Malta" },
                    { 111, "MA", "Marruecos" },
                    { 112, "MU", "Mauricio" },
                    { 113, "MR", "Mauritania" },
                    { 114, "MX", "México" },
                    { 115, "FM", "Micronesia" },
                    { 116, "MD", "Moldavia" },
                    { 117, "MC", "Mónaco" },
                    { 118, "MN", "Mongolia" },
                    { 119, "ME", "Montenegro" },
                    { 120, "MZ", "Mozambique" },
                    { 121, "NA", "Namibia" },
                    { 122, "NR", "Nauru" },
                    { 123, "NP", "Nepal" },
                    { 124, "NI", "Nicaragua" },
                    { 125, "NE", "Níger" },
                    { 126, "NG", "Nigeria" },
                    { 127, "NO", "Noruega" },
                    { 128, "NZ", "Nueva Zelanda" },
                    { 129, "OM", "Omán" },
                    { 130, "NL", "Países Bajos" },
                    { 131, "PK", "Pakistán" },
                    { 132, "PW", "Palaos" },
                    { 133, "PA", "Panamá" },
                    { 134, "PG", "Papúa Nueva Guinea" },
                    { 135, "PY", "Paraguay" },
                    { 136, "PE", "Perú" },
                    { 137, "PL", "Polonia" },
                    { 138, "PT", "Portugal" },
                    { 139, "GB", "Reino Unido" },
                    { 140, "CF", "República Centroafricana" },
                    { 141, "CZ", "República Checa" },
                    { 142, "DO", "República Dominicana" },
                    { 143, "RW", "Ruanda" },
                    { 144, "RO", "Rumanía" },
                    { 145, "RU", "Rusia" },
                    { 146, "WS", "Samoa" },
                    { 147, "KN", "San Cristóbal y Nieves" },
                    { 148, "SM", "San Marino" },
                    { 149, "VC", "San Vicente y las Granadinas" },
                    { 150, "LC", "Santa Lucía" },
                    { 151, "ST", "Santo Tomé y Príncipe" },
                    { 152, "SN", "Senegal" },
                    { 153, "RS", "Serbia" },
                    { 154, "SC", "Seychelles" },
                    { 155, "SL", "Sierra Leona" },
                    { 156, "SG", "Singapur" },
                    { 157, "SY", "Siria" },
                    { 158, "SO", "Somalia" },
                    { 159, "LK", "Sri Lanka" },
                    { 160, "ZA", "Sudáfrica" },
                    { 161, "SD", "Sudán" },
                    { 162, "SS", "Sudán del Sur" },
                    { 163, "SE", "Suecia" },
                    { 164, "CH", "Suiza" },
                    { 165, "SR", "Surinam" },
                    { 166, "TH", "Tailandia" },
                    { 167, "TZ", "Tanzania" },
                    { 168, "TJ", "Tayikistán" },
                    { 169, "TL", "Timor Oriental" },
                    { 170, "TG", "Togo" },
                    { 171, "TO", "Tonga" },
                    { 172, "TT", "Trinidad y Tobago" },
                    { 173, "TN", "Túnez" },
                    { 174, "TM", "Turkmenistán" },
                    { 175, "TR", "Turquía" },
                    { 176, "TV", "Tuvalu" },
                    { 177, "UA", "Ucrania" },
                    { 178, "UG", "Uganda" },
                    { 179, "UY", "Uruguay" },
                    { 180, "UZ", "Uzbekistán" },
                    { 181, "VU", "Vanuatu" },
                    { 182, "VA", "Vaticano" },
                    { 183, "VE", "Venezuela" },
                    { 184, "VN", "Vietnam" },
                    { 185, "YE", "Yemen" },
                    { 186, "DJ", "Yibuti" },
                    { 187, "ZM", "Zambia" },
                    { 188, "ZW", "Zimbabue" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_CountryId",
                table: "Clubs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClub_ClubId",
                table: "UserClub",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClub");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UBB_SE_2024_Music.Data.Migrations
{
    /// <inheritdoc />
    public partial class OldModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdDistributionData",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    AdCampaign = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdDistributionData", x => new { x.SongId, x.AdCampaign });
                });

            migrationBuilder.CreateTable(
                name: "ArtistDetails",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistDetails", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "ExcludedCountries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcludedCountries", x => new { x.CountryId, x.SongId });
                });

            migrationBuilder.CreateTable(
                name: "MostPlayedArtistInformation",
                columns: table => new
                {
                    Artist_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_Listen_Events = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MostPlayedArtistInformation", x => x.Artist_Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongDataBaseModel",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subgenre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDataBaseModel", x => x.SongId);
                });

            migrationBuilder.CreateTable(
                name: "SongFeatures",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongFeatures", x => new { x.SongId, x.ArtistId });
                });

            migrationBuilder.CreateTable(
                name: "SongRecommendationDetails",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    MinutesListened = table.Column<int>(type: "int", nullable: false),
                    NumberOfPlays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongRecommendationDetails", x => new { x.SongId, x.Month, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subgenre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExplicit = table.Column<bool>(type: "bit", nullable: false),
                    SongPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                });

            migrationBuilder.CreateTable(
                name: "Sounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SoundFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trends",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trends", x => new { x.SongId, x.Genre, x.Language, x.Country });
                });

            migrationBuilder.CreateTable(
                name: "UserDemographicsDetails",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Date_Of_fBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Premium_User = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDemographicsDetails", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPlaybackBehaviour",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlaybackBehaviour", x => new { x.UserId, x.SongId, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistSongItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSongItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistSongItems_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSongItems_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongItems_PlaylistId",
                table: "PlaylistSongItems",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongItems_SongId",
                table: "PlaylistSongItems",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdDistributionData");

            migrationBuilder.DropTable(
                name: "ArtistDetails");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "ExcludedCountries");

            migrationBuilder.DropTable(
                name: "MostPlayedArtistInformation");

            migrationBuilder.DropTable(
                name: "PlaylistSongItems");

            migrationBuilder.DropTable(
                name: "SongDataBaseModel");

            migrationBuilder.DropTable(
                name: "SongFeatures");

            migrationBuilder.DropTable(
                name: "SongRecommendationDetails");

            migrationBuilder.DropTable(
                name: "Sounds");

            migrationBuilder.DropTable(
                name: "Trends");

            migrationBuilder.DropTable(
                name: "UserDemographicsDetails");

            migrationBuilder.DropTable(
                name: "UserPlaybackBehaviour");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}

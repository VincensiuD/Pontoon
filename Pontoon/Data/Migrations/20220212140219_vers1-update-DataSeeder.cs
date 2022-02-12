using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pontoon.Data.Migrations
{
    public partial class vers1updateDataSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardSequences",
                columns: new[] { "Id", "OwnerName" },
                values: new object[,]
                {
                    { 1, "Dealer" },
                    { 2, "Player" },
                    { 3, "PlayerSplit1" },
                    { 4, "PlayerSplit2" }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardSequenceId", "Colour", "DisplayCode", "Rank", "Suit", "Value" },
                values: new object[,]
                {
                    { 1, null, "RED", "HeartA", "A", "Heart", 1 },
                    { 2, null, "RED", "Heart2", "2", "Heart", 2 },
                    { 3, null, "RED", "Heart3", "3", "Heart", 3 },
                    { 4, null, "RED", "Heart4", "4", "Heart", 4 },
                    { 5, null, "RED", "Heart5", "5", "Heart", 5 },
                    { 6, null, "RED", "Heart6", "6", "Heart", 6 },
                    { 7, null, "RED", "Heart7", "7", "Heart", 7 },
                    { 8, null, "RED", "Heart8", "8", "Heart", 8 },
                    { 9, null, "RED", "Heart9", "9", "Heart", 9 },
                    { 10, null, "RED", "Heart10", "10", "Heart", 10 },
                    { 11, null, "RED", "HeartJ", "J", "Heart", 10 },
                    { 12, null, "RED", "HeartQ", "Q", "Heart", 10 },
                    { 13, null, "RED", "HeartK", "K", "Heart", 10 },
                    { 14, null, "BLK", "SpadeA", "A", "Spade", 1 },
                    { 15, null, "BLK", "Spade2", "2", "Spade", 2 },
                    { 16, null, "BLK", "Spade3", "3", "Spade", 3 },
                    { 17, null, "BLK", "Spade4", "4", "Spade", 4 },
                    { 18, null, "BLK", "Spade5", "5", "Spade", 5 },
                    { 19, null, "BLK", "Spade6", "6", "Spade", 6 },
                    { 20, null, "BLK", "Spade7", "7", "Spade", 7 },
                    { 21, null, "BLK", "Spade8", "8", "Spade", 8 },
                    { 22, null, "BLK", "Spade9", "9", "Spade", 9 },
                    { 23, null, "BLK", "Spade10", "10", "Spade", 10 },
                    { 24, null, "BLK", "SpadeJ", "J", "Spade", 10 },
                    { 25, null, "BLK", "SpadeQ", "Q", "Spade", 10 },
                    { 26, null, "BLK", "SpadeK", "K", "Spade", 10 },
                    { 27, null, "RED", "DiamondA", "A", "Diamond", 1 },
                    { 28, null, "RED", "Diamond2", "2", "Diamond", 2 },
                    { 29, null, "RED", "Diamond3", "3", "Diamond", 3 },
                    { 30, null, "RED", "Diamond4", "4", "Diamond", 4 },
                    { 31, null, "RED", "Diamond5", "5", "Diamond", 5 },
                    { 32, null, "RED", "Diamond6", "6", "Diamond", 6 },
                    { 33, null, "RED", "Diamond7", "7", "Diamond", 7 },
                    { 34, null, "RED", "Diamond8", "8", "Diamond", 8 },
                    { 35, null, "RED", "Diamond9", "9", "Diamond", 9 },
                    { 36, null, "RED", "Diamond10", "10", "Diamond", 10 },
                    { 37, null, "RED", "DiamondJ", "J", "Diamond", 10 },
                    { 38, null, "RED", "DiamondQ", "Q", "Diamond", 10 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "CardSequenceId", "Colour", "DisplayCode", "Rank", "Suit", "Value" },
                values: new object[,]
                {
                    { 39, null, "RED", "DiamondK", "K", "Diamond", 10 },
                    { 40, null, "BLK", "ClubA", "A", "Club", 1 },
                    { 41, null, "BLK", "Club2", "2", "Club", 2 },
                    { 42, null, "BLK", "Club3", "3", "Club", 3 },
                    { 43, null, "BLK", "Club4", "4", "Club", 4 },
                    { 44, null, "BLK", "Club5", "5", "Club", 5 },
                    { 45, null, "BLK", "Club6", "6", "Club", 6 },
                    { 46, null, "BLK", "Club7", "7", "Club", 7 },
                    { 47, null, "BLK", "Club8", "8", "Club", 8 },
                    { 48, null, "BLK", "Club9", "9", "Club", 9 },
                    { 49, null, "BLK", "Club10", "10", "Club", 10 },
                    { 50, null, "BLK", "ClubJ", "J", "Club", 10 },
                    { 51, null, "BLK", "ClubQ", "Q", "Club", 10 },
                    { 52, null, "BLK", "ClubK", "K", "Club", 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardSequences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CardSequences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CardSequences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CardSequences",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 52);
        }
    }
}

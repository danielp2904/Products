using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Products.Migrations
{
    /// <inheritdoc />
    public partial class initialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "CNPJ", "Name", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, "12345678901234", "Cliente A", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "12345678901234", "Cliente B", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PaymentTerms",
                columns: new[] { "PaymentTermsId", "Days", "Description", "PurchasesId" },
                values: new object[,]
                {
                    { 1, 0, "À vista", 1 },
                    { 2, 30, "30 dias", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "Value" },
                values: new object[,]
                {
                    { 1, "Produto A", 100.00m },
                    { 2, "Produto B", 200.00m }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchasesId", "Amount", "ClientId", "ProductId" },
                values: new object[,]
                {
                    { 1, 150.00m, 1, 1 },
                    { 2, 250.00m, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchasesId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Purchases",
                keyColumn: "PurchasesId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentTerms",
                keyColumn: "PaymentTermsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentTerms",
                keyColumn: "PaymentTermsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
        }
    }
}

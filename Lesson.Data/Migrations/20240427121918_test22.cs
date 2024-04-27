using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson.Data.Migrations
{
    public partial class test22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("4173db6f-82bc-4d1f-8e66-2c61b26dddb6"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("e47759fa-3a7d-4730-913f-7c6a632138db"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "ModifiedBy", "ModifiedDate", "Tutle", "UserId", "ViewCount", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("2d1434ff-a5e9-4d67-935b-a8dc7d58ad9e"), new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"), "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Jako ismo", new DateTime(2024, 4, 27, 16, 19, 18, 41, DateTimeKind.Local).AddTicks(5400), null, null, new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"), null, null, "C# lesson-2", new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"), 16, false },
                    { new Guid("4cd44a30-b6d5-4615-9173-1677c801b6f8"), new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.", "Jako ismo", new DateTime(2024, 4, 27, 16, 19, 18, 41, DateTimeKind.Local).AddTicks(5384), null, null, new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"), null, null, "C# lesson-1", new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"), 16, false }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("26c34f97-7d52-452b-8e70-48135d3756cd"),
                column: "ConcurrencyStamp",
                value: "b9bbbd85-e52f-4e0e-82e5-6e6b8eb8df97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4380fcf7-df75-485f-888a-d7715be71026"),
                column: "ConcurrencyStamp",
                value: "682f0be8-6e2e-416e-8c56-f720a3fee7a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("81d91823-eb61-4d17-a1fc-8a286f88f6d4"),
                column: "ConcurrencyStamp",
                value: "4186de15-336d-4dba-ae01-19de1db7dc82");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b5c0033f-e7f1-4610-a19c-fa970c039602"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5f1614e-e724-49ab-9d8a-10f68cfc8d43", "AQAAAAEAACcQAAAAEGUeHjsib0h1i1NHottzIImeVvTIdlr31ix9RPAajijMSwLdV+tr3DOv+ruOLdfthA==", "f5d22051-4217-4f9f-b701-bb474536800f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ae6db0d-56ee-49c3-ac81-b20b98176066", "AQAAAAEAACcQAAAAEJcEhhismg25llOTJv4Lxxk3TFZP/eeOhusc0eYvCKKnNZdV412vT2wIwTeNUDapbA==", "43b3b11b-d99f-4dcf-b4f5-384bda653eca" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 16, 19, 18, 41, DateTimeKind.Local).AddTicks(8695));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c2e31f7-fbf2-44d1-9c3d-321165616b47"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 16, 19, 18, 41, DateTimeKind.Local).AddTicks(8704));

            migrationBuilder.UpdateData(
                table: "images",
                keyColumn: "Id",
                keyValue: new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"),
                column: "CreatedDate",
                value: new DateTime(2024, 4, 27, 16, 19, 18, 41, DateTimeKind.Local).AddTicks(8980));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("2d1434ff-a5e9-4d67-935b-a8dc7d58ad9e"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("4cd44a30-b6d5-4615-9173-1677c801b6f8"));

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "Content", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "ImageId", "ModifiedBy", "ModifiedDate", "Tutle", "UserId", "ViewCount", "isDeleted" },
                values: new object[,]
                {
                    { new Guid("4173db6f-82bc-4d1f-8e66-2c61b26dddb6"), new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"), "It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.", "Jako ismo", new DateTime(2024, 3, 31, 23, 58, 36, 495, DateTimeKind.Local).AddTicks(6265), null, null, new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"), null, null, "C# lesson-2", new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"), 16, false },
                    { new Guid("e47759fa-3a7d-4730-913f-7c6a632138db"), new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.", "Jako ismo", new DateTime(2024, 3, 31, 23, 58, 36, 495, DateTimeKind.Local).AddTicks(6259), null, null, new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"), null, null, "C# lesson-1", new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"), 16, false }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("26c34f97-7d52-452b-8e70-48135d3756cd"),
                column: "ConcurrencyStamp",
                value: "b0b493f0-734e-46ac-a79f-c1a929b23c13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4380fcf7-df75-485f-888a-d7715be71026"),
                column: "ConcurrencyStamp",
                value: "2650a42d-35a2-4737-bb5a-2c76878aa51a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("81d91823-eb61-4d17-a1fc-8a286f88f6d4"),
                column: "ConcurrencyStamp",
                value: "3b10c937-867d-43e5-b566-744270e6786d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b5c0033f-e7f1-4610-a19c-fa970c039602"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af3ddbad-7697-43ea-959d-6f238bfc4f5f", "AQAAAAEAACcQAAAAEDC+4dRZSEkIjdUvu86gHbrWmIqxBwovANOdzDgUyW0eA8rilotNJ6igtfuHwLsPgA==", "4f4be4c3-c23a-4ec0-93a1-f7bc86e36709" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0c8114c-578e-4d0a-84d9-d936e0f34a7c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1aec562f-69f7-4ff8-911f-d28cc89da07c", "AQAAAAEAACcQAAAAEOrgRP24x/ayPSDPo7pfWwxSFSbblw77DL++XkwoSxm0qNjm0Wznw1KhF6n34H6hfQ==", "50e3fef6-9f9c-4215-9ff2-c7f996b09ea5" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7bfb7cb1-748f-4728-816e-354aa2388054"),
                column: "CreatedDate",
                value: new DateTime(2024, 3, 31, 23, 58, 36, 495, DateTimeKind.Local).AddTicks(7810));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c2e31f7-fbf2-44d1-9c3d-321165616b47"),
                column: "CreatedDate",
                value: new DateTime(2024, 3, 31, 23, 58, 36, 495, DateTimeKind.Local).AddTicks(7814));

            migrationBuilder.UpdateData(
                table: "images",
                keyColumn: "Id",
                keyValue: new Guid("53c70e42-4494-47e0-8391-43aed02dadd3"),
                column: "CreatedDate",
                value: new DateTime(2024, 3, 31, 23, 58, 36, 495, DateTimeKind.Local).AddTicks(7944));
        }
    }
}

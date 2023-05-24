using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Crud_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseCode", "CourseName", "CourseTitle" },
                values: new object[,]
                {
                    { 3L, "CSC101", "Introduction to Computer Science", "CS101" },
                    { 4L, "MAT201", "Linear Algebra", "LA201" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Class", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 3L, "10A", new DateTime(2005, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe" },
                    { 4L, "11B", new DateTime(2004, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "StudentCourses",
                columns: new[] { "Id", "CourseId", "StudentId" },
                values: new object[,]
                {
                    { 3L, 3L, 3L },
                    { 4L, 4L, 4L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "StudentCourses",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}

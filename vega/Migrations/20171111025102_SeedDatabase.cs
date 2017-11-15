using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MAKE (NAME) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO MAKE (NAME) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO MAKE (NAME) VALUES ('Make3')");

            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 1 - MODEL A', (SELECT ID FROM MAKE WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 1 - MODEL B', (SELECT ID FROM MAKE WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 1 - MODEL C', (SELECT ID FROM MAKE WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 2 - MODEL A', (SELECT ID FROM MAKE WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 2 - MODEL B', (SELECT ID FROM MAKE WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 2 - MODEL C', (SELECT ID FROM MAKE WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 3 - MODEL A', (SELECT ID FROM MAKE WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 3 - MODEL B', (SELECT ID FROM MAKE WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO MODEL (NAME, MAKEID) VALUES ('Make 3 - MODEL C', (SELECT ID FROM MAKE WHERE Name = 'Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM MAKE WHERE NAME IN ('Make1','Make2','Make3')");

        }
    }
}

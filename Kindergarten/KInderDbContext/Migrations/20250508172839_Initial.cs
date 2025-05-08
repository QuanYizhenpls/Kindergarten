using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinderDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Agreement_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vacation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SickLeave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dismissal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentContract = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Agreement_Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Plan_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfTheEvent = table.Column<DateOnly>(type: "date", nullable: false),
                    Development = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Plan_Id);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Salary_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Allowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Prepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Penalty = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Salary_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agreement_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Plan_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Salary_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_Id);
                    table.ForeignKey(
                        name: "FK_Employees_Agreements_Agreement_Id",
                        column: x => x.Agreement_Id,
                        principalTable: "Agreements",
                        principalColumn: "Agreement_Id");
                    table.ForeignKey(
                        name: "FK_Employees_Plans_Plan_Id",
                        column: x => x.Plan_Id,
                        principalTable: "Plans",
                        principalColumn: "Plan_Id");
                    table.ForeignKey(
                        name: "FK_Employees_Salaries_Salary_Id",
                        column: x => x.Salary_Id,
                        principalTable: "Salaries",
                        principalColumn: "Salary_Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDatas",
                columns: table => new
                {
                    EmployeeData_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pasport = table.Column<int>(type: "int", nullable: false),
                    SNILS = table.Column<int>(type: "int", nullable: false),
                    INN = table.Column<int>(type: "int", nullable: false),
                    EmploymentRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeesEmployee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDatas", x => x.EmployeeData_Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDatas_Employees_EmployeesEmployee_Id",
                        column: x => x.EmployeesEmployee_Id,
                        principalTable: "Employees",
                        principalColumn: "Employee_Id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Group_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Plan_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Group_Id);
                    table.ForeignKey(
                        name: "FK_Groups_Employees_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "Employees",
                        principalColumn: "Employee_Id");
                    table.ForeignKey(
                        name: "FK_Groups_Plans_Plan_Id",
                        column: x => x.Plan_Id,
                        principalTable: "Plans",
                        principalColumn: "Plan_Id");
                });

            migrationBuilder.CreateTable(
                name: "Kindergartners",
                columns: table => new
                {
                    Kindergartner_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FIO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentsContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kindergartners", x => x.Kindergartner_Id);
                    table.ForeignKey(
                        name: "FK_Kindergartners_Groups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Groups",
                        principalColumn: "Group_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDatas_EmployeesEmployee_Id",
                table: "EmployeeDatas",
                column: "EmployeesEmployee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Agreement_Id",
                table: "Employees",
                column: "Agreement_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Plan_Id",
                table: "Employees",
                column: "Plan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Salary_Id",
                table: "Employees",
                column: "Salary_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Employee_Id",
                table: "Groups",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Plan_Id",
                table: "Groups",
                column: "Plan_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Kindergartners_Group_Id",
                table: "Kindergartners",
                column: "Group_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDatas");

            migrationBuilder.DropTable(
                name: "Kindergartners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientWebAPI.Migrations
{
    public partial class Atlas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ssn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientLabVisit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientDetails_Id = table.Column<int>(type: "int", nullable: true),
                    LabName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabTestRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLabVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLabVisit_PatientDetails_PatientDetails_Id",
                        column: x => x.PatientDetails_Id,
                        principalTable: "PatientDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVaccinationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientDetails_Id = table.Column<int>(type: "int", nullable: true),
                    VaccineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VaccineValidity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministeredBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVaccinationData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVaccinationData_PatientDetails_PatientDetails_Id",
                        column: x => x.PatientDetails_Id,
                        principalTable: "PatientDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisitHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientDetails_Id = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NurseName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NurseName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisitHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVisitHistory_PatientDetails_PatientDetails_Id",
                        column: x => x.PatientDetails_Id,
                        principalTable: "PatientDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientLabResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLabVisit_Id = table.Column<int>(type: "int", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestObservation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLabResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLabResult_PatientLabVisit_PatientLabVisit_Id",
                        column: x => x.PatientLabVisit_Id,
                        principalTable: "PatientLabVisit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMedication",
                columns: table => new
                {
                    PatientDetails_Id = table.Column<int>(type: "int", nullable: true),
                    PatientLabVisit_Id = table.Column<int>(type: "int", nullable: true),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescribedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrescriptionPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_PatientMedication_PatientDetails_PatientDetails_Id",
                        column: x => x.PatientDetails_Id,
                        principalTable: "PatientDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientMedication_PatientLabVisit_PatientLabVisit_Id",
                        column: x => x.PatientLabVisit_Id,
                        principalTable: "PatientLabVisit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientLabResult_PatientLabVisit_Id",
                table: "PatientLabResult",
                column: "PatientLabVisit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLabVisit_PatientDetails_Id",
                table: "PatientLabVisit",
                column: "PatientDetails_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_PatientDetails_Id",
                table: "PatientMedication",
                column: "PatientDetails_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_PatientLabVisit_Id",
                table: "PatientMedication",
                column: "PatientLabVisit_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVaccinationData_PatientDetails_Id",
                table: "PatientVaccinationData",
                column: "PatientDetails_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitHistory_PatientDetails_Id",
                table: "PatientVisitHistory",
                column: "PatientDetails_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientLabResult");

            migrationBuilder.DropTable(
                name: "PatientMedication");

            migrationBuilder.DropTable(
                name: "PatientVaccinationData");

            migrationBuilder.DropTable(
                name: "PatientVisitHistory");

            migrationBuilder.DropTable(
                name: "PatientLabVisit");

            migrationBuilder.DropTable(
                name: "PatientDetails");
        }
    }
}

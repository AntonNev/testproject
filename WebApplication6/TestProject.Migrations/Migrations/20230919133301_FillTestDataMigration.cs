using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Data.Migrations
{
    public partial class FillTestDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
insert into ProgrammingLanguages (Name)
values 
    ('C#'),
    ('Java');

insert into Departments (Name, Floor)
values
    ('First', 1),
    ('Second', 2);

insert into Persons (Name, Surname, Age, Gender, DepartmentId)
values
    ('N1', 'S1', 23, 1, 1),
    ('N2', 'S2', 36, 2, 1),
    ('N3', 'S3', 19, 2, 2);

insert into JobExperiences (PersonId, ProgrammingLanguageId)
values
    (1, 1),
    (1, 2),
    (2, 1),
    (3, 2);
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

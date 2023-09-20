using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entities
{
    public class Person : ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
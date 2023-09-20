using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Floor { get; set; }
    }
}
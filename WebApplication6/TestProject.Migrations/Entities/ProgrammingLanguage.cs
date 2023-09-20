using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entities
{
    public class ProgrammingLanguage
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
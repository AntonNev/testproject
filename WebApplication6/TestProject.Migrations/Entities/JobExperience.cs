using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.Data.Entities
{
    public class JobExperience
    {
        [Key, Column(Order = 0)]
        public int PersonId { get; set; }
        public Person Person { get; set; }

        [Key, Column(Order = 1)]
        public int ProgrammingLanguageId { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
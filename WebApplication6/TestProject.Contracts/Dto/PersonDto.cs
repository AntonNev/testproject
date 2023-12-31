﻿using TestProject.Data.Entities;

namespace TestProject.Contracts.Dto
{
    public class PersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }
    }
}

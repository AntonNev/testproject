using System;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime LastActiveTime { get; set; }
    }
}
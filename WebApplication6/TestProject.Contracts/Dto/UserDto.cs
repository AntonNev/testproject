using System;

namespace TestProject.Contracts.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime LastActiveTime { get; set; }
    }
}
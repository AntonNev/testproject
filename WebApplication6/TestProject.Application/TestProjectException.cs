using System;

namespace TestProject.Application
{
    public class TestProjectException : Exception
    {
        public TestProjectException(string message) : base(message)
        {
        }
    }
}
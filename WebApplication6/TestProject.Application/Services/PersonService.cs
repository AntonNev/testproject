using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Contracts;
using TestProject.Contracts.Dto;
using TestProject.Data;
using TestProject.Data.Entities;

namespace TestProject.Application
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PersonService> _logger;

        public PersonService(ApplicationDbContext dbContext, ILogger<PersonService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync(CancellationToken token)
        {
            return await _dbContext.Persons.ToListAsync(token);
        }

        public async Task AddPersonAsync(PersonDto person, CancellationToken token)
        {
            if (person == null)
            {
                _logger.LogWarning($"Add person: person is null");
                throw new TestProjectException($"Person is null");
            }

            var dbPerson = new Person()
            {
                Name = person.Name,
                Age = person.Age,
                Gender = person.Gender,
                Surname = person.Surname,
                DepartmentId = person.DepartmentId
            };

            await _dbContext.AddAsync(dbPerson, token);
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task ChangePersonAsync(PersonDto person, CancellationToken token)
        {
            if (person == null)
            {
                _logger.LogWarning($"Change person: person is null");
                throw new TestProjectException($"Person is null");
            }

            var dbPerson = await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id == person.Id, token);
            if (dbPerson == null)
            {
                _logger.LogWarning($"Change person: person not found for id {person.Id}");
                throw new TestProjectException($"Person not found for id {person.Id}");
            }

            dbPerson.Age = person.Age;
            dbPerson.DepartmentId = person.DepartmentId;
            dbPerson.Gender = person.Gender;
            dbPerson.Surname = person.Surname;
            dbPerson.Name = person.Name;

            await _dbContext.SaveChangesAsync(token);
        }

        public async Task DeletePersonAsync(int personId, CancellationToken token)
        {
            var dbPerson = await _dbContext.Persons
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == personId, token);
            if (dbPerson == null)
            {
                _logger.LogWarning($"Delete person: person not found for id {personId}");
                return;
            }

            dbPerson.IsDeleted = true;

            await _dbContext.SaveChangesAsync(token);
        }
    }
}
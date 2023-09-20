using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Contracts;
using TestProject.Contracts.Dto;
using TestProject.Data.Entities;

namespace TestProject.Application
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync(CancellationToken token);

        Task AddPersonAsync(PersonDto person, CancellationToken token);

        Task ChangePersonAsync(PersonDto person, CancellationToken token);

        Task DeletePersonAsync(int personId, CancellationToken token);
    }
}

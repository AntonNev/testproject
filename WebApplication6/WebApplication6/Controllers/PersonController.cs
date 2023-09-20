using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Application;
using TestProject.Authentication;
using TestProject.Contracts;
using TestProject.Contracts.Dto;
using TestProject.Data;
using TestProject.Data.Entities;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(ApplicationDbContext dbContext, IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get(CancellationToken cancellationToken)
        {
            return await _personService.GetAllPersonsAsync(cancellationToken);
        }

        [HttpPost]
        public async Task Add(PersonDto person, CancellationToken cancellationToken)
        {
            await _personService.AddPersonAsync(person, cancellationToken);
        }

        [HttpPut]
        public async Task Change(PersonDto person, CancellationToken cancellationToken)
        {
            await _personService.ChangePersonAsync(person, cancellationToken);
        }

        [HttpDelete]
        public async Task Delete(int personId, CancellationToken cancellationToken)
        {
            await _personService.DeletePersonAsync(personId, cancellationToken);
        }
    }
}

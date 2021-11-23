using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public UsersController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get(string? sort, string? searchName, string? searchLastName, string? searchPhoneNumber, int? pageNumber, int? pageSize)
        {
            int currentPageSize = pageSize ?? 5;
            int currentPageNumber = pageNumber ?? 1;
            string currentSort = sort ?? "desc";
            IQueryable<User> users = _dbContext.Users;

            if (searchName != null || searchLastName != null || searchPhoneNumber != null)
            {
                currentPageNumber = 1;
                if (searchName != null)
                {
                    users = users.Where(s => s.Name.Contains(searchName));
                }
                if (searchLastName != null)
                {
                    users = users.Where(s => s.LastName.Contains(searchLastName));
                }
                if (searchPhoneNumber != null)
                {
                    users = users.Where(s => s.PhoneNumber.Contains(searchPhoneNumber));
                }
            }
            switch (currentSort)
            {
                case "desc":
                    users = users.OrderByDescending(x => x.LastName);
                    break;
                case "asc":
                    users = users.OrderBy(x => x.LastName);
                    break;
            }
            return Ok(users.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest(ModelState);
            }
                
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User userObj)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                user.Name = userObj.Name;
                user.LastName = userObj.LastName;
                user.PhoneNumber = userObj.PhoneNumber;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted");
            }
        }
    }
}

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
    public class CatsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public CatsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        // GET: api/<CatsController>
        [HttpGet]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
        {
            int currentPageSize = pageSize ?? 5;
            int currentPageNumber = pageNumber ?? 1;
            var cats = await _dbContext.Cats.ToListAsync();
            return Ok(cats.Skip((currentPageNumber -1) * currentPageSize).Take(currentPageSize));
        }

        // GET api/<CatsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await _dbContext.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(cat);
        }

        // POST api/<CatsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cat cat)
        {
            await _dbContext.Cats.AddAsync(cat);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CatsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cat catObj)
        {
            var cat = await _dbContext.Cats.FindAsync(id);
            if(cat == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                cat.Name = catObj.Name;
                cat.Age = catObj.Age;
                cat.Breed = catObj.Breed;
                cat.Sex = catObj.Sex;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<CatsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _dbContext.Cats.FindAsync(id);
            if(cat == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Cats.Remove(cat);
                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted");
            }
            
        }
        //GET SearchCats
        [HttpGet("[action]")]
        public async Task<IActionResult> SearchCats(string query)
        {
            var cat = await _dbContext.Cats.Where(x => x.Name.StartsWith(query)).ToListAsync();
            if (cat == null)
            {
                return NotFound("No record found");
            }
            return Ok(cat);
        }

    }
}

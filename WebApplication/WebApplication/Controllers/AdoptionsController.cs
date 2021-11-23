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
    public class AdoptionsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public AdoptionsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<AdoptionsController>
        [HttpGet]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
        {
            int currentPageSize = pageSize ?? 5;
            int currentPageNumber = pageNumber ?? 1;
            var adoptions = await _dbContext.Adoptions.ToListAsync();
            return Ok(adoptions.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }
        // GET: api/<AdoptionsController>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSortedByDate(string sort)
        {
            IQueryable<Adoption> adoptions;
            switch (sort)
            {
                case "desc":
                    adoptions = _dbContext.Adoptions.OrderByDescending(x => x.Date);
                    break;
                case "asc":
                    adoptions = _dbContext.Adoptions.OrderBy(x => x.Date);
                    break;
                default:
                    adoptions = _dbContext.Adoptions;
                    break;
            }
            return Ok(adoptions);
        }
        // GET api/<AdoptionsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var adoption = await _dbContext.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(adoption);
        }

        // POST api/<AdoptionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Adoption adoption)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.Adoptions.AddAsync(adoption);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<AdoptionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Adoption adoptionObj)
        {
            var adoption = await _dbContext.Adoptions.FindAsync(id);
            var user = await _dbContext.Users.FindAsync(adoptionObj.User);
            var cat = _dbContext.Cats.Find(adoptionObj.Cat);

            if (adoption == null || cat == null || user == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                adoption.Date = adoptionObj.Date;
                adoption.Cat = adoptionObj.Cat;
                adoption.User = adoptionObj.User;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<AdoptionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var adoption = await _dbContext.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Adoptions.Remove(adoption);
                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted");
            }
        }
    }
}

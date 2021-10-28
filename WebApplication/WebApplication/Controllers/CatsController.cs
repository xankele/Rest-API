using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Cat> Get()
        {
            return _dbContext.Cats;
        }

        // GET api/<CatsController>/5
        [HttpGet("{id}")]
        public Cat Get(int id)
        {
            var cat = _dbContext.Cats.Find(id);
            return cat;
        }

        // POST api/<CatsController>
        [HttpPost]
        public void Post([FromBody] Cat cat)
        {
            _dbContext.Cats.Add(cat);
            _dbContext.SaveChanges();
        }

        // PUT api/<CatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cat catObj)
        {
            var cat = _dbContext.Cats.Find(id);
            cat.Name = catObj.Name;
            cat.Age = catObj.Age;
            cat.Breed = catObj.Breed;
            cat.Sex = catObj.Sex;
            _dbContext.SaveChanges();

        }

        // DELETE api/<CatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cat = _dbContext.Cats.Find(id);
            _dbContext.Cats.Remove(cat);
            _dbContext.SaveChanges();
        }
    }
}

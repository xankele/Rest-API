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
    public class AdoptionsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public AdoptionsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<AdoptionsController>
        [HttpGet]
        public IEnumerable<Adoption> Get()
        {
            return _dbContext.Adoptions;
        }

        // GET api/<AdoptionsController>/5
        [HttpGet("{id}")]
        public Adoption Get(int id)
        {
            var adoption = _dbContext.Adoptions.Find(id);
            return adoption;
        }

        // POST api/<AdoptionsController>
        [HttpPost]
        public void Post([FromBody] Adoption adoption)
        {
            _dbContext.Adoptions.Add(adoption);
            _dbContext.SaveChanges();
        }

        // PUT api/<AdoptionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Adoption adoptionObj)
        {
            var adoption = _dbContext.Adoptions.Find(id);
            adoption.Date = adoptionObj.Date;
            adoption.Cat = adoptionObj.Cat;
            adoption.User = adoptionObj.User;
            _dbContext.SaveChanges();
        }

        // DELETE api/<AdoptionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var adoption = _dbContext.Adoptions.Find(id);
            _dbContext.Adoptions.Remove(adoption);
            _dbContext.SaveChanges();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private static List<Cat> cats = new List<Cat>()
        {
            new Cat(){Id = 1, Age = 2, Breed = "serval", Name = "Ania", Sex = "female" }
        };
        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return cats;
        }
        [HttpPost]
        public void Post([FromBody] Cat cat)
        {
            cats.Add(cat);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cat cat)
        {
            cats[id] = cat;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cats.RemoveAt(id);
        }
    }
    
}

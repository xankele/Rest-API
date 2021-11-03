using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public enum Sex
    {
        Female,
        Male
    }
    public class Cat
    {
//[Key]
       // [Required]
        public int Id { get; set; }
        //[Required]
       // [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; }
        //[Required]
        public Sex Sex { get; set; }
        //[Required]
        //[StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Breed { get; set; }
        //[Required]
        //[Range(0, 200)]
        public int Age { get; set; }
    }
}

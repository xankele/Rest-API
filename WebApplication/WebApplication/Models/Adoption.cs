﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Adoption
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int IdCat { get; set; }
        public int IdUser { get; set; }
        
    }
}

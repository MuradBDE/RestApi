﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
    }
}

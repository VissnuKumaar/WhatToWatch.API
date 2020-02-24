using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.API.Models
{
    public class MovieRequest
    {
        public string genre { get; set; }
        public string imdb_start { get; set; }
        public string imdb_end { get; set; }
        public string sources { get; set; }
        public string year_start { get; set; }
        public string year_end { get; set; }
    }
}

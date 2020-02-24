using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.API.Models
{
    public class MovieResponse
    {
        public string id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public string imdb_rating { get; set; }
        public string released_on { get; set; }
        public string classification { get; set; }
        public string poster_url { get; set; }
    }
}

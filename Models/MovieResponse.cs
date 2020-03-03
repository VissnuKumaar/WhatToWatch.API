using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.API.Models
{
    public class MovieResponse
    {
        public bool status { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public string imdb_rating { get; set; }
        public string released_on { get; set; }
        public string classification { get; set; }
        public string poster_url { get; set; }
        public int runtime { get; set; }
        public string trailer { get; set; }
        public string applaunch { get; set; }
        public string android_launch { get; set; }
        public string genre { get; set; }
        public string source { get; set; }
    }
}

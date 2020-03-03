using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.API.Helper
{
    public class MovieHelper
    {
        public int randomCounter(string source, int imdb_diff, int year_diff)
        {
            int imdbcount = imdb_diff < 2 ? 10 : imdb_diff < 3 ? 30 : imdb_diff < 4 ? 40 : imdb_diff < 5 ? 50 : 800;
            int yearcount = year_diff < 2 ? 80 : year_diff < 3 ? 150 : year_diff < 4 ? 250 : year_diff < 5 ? 350 : 800;
            int streamCount = source == "hbo" || source == "disney" ? 300 : source == "hulu" || source == "amazon" ? 500 : 800;
            return Math.Min(Math.Min(imdbcount, yearcount), streamCount);
        }
    }
}

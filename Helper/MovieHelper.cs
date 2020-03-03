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
            if (imdb_diff < 2 || year_diff < 2) return 30;
            else if (imdb_diff < 3 || year_diff < 3) return 60;
            else if (imdb_diff < 5 || year_diff < 5) return 100;
            else if (source == "hbo" || source == "disney") return 500;
            else if (source == "hulu" || source == "amazon") return 700;
            else return 1000;
        }
    }
}

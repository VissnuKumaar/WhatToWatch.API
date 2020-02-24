using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatToWatch.API.Models;

namespace WhatToWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> GetMovieDataAsync([FromBody]MovieRequest movieRequest)
        {
            MovieResponse apiResponse = new MovieResponse();
            var source = "netflix";
            int result = 0;
            int count = 1000;
            try
            {
                do
                {
                    if (movieRequest.sources == "hulu")
                    {
                        source = "hulu_plus";
                        count = 300;
                    }
                    else if (movieRequest.sources == "amazon")
                    {
                        source = "amazon_prime";
                        count = 300;
                    }
                    Random random = new Random();
                    int randSkip = random.Next(0, count);
                    int randTake = random.Next(1, 50);
                    var movieApiUrl = "*******" + movieRequest.sources + "*****"+source+"******" + source;
                    movieApiUrl = movieApiUrl + "&genre=" + movieRequest.genre;
                    movieApiUrl = movieApiUrl + "&imdb_start=" + movieRequest.imdb_start;
                    movieApiUrl = movieApiUrl + "&imdb_end=" + movieRequest.imdb_end;
                    movieApiUrl = movieApiUrl + "&skip=" + randSkip.ToString();
                    movieApiUrl = movieApiUrl + "&take=" + randTake.ToString();
                    movieApiUrl = movieApiUrl + "&year_start=" + movieRequest.year_start;
                    movieApiUrl = movieApiUrl + "&year_end=" + movieRequest.year_end;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(movieApiUrl);
                    var model = await response.Content.ReadAsAsync<List<MovieResponse>>();
                    if (model.Count != 0)
                    {
                        result = 1;
                    }
                    else
                    {
                        continue;
                    }
                    apiResponse.id = model[1].id;
                    apiResponse.title = model[1].title;
                    apiResponse.overview = model[1].overview;
                    apiResponse.released_on = model[1].released_on;
                    apiResponse.imdb_rating = model[1].imdb_rating;
                    apiResponse.classification = model[1].classification;
                    apiResponse.poster_url = "****" + model[1].id + "/poster-342.webp";
                } while (result == 0);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return apiResponse;
        }
    }
}
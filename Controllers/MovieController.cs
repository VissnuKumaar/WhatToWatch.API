using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WhatToWatch.API.Helper;
using WhatToWatch.API.Models;

namespace WhatToWatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        MovieHelper helper = new MovieHelper();

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> GetMovieDataAsync([FromBody]MovieRequest movieRequest)
        {
            MovieResponse apiResponse = new MovieResponse();
            int year_diff = Convert.ToInt32(movieRequest.year_end) - Convert.ToInt32(movieRequest.year_start);
            int imdb_diff = Convert.ToInt32(movieRequest.imdb_end) - Convert.ToInt32(movieRequest.imdb_start);
            string stream_type = movieRequest.sources;
            string source = (stream_type == "netflix") ? "netflix" : (stream_type == "amazon") ? "amazon_prime" : (stream_type == "disney") ? "disney_plus" : (stream_type == "hulu") ? "hulu_plus" : "hbo";
            int count = helper.randomCounter(stream_type, imdb_diff, year_diff);
            int result = 0;
            int counter = 0;
            try
            {
                do
                {
                    Random random = new Random();
                    int randSkip = random.Next(0, count);
                    int randTake = random.Next(1, 50);
                    var movieApiUrl = "https://******/" + movieRequest.sources + "?*********" + source + "&rt_end=100&rt_start=0&sort=4&sources=" + source;
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
                    if (counter > 15)
                    {
                        result = 1;
                        apiResponse.status = false;
                        break;
                    }
                    if (model.Count != 0)
                    {
                        result = 1;
                        var movieDetailUrl = "https://api.reelgood.com/v3.0/content/movie/" + model[0].id + "?availability=onAnySource&interaction=true&region=us";
                        var detailResponse = await client.GetAsync(movieDetailUrl);
                        var detailModel = await detailResponse.Content.ReadAsStringAsync();
                        var models = JsonConvert.DeserializeObject<test>(detailModel);
                        var a = models.trailer;
                        if (a == null)
                        {
                            apiResponse.runtime = 0;
                            apiResponse.trailer = null;
                            apiResponse.applaunch = null;
                        }
                        else
                        {
                            var detailsModel = await detailResponse.Content.ReadAsAsync<MovieDetails>();
                            apiResponse.runtime = detailsModel.runtime;
                            apiResponse.trailer = detailsModel.trailer.key;
                            for(int i = 0; i < 3; i++)
                            {
                                if (detailsModel.availability[i].source_name == source)
                                {
                                    apiResponse.applaunch = detailsModel.availability[i].source_data.links.web;
                                }
                            }
                        }
                        apiResponse.status = true;
                        apiResponse.id = model[0].id;
                        apiResponse.title = model[0].title;
                        apiResponse.overview = model[0].overview;
                        apiResponse.released_on = model[0].released_on;
                        apiResponse.imdb_rating = model[0].imdb_rating;
                        apiResponse.classification = model[0].classification;
                        apiResponse.poster_url = "**********" + model[0].id + "/******.webp";
                        apiResponse.genre = movieRequest.genre_name;
                        apiResponse.source = movieRequest.sources;
                    }
                    else
                    {
                        ++counter;
                        count = (counter > 10) ? (count - 10) : (counter > 13) ? 10 : count;
                        continue;
                    }

                } while (result == 0);
            }
            catch (Exception e)
            {
                apiResponse.status = false;
            }
            return apiResponse;
        }
    }
}

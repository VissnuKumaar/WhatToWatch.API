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
        public async Task<ActionResult<List<MovieResponse>>> GetMovieDataAsync([FromBody]MovieRequest movieRequest)
        {
            List<MovieResponse> randomMovies = new List<MovieResponse>();
            int year_diff = Convert.ToInt32(movieRequest.year_end) - Convert.ToInt32(movieRequest.year_start);
            int imdb_diff = Convert.ToInt32(movieRequest.imdb_end) - Convert.ToInt32(movieRequest.imdb_start);
            string stream_type = movieRequest.sources;
            string source = (stream_type == "netflix") ? "netflix" : (stream_type == "amazon") ? "amazon_prime" : (stream_type == "disney") ? "disney_plus" : (stream_type == "hulu") ? "hulu_plus" : "hbo";
            int count = helper.randomCounter(stream_type, imdb_diff, year_diff);
            int result = 0;
            int counter = 0;
            int tempSkip = 0;
            try
            {
                do
                {
                    Random random = new Random();
                    int randSkip = counter == 0 ? random.Next(0, count) : random.Next(0, tempSkip - 1);
                    tempSkip = randSkip;
                    int randTake = random.Next(1, 20);
                    int randSort = random.Next(1, 3);
                    var movieApiUrl = "****" + source + "***" + source + "****" + randSort + "***" + source;
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
                    List<MovieResponse> model = await response.Content.ReadAsAsync<List<MovieResponse>>();
                    if (counter > 15)
                    {
                        result = 1;
                        randomMovies[0].status = false;
                        break;
                    }
                    if (model.Count != 0)
                    {
                        for (int modelCount = 0; modelCount < model.Count; ++modelCount)
                        {
                            MovieResponse apiResponse = new MovieResponse();
                            result = 1;
                            var movieDetailUrl = "******" + model[modelCount].id + "****";
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
                                for (int i = 0; i < 3; i++)
                                {
                                    if (detailsModel.availability[i].source_name == source)
                                    {
                                        apiResponse.applaunch = detailsModel.availability[i].source_data.links.web;
                                        apiResponse.android_launch = detailsModel.availability[i].source_data.links.android;
                                        break;
                                    }
                                }
                            }
                            apiResponse.status = true;
                            apiResponse.id = model[modelCount].id;
                            apiResponse.title = model[modelCount].title;
                            apiResponse.overview = model[modelCount].overview;
                            apiResponse.released_on = model[modelCount].released_on;
                            apiResponse.imdb_rating = model[modelCount].imdb_rating;
                            apiResponse.classification = model[modelCount].classification;
                            apiResponse.poster_url = "****" + model[modelCount].id + "***";
                            apiResponse.genre = movieRequest.genre_name;
                            apiResponse.source = movieRequest.sources;
                            randomMovies.Add(apiResponse);
                        }
                    }
                    else
                    {
                        ++counter;
                        if (counter >= 14) count = 1;
                        else if (counter >= 12) count = 8;
                        else if (counter >= 10) count = count - 100 < 0 ? Math.Abs(count - 100) : count - 100;
                        else if (counter >= 5) count = count - 50 < 0 ? Math.Abs(count - 50) : count - 50;
                        continue;
                    }

                } while (result == 0);
            }
            catch (Exception e)
            {
                randomMovies[0].status = false;
            }
            return randomMovies;
        }
    }
}


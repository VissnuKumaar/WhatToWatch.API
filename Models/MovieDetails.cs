using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatToWatch.API.Models
{
    public class Trailer
    {
        public string site { get; set; }
        public string key { get; set; }
        public object url { get; set; }
    }

    public class Trailer2
    {
        public string site { get; set; }
        public string key { get; set; }
        public object url { get; set; }
    }

    public class Tag
    {
        public string slug { get; set; }
        public string display_name { get; set; }
    }

    public class Links
    {
        public string web { get; set; }
        public string ios { get; set; }
        public string android { get; set; }
    }

    public class Web
    {
        public string movie_id { get; set; }
    }

    public class Ios
    {
        public string movie_id { get; set; }
    }

    public class Android
    {
        public string movie_id { get; set; }
    }

    public class References
    {
        public Web web { get; set; }
        public Ios ios { get; set; }
        public Android android { get; set; }
    }

    public class SourceData
    {
        public Links links { get; set; }
        public References references { get; set; }
        public string web_link { get; set; }
    }

    public class Availability
    {
        public string source_id { get; set; }
        public string source_name { get; set; }
        public int access_type { get; set; }
        public SourceData source_data { get; set; }
        public object rental_cost_hd { get; set; }
        public object rental_cost_sd { get; set; }
        public double? purchase_cost_hd { get; set; }
        public double? purchase_cost_sd { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public DateTime birthdate { get; set; }
        public bool has_poster { get; set; }
        public bool has_square { get; set; }
        public int role_type { get; set; }
        public string role { get; set; }
        public int? rank { get; set; }
    }

    public class Streamability
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    public class ListingKey
    {
        public string listing_type { get; set; }
        public string listing_identifier { get; set; }
    }

    public class Rank
    {
        public int rank { get; set; }
        public string text { get; set; }
        public ListingKey listing_key { get; set; }
    }

    public class AlsoWatched
    {
        public string id { get; set; }
        public string content_type { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
    }

    public class Content
    {
        public string text { get; set; }
        public List<Rank> ranks { get; set; }
        public List<AlsoWatched> also_watched { get; set; }
    }

    public class ScoreBreakdown
    {
        public List<Streamability> streamability { get; set; }
        public Content content { get; set; }
    }

    public class ReelgoodScores
    {
        public double streamability { get; set; }
        public double content { get; set; }
        public object follow_through { get; set; }
        public int reelgood_rank { get; set; }
        public double reelgood_popularity { get; set; }
    }

    public class MovieDetails
    {
        public string id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public string tagline { get; set; }
        public string reelgood_synopsis { get; set; }
        public string classification { get; set; }
        public int runtime { get; set; }
        public DateTime released_on { get; set; }
        public Trailer trailer { get; set; }
        public List<Trailer2> trailers { get; set; }
        public string language { get; set; }
        public bool has_poster { get; set; }
        public bool has_backdrop { get; set; }
        public double imdb_rating { get; set; }
        public int rt_critics_rating { get; set; }
        public int rt_audience_rating { get; set; }
        public bool watchlisted { get; set; }
        public bool seen { get; set; }
        public List<string> sources { get; set; }
        public bool on_free { get; set; }
        public bool on_rent_purchase { get; set; }
        public List<int> genres { get; set; }
        public List<Tag> tags { get; set; }
        public List<string> countries { get; set; }
        public List<Availability> availability { get; set; }
        public List<Person> people { get; set; }
        public ScoreBreakdown score_breakdown { get; set; }
        public ReelgoodScores reelgood_scores { get; set; }
    }

    public class test
    {
        public Trailer trailer { get; set; }
    }
}

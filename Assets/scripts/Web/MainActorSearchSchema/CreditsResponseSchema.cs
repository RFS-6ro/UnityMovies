using System;
using System.Collections.Generic;

namespace WebJSONFormats
{
    [Serializable]
    public class CreditsResponseSchema
    {
        public bool adult;
        public string backdrop_path;
        public CollectionSchema belongs_to_collection;
        public int budget;
        public List<Genres> genres;
        public string homepage;
        public int id;
        public string imdb_id;
        public string original_language;
        public string original_title;
        public string overview;
        public float popularity;
        public string poster_path;
        public List<ProductionCompany> production_companies;
        public List<ProductionCountry> production_countries;
        public string release_date;
        public long revenue;
        public int runtime;
        public List<SpokenLanguage> spoken_languages;
        public string status;
        public string tagline;
        public string title;
        public bool video;
        public float vote_average;
        public int vote_count;
        public Credits credits;
    }
}

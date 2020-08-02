using System;

namespace WebJSONFormats
{
    [Serializable]
    public class MovieResultSchema
    {
        public float popularity;

        public int id;

        public bool video;

        public int vote_count;

        public float vote_average;

        public string title;

        public string release_date;

        public string original_language;

        public string original_title;

        public int[] genre_ids;

        public string backdrop_path;

        public bool adult;

        public string overview;

        public string poster_path;
    }
}

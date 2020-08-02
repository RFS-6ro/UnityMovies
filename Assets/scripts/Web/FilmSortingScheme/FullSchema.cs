using System;
using System.Collections.Generic;

namespace WebJSONFormats
{
    [Serializable]
    public class FullSchema
    {
        public int page;

        public int total_results;

        public int total_pages;

        public List<MovieResultSchema> results;
    }
}

using System;
using UnityEngine;

namespace UnityMovies.Model
{
    [Serializable]
    public class MovieData
    {
        private Sprite _poster;

        private float _rating;

        private string _title;

        private string _releaseDate;

        private string _overview;

        private string _fullDescriptionLink;

        private int _id;

        private string _mainActorName;

        public Sprite Poster { get => _poster; set => _poster = value; }

        public float Rating { get => _rating; set => _rating = (value >= 0.0f && value <= 10.0f) ? value : 0f; }

        public string Title { get => _title; set => _title = value; }

        public string ReleaseDate { get => _releaseDate; set => _releaseDate = value; }

        public string Overview { get => _overview; set => _overview = value; }

        public string FullDescriptionLink { get => _fullDescriptionLink; set => _fullDescriptionLink = value; }

        public int Id { get => _id; set => _id = value; }

        public string MainActorName { get => _mainActorName; set => _mainActorName = value; }
    }
}

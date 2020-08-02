using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityMovies.Controller;
using UnityMovies.View;
using WebJSONFormats;

namespace UnityMovies.Model
{
    public class MovieLoader : MonoBehaviour
    {
        private string _apiKey = @"1bdd774b4e1b63ea4348a44eaf4ff3c1";

        public ObservableCollection<MovieData> Movies;

        private void Awake()
        {
            Movies = new ObservableCollection<MovieData>();

            GetMoviesAsync();
        }

        /// <summary>
        /// Load Most popular movies in 2019 async
        /// </summary>
        public async void GetMoviesAsync()
        {
            Movies.Clear();

            string uri = @"https://api.themoviedb.org/3/discover/movie?primary_release_year=2019&sort_by=popularity.desc&api_key=" + _apiKey;

            FullSchema moviesResponse = await DeserializeFromUriAsync<FullSchema>(uri);

            if (moviesResponse != null)
            {
                foreach (var movie in moviesResponse.results)
                {
                    Movies.Add(await SetFilmDataAsync(movie));
                }
            }
        }

        /// <summary>
        /// Filter movie result
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Filtered MovieData</returns>
        private async Task<MovieData> SetFilmDataAsync(MovieResultSchema movie)
        {
            MovieData tempData = new MovieData();

            tempData.Overview = movie.overview;

            int MovieId = movie.id;

            tempData.Id = MovieId;
            
            tempData.FullDescriptionLink = @"https://www.themoviedb.org/movie/" + MovieId.ToString();

            if (movie.poster_path != null)
            {
                string posterPath = @"https://image.tmdb.org/t/p/original/" + movie.poster_path.Remove(0, 1);
                tempData.Poster = await GetImageByUriAsync(posterPath);
            }
            else
            {
                tempData.Poster = null;
            }

            tempData.ReleaseDate = movie.release_date;

            tempData.Rating = movie.vote_average;

            tempData.Title = movie.title + $"({movie.original_title})";

            #region working searching main actor's name by movie id

            tempData.MainActorName = await GetMainActorbyMovieID(MovieId);

            #endregion

            return tempData;
        }

        /// <summary>
        /// Get Main Actor's name by movie credits
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<string> GetMainActorbyMovieID(int id)
        {
            var uri = @"https://api.themoviedb.org/3/movie/" + id.ToString() + $"?api_key={_apiKey}&append_to_response=credits";

            CreditsResponseSchema credits = await DeserializeFromUriAsync<CreditsResponseSchema>(uri);

             return credits.credits.cast[0].name;
        }

        /// <summary>
        /// Generate Object of type T, loaded from web page by URI async
        /// </summary>
        /// <typeparam name="T">
        /// Type of response
        /// </typeparam>
        private async Task<T> DeserializeFromUriAsync<T>(string uri)
        {
            string jsonResponse = default(string);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    jsonResponse = await response.Content.ReadAsStringAsync();
                }

                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
                return default(T);
            }
        }

        /// <summary>
        /// Generate sprite from URI async
        /// </summary>
        private async Task<Sprite> GetImageByUriAsync(string uri)
        {
            byte[] imageBytes;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    imageBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }

                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(imageBytes))
                {
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100.0f, 0, SpriteMeshType.Tight);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
                return null;
            }
        }

        private void ShowErrorMessage(string message)
        {
            FindObjectOfType<ViewController>().TurnViewOn(ViewTypes.ErrorCanvas, true,
            () => {
                FindObjectOfType<ErrorView>().SetMessage(message);
            });
        }
    }
}

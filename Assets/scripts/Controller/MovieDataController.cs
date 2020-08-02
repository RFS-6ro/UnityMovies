using UnityEngine;
using UnityMovies.Model;
using UnityMovies.View;

namespace UnityMovies.Controller
{
    public class MovieDataController : MonoBehaviour
    {
        //MVC
        private MovieDataView _movieDataView;
        private MovieLoader _movieLoaderModel;
        
        //cache
        private int _currentMovieIndex;
        private MovieData _currentMovieData;
        
        #region Unity Methods
        private void Awake()
        {
            _movieDataView = FindObjectOfType<MovieDataView>();
            _movieLoaderModel = FindObjectOfType<MovieLoader>();

            _currentMovieIndex = 0;
        }

        private void Start()
        {
            if (_movieLoaderModel == null)
            {
                _movieLoaderModel = FindObjectOfType<MovieLoader>();
            }
            if (_movieLoaderModel != null && _movieLoaderModel.Movies != null)
            {
                _movieLoaderModel.Movies.CollectionChanged += MoviesCollectionChanged;
            }
        }

        private void OnDestroy()
        {
            if (_movieLoaderModel == null)
            {
                _movieLoaderModel = FindObjectOfType<MovieLoader>();
            }
            if (_movieLoaderModel != null && _movieLoaderModel.Movies != null)
            {
                _movieLoaderModel.Movies.CollectionChanged -= MoviesCollectionChanged;
            }
        }
        #endregion

        private void MoviesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _movieDataView = FindObjectOfType<MovieDataView>();

            if (_movieDataView != null)
            {
                if (_movieDataView.IsUpdated == false)
                {
                    Shift(0);
                }
                
                if (_currentMovieIndex >= _movieLoaderModel.Movies.Count)
                {
                    Shift(_currentMovieIndex - _movieLoaderModel.Movies.Count);
                }
            }
        }

        /// <summary>
        /// shifts in movie list and display it
        /// </summary>
        /// <param name="amount"></param>
        public void Shift(int amount)
        {
            if (_movieLoaderModel.Movies != null)
            {
                if ((_movieLoaderModel.Movies.Count > _currentMovieIndex + amount) && (_currentMovieIndex + amount >= 0))
                {
                    _currentMovieIndex += amount;
                    _currentMovieData = _movieLoaderModel.Movies[_currentMovieIndex];

                    if (_movieDataView == null)
                    {
                        _movieDataView = FindObjectOfType<MovieDataView>();
                    }

                    _movieDataView.UpdateMovie(_currentMovieData);
                }
            }
        }
        
        public void OpenMovieWebPage()
        {
            if (_currentMovieData != null)
            {
                Application.OpenURL(_currentMovieData.FullDescriptionLink);
            }
        }
    }
}

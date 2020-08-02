using UnityEngine;
using TMPro;
using UnityMovies.Controller;
using UnityMovies.Model;
using Vuforia;

namespace UnityMovies.View
{
    public class MovieDataView : ViewBase
    {
        //MVC
        private MovieDataController _movieDataController;
        private ViewController _pageSwitchController;

        //UI
        [SerializeField] private TMP_Text _tittle;
        [SerializeField] private TMP_Text _releaseDate;
        [SerializeField] private TMP_Text _overview;
        [SerializeField] private UnityEngine.UI.Image _poster;
        [SerializeField] private UnityEngine.UI.Image _ratingFiller;
        [SerializeField] private TMP_Text _ratingPercent;
        
        [SerializeField] private Sprite _missingSprite;

        public bool IsUpdated { get; set; }

        #region Unity Methods
        private void Awake()
        {
            IsUpdated = false;
        }
        #endregion

        #region User Input

        public void OnNextMovieClick()
        {
            if (_movieDataController == null)
            {
                _movieDataController = FindObjectOfType<MovieDataController>();
            }

            _movieDataController.Shift(1);
        }

        public void OnPreviousMovieClick()
        {
            if (_movieDataController == null)
            {
                _movieDataController = FindObjectOfType<MovieDataController>();
            }

            _movieDataController.Shift(-1);
        }

        public void OnMoreInfoClick()
        {
            if (_movieDataController == null)
            {
                _movieDataController = FindObjectOfType<MovieDataController>();
            }

            _movieDataController.OpenMovieWebPage();
        }

        public void OnPosterClick()
        {
            if (_pageSwitchController == null)
            {
                _pageSwitchController = FindObjectOfType<ViewController>();
            }
            _pageSwitchController.TurnViewOn(ViewTypes.ARRecognizer, true, () =>
            {
                //Action = enable AR camera
                Camera.main.GetComponent<VuforiaBehaviour>().enabled = true;
            });
        }
        #endregion

        /// <summary>
        /// Controller Update
        /// </summary>
        /// <param name="movie"></param>
        public void UpdateMovie(MovieData movie)
        {
            IsUpdated = true;

            _tittle.text = movie.Title;
            _releaseDate.text = movie.ReleaseDate;
            _overview.text = movie.Overview;

            if (movie.Poster == null)
            {
                _poster.sprite = _missingSprite;
            }
            else
            {
                _poster.sprite = movie.Poster;
            }

            _ratingFiller.fillAmount = (movie.Rating) / 10.0f;
            _ratingPercent.text = (movie.Rating * 10.0f).ToString() + "%";
        }

    }
}

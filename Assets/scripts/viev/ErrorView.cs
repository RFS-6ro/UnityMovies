using UnityEngine;
using TMPro;
using UnityMovies.Controller;
using UnityMovies.Model;

namespace UnityMovies.View
{
    public class ErrorView : ViewBase
    {
        [SerializeField] private TMP_Text _textBox;

        public void SetMessage(string message)
        {
            _textBox.text = message + ".\n Tap to close error and try load movies again";
        }

        public void OnClick()
        {
            FindObjectOfType<ViewController>().TurnViewOn(ViewTypes.UICanvas, true, () =>
            {
                FindObjectOfType<MovieDataView>().IsUpdated = false;
                FindObjectOfType<MovieLoader>().GetMoviesAsync();
            });
        }
    }
}

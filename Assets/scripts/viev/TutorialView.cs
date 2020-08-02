using UnityMovies.Controller;

namespace UnityMovies.View
{
    public class TutorialView : ViewBase
    {
        private ViewController _pageSwitchController;

        private void Start()
        {
            _pageSwitchController = FindObjectOfType<ViewController>();
            _pageSwitchController.TurnViewOn(ViewTypes.UICanvas);
        }
        public void OnTutorialClick()
        {
            _pageSwitchController.TurnViewOff(ViewTypes.TutorialCanvas);
        }
    }
}

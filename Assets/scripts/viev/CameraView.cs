using UnityEngine;
using UnityMovies.Controller;
using Vuforia;

namespace UnityMovies.View
{
    public class CameraView : ViewBase
    {
        //MVC
        private ViewController _pageSwitchController;

        private void Awake()
        {
            _pageSwitchController = FindObjectOfType<ViewController>();
        }

        public void OnCloseButtonClick()
        {
            _pageSwitchController.TurnViewOn(ViewTypes.UICanvas, true, () =>
            {
                //Action = disable AR camera
                Camera.main.GetComponent<VuforiaBehaviour>().enabled = false;
            });
        }
    }
}
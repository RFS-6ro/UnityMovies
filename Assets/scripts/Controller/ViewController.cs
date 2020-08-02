using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovies.View;

namespace UnityMovies.Controller
{
    public class ViewController : MonoBehaviour
    {
        public static ViewController Instance;

        private Hashtable _views;
        private List<ViewBase> _onList;
        private List<ViewBase> _offList;

        public ViewBase[] Views;
        public ViewTypes EntryViewType;
        [HideInInspector] public ViewTypes CurrentViewType;

        #region Unity Methods
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _views = new Hashtable();
                _onList = new List<ViewBase>();
                _offList = new List<ViewBase>();

                RegisterAllViews();

                CurrentViewType = EntryViewType;

                if (EntryViewType != ViewTypes.None)
                {
                    TurnViewOn(EntryViewType);
                }
            }
        }
        #endregion

        public void TurnViewOn(ViewTypes type, bool closeCurrentView = false, Action callback = null)
        {
            if (type == ViewTypes.None)
            {
                return;
            }

            if (ViewExists(type) == false)
            {
                return;
            }

            if (closeCurrentView)
            {
                TurnViewOff(CurrentViewType, type);
            }
            else
            {
                ViewBase page = GetView(type);
                page.gameObject.SetActive(true);
                page.SetState(true);
                CurrentViewType = type;
            }
            callback?.Invoke();
        }

        public void TurnViewOff(ViewTypes offType, ViewTypes onType = ViewTypes.None, bool waitForExit = false)
        {
            if (offType == ViewTypes.None)
            {
                return;
            }

            if (ViewExists(offType) == false)
            {
                return;
            }

            ViewBase offPage = GetView(offType);

            if (offPage.gameObject.activeSelf)
            {
                offPage.SetState(false);
            }

            if (waitForExit && offPage.UseAnimation)
            {
                ViewBase onPage = GetView(onType);
                StopCoroutine(nameof(WaitForViewExit));
                StartCoroutine(WaitForViewExit(onPage, offPage));
            }
            else
            {
                TurnViewOn(onType);
            }
        }

        private IEnumerator WaitForViewExit(ViewBase onView, ViewBase offView)
        {
            while (offView.TargetState != ViewBase.FLAG_NONE)
            {
                yield return null;
            }
            TurnViewOn(onView.Type);
        }

        public bool IsViewOn(ViewTypes type)
        {
            if (ViewExists(type) == false)
            {
                return false;
            }

            return GetView(type).IsOn;
        }

        private ViewBase GetView(ViewTypes type)
        {
            if (ViewExists(type) == false)
            {
                return null;
            }

            return (ViewBase)_views[type];
        }
        
        public bool ViewExists(ViewTypes type)
        {
            return _views.Contains(type);
        }

        private void RegisterAllViews()
        {
            foreach (ViewBase view in Views)
            {
                RegisterView(view);
            }
        }

        private void RegisterView(ViewBase view)
        {
            if (ViewExists(view.Type))
            {
                return;
            }

            _views.Add(view.Type, view);
            TurnViewOff(view.Type);
        }
    }
}

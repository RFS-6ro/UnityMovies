using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMovies.View
{
    public class ViewBase : MonoBehaviour
    {
        public static readonly string FLAG_ON = "On";
        public static readonly string FLAG_OFF = "Off";
        public static readonly string FLAG_NONE = "None";

        public bool UseAnimation;
        public ViewTypes Type;

        public string TargetState { get; private set; }
        public bool IsOn { get; private set; }

        public void SetState(bool state)
        {
            if (UseAnimation)
            {
                StopCoroutine(nameof(AwaitAnimation));
                StartCoroutine(AwaitAnimation(state));
            }
            else
            {
                IsOn = state;
                if (state == false)
                {
                    gameObject.SetActive(state);
                }
            }
        }

        private IEnumerator AwaitAnimation(bool state)
        {
            TargetState = state ? FLAG_ON : FLAG_OFF;

            //await for animation end
            //Animation by Tween or Animator
            yield return null;

            TargetState = FLAG_NONE;

            IsOn = state;
            gameObject.SetActive(state);
        }
    }
}

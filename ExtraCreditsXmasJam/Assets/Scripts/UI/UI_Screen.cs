using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UserInt
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]

    public class UI_Screen : MonoBehaviour
    {

        #region MAIN_VARS
        [Header("Main Properties")]
        public Selectable startSelectable;

        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();
        #endregion

        #region HELPER_VARS
        private Animator animator;
        #endregion

        #region MAIN_METHODS
        // NAME   : Awake
        // DESC   : Called one frame before Start
        // PARAMS : N/A
        // RETURN : N/A
        void Awake()
        {
            //Get the animator 
            animator = GetComponent<Animator>();
        }

        // NAME   : Start
        // DESC   : Use this for initialization
        // PARAMS : N/A
        // RETURN : N/A
        void Start()
        {


            // If the screen starts with something that can be selected highlight it
            if (startSelectable)
            {
                EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
            }
        }

        // NAME   : StartScreen
        // DESC   : Displays the screen and starts its logic
        // PARAMS : N/A
        // RETURN : N/A
        public virtual void StartScreen()
        {
            //Broadcast the starting of the screen
            if (onScreenStart != null)
            {
                onScreenStart.Invoke();
            }

            //Start animating the screen
            HandleAnimator("show");

            Debug.Log("Showing the Screen.");
        }

        // NAME   : CloseScreen
        // DESC   : Hides the screen and stops its logic
        // PARAMS : N/A
        // RETURN : N/A
        public virtual void CloseScreen()
        {
            //Broadcast the stopping of the screen
            if (onScreenClose != null)
            {
                onScreenClose.Invoke();
            }

            //Start animating the screen
            HandleAnimator("hide");
        }
        #endregion

        #region HELPER_METHODS
        // NAME   : HandleAnimator
        // DESC   : Starts the animator if it exists
        // PARAMS : trigger - name of the animation to play
        // RETURN : N/A
        private void HandleAnimator(string trigger)
        {
            //If there is an animator, start with the show animation
            if (animator != null)
            {
                animator.SetTrigger(trigger);
                Debug.Log("Moving to " + trigger + ".");
            }

            Debug.Log("No animator was found.");
        }
        #endregion
    }
}


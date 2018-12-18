using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UserInt
{
    public class UI_Sys : MonoBehaviour
    {
        #region MAIN_VARS

        [Header("Main Properties")]
        public UI_Screen startScreen;

        [Header("System Events")]
        public UnityEvent onSwitchScreen = new UnityEvent();

        [Header("Fader Properties")]
        public Image fader;
        public float fadeInDuration = 1.0f;
        public float fadeOutDuration = 1.0f;
 
        public Component[] screens = new Component[0];
        #endregion

        #region HELPER_VARS
        private UI_Screen currentScreen;
        private UI_Screen prevScreen;
        #endregion

        #region MAIN_METHODS
        // NAME   : Start
        // DESC   : Use this for initialization
        // PARAMS : N/A
        // RETURN : N/A
        void Start()
        {
            // Get all  inherited screens(active/inactive)
            screens = GetComponentsInChildren<UI_Screen>(true);

            //Initialize all screens
            InitializeScreens();

            // Show the start screen
            if(startScreen)
            {
                SwitchScreen(startScreen);
            }

            // If the fader exists, turn it on
            if(fader)
            {
                fader.gameObject.SetActive(true);
            }

            // Fade in the first screen
            FadeIn();
        }

        // NAME   : Update
        // DESC   : Update is called once per frame
        // PARAMS : N/A
        // RETURN : N/A
        void Update()
        {

        }

        // NAME   : CurrentScreen
        // DESC   : Returns the screen that is currently being displayed
        // PARAMS : N/A
        // RETURN : Returns the screen that is currently being displayed
        public UI_Screen CurrentScreen { get { return currentScreen; } }

        // NAME   : PreviousScreen
        // DESC   : Returns the screen that was previously displayed
        // PARAMS : N/A
        // RETURN : Returns the screen that was previously displayed
        public UI_Screen PreviousScreen { get { return prevScreen; } }

        // NAME   : SwitchScreen
        // DESC   : Displays the specified screen
        // PARAMS : Screen - The screen to switch to
        // RETURN : N/A
        public void SwitchScreen(UI_Screen screen)
        {
            // Check if its a valid screen
            if(screen)
            {
                // Check if the current screen exists
                if(currentScreen)
                {
                    // Close the current screen and mark as previous
                    currentScreen.CloseScreen();
                    prevScreen = currentScreen;
                }
                Debug.Log("Starting the screen.");
                //Swap screens and display the current screen
                currentScreen = screen;
                currentScreen.gameObject.SetActive(true);
                currentScreen.StartScreen();

               


                //Broadcast the switching of screens
                if(onSwitchScreen != null)
                {
                    onSwitchScreen.Invoke();
                }
            }
        }

        // NAME   : GoToPreviousScreen
        // DESC   : Switches to the previous screen
        // PARAMS : N/A
        // RETURN : N/A
        public void GoToPreviousScreen()
        {
            // Switch to the previous screen if it exists
            if(prevScreen)
            {
                SwitchScreen(prevScreen);
            }
        }

        // NAME   : LoadScene
        // DESC   : Load the specified scene
        // PARAMS : sceneIndex - Number of the scene to load
        // RETURN : N/A
        public void LoadScene(int sceneIndex)
        {
            // Start loading the scene
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }

        // NAME   : FadeIn
        // DESC   : Transitions from black to the screen
        // PARAMS : N/A
        // RETURN : N/A
        public void FadeIn()
        {
            //If the fader exists, go from black to the screen
            if(fader)
            {
                fader.CrossFadeAlpha(0.0f, fadeInDuration, false);
            }
        }

        // NAME   : FadeOut
        // DESC   : Transitions from the screen to black
        // PARAMS : N/A
        // RETURN : N/A
        public void FadeOut()
        {
            //If the fader exists, go from the screen to black
            if (fader)
            {
                fader.CrossFadeAlpha(1.0f, fadeOutDuration, false);
            }
        }
        #endregion

        #region HELPER_METHODS
        // NAME   : WaitToLoadScene
        // DESC   : Starts the process of loading a scene
        // PARAMS : sceneIndex - Number of the scene to load
        // RETURN : TBD
        private IEnumerator WaitToLoadScene(int sceneIndex)
        {
            // TBD
            yield return null;
        }

        // NAME   : InitializeScreens
        // DESC   : Sets all screens to active
        // PARAMS : N/A
        // RETURN : N/A
        private void InitializeScreens()
        {
            //For each screen, set it to active
            foreach(var screen in screens)
            {
                screen.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}


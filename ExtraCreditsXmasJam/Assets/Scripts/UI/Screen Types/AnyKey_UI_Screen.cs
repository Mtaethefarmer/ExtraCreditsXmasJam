using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInt
{
    public class AnyKey_UI_Screen : UI_Screen
    {
        #region MAIN_VARS
        [Header("AnyKey Screen Properties")]
        public UI_Screen nextScreen;
        #endregion

        #region HELPERS_VARS

        #endregion

        #region MAIN_METHODS
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
			//Check if any keyboard input has been pressed
			if(Input.anyKeyDown)
			{
				//Change to the next scene
				if(nextScreen)
				{
					
				}
			}
        }
        #endregion

        #region HELPER_METHIDS

        #endregion



    }
}


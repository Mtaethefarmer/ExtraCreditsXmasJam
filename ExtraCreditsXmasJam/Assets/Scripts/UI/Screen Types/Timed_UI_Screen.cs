using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UserInt
{
public class Timed_UI_Screen : UI_Screen {

        #region MAIN_VARS
		[Header("Timed Screen Properties")]
		public float screenTime = 2.0f;

		[Header("Timed Screen Events")]
		public UnityEvent onTimeCompleted = new UnityEvent();

        #endregion

        #region HELPER_VARS
		private float startTime;
        #endregion

        #region MAIN_METHODS
		// NAME   : StartScreen
        // DESC   : Displays the screen and starts its logic
        // PARAMS : N/A
        // RETURN : N/A
		public override void StartScreen()
		{
			// Start the screen
			base.StartScreen();

			// Set the start time to the current time
			startTime = Time.time;

			// Wait for the time to run out
			StartCoroutine(WaitForTime());
		}
        #endregion

        #region HELPER_METHODS

		// NAME   : WaitForTime
        // DESC   : Halts processes on this object until the timer has run out
        // PARAMS : N/A
        // RETURN : TBD
		private IEnumerator WaitForTime()
		{
			// Wait for the time to run ou
			yield return new WaitForSeconds(screenTime);

			// Broadcast that the timer has completed
			if(onTimeCompleted != null)
			{
				onTimeCompleted.Invoke();
			}
		}
        #endregion
}
}


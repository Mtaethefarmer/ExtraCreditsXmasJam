using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text

public class GameOver : MonoBehaviour
{

    //Amount of health the player has
    private int _health = 20;

    //Text that displays the player's health
    public Text healthText;

    //Amount of time passed from 0 ~ 1 second
    private float _timePassed = 0;

    private void Update()
    {

        //Keep track of the amount of time passed
        _timePassed += Time.deltaTime;

        //Convert the player's health into a string
        healthText.text = "Game Over: " + _health;

        //Check if one second has passed
        if (Countdown())
        {
            //Decrement the player's health
            --_health;

            //Clamp health to zero
            if (_health < 0)
            {
                _health = 0;
            }

        }

        //Check if the user has pressed the spacebar key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Decrement the player's health
            --_health;

            //Clamp health to zero
            if (_health < 0)
            {
                _health = 0;
            }
        }

    }

    private bool Countdown()
    {
        //Check if one second has passed
        if (_timePassed > 1)
        {
            //Reset the time passed and return that one second has passed
            _timePassed = 0;
            return true;
        }

        //Return that one second has NOT passed
        return false;
    }
}

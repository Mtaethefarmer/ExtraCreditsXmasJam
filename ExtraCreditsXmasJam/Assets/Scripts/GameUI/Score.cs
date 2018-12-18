using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {

    public void UpdateScore(int score)
    {
        gameObject.GetComponent<Text>().text = "Score: " + score;
    }
}

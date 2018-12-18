using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour {

    public void UpdateTimer(float time)
    {
        gameObject.GetComponent<Text>().text = "Time: " + string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);
    }
}

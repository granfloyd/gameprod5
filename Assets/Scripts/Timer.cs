using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 120;
    public bool bTimer = false;
    public Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
        bTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bTimer)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTimer(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                bTimer = false;
            }
        }
    }

    void DisplayTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}

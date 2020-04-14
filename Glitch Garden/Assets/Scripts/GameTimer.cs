using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Our level timer in seconds")]
    [SerializeField] float levelTimer = 10;
    bool levelTimerUp = false;

    void Update()
    {
        if (levelTimerUp) { return; }

        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTimer;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTimer);
        if(timerFinished)
        {
            FindObjectOfType<LevelController>().TimerUp();
        }
    }
}

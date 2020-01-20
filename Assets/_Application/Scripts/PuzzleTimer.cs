using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    public TextMeshPro text;
    float theTime;
    public float speed = 1;
    public bool isPlaying = false;

    private bool isPaused = false;

    void Update()
    {
        if (isPlaying == true)
        {
            if (isPaused == false)
            {
                UpdateTimer();
            }
        }
    }

    void UpdateTimer()
    {
        theTime += Time.deltaTime * speed;
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        text.text = (minutes + ":" + seconds);
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void UnPauseTimer()
    {
        isPaused = false;
    }

    public void ResetTimer()
    {
        PauseTimer();
        theTime = 0.0f;
    }

    public void StartTimer()
    {
        isPlaying = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Timer : MonoBehaviour
{
    public int seconds;
    public int milliSeconds;
    public TextMeshPro textSeconds;
    public TextMeshPro textMilliSeconds;

    private string secondsTextPlaceholder;
    private string miliSecondsTextPlaceholder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PuzzleTimer");
        seconds = 0;
        Time.timeScale = 1;

        secondsTextPlaceholder = "0";
        miliSecondsTextPlaceholder = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure seconds placeholer is updated.
        if (seconds < 10)
            secondsTextPlaceholder = "0";
        else
            secondsTextPlaceholder = "";

        // Update TMPro text
        textSeconds.text = ("00" + "." + secondsTextPlaceholder + seconds + "." + "00");
    }

    IEnumerator PuzzleTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }
    }
}

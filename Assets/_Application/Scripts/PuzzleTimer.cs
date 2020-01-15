using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleTimer : MonoBehaviour
{
    public TextMeshPro text;
    float theTime;
    public float speed = 1;

    void Start()
    {
        //TextMeshPro text = GetComponent<TextMeshPro>;
    }

    void Update()
    {
        theTime += Time.deltaTime * speed;
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        text.text = (minutes + ":" + seconds);
    }
}

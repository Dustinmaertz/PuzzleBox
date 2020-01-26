using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public int currentPuzzleID = 0;
    public GameObject[] puzzles;
    public GameObject collectionSpawn;
    public Transform puzzleSpawn;
    public TextMeshPro puzzlePartText;
    public TextMeshPro textPuzzleComplete;
    //public TextMeshPro textPuzzleCompleteScore;
    //public TextMeshPro textPuzzleCompleteTime;
    public AudioClip audioPuzzleGrab;
    public AudioClip audioPuzzleRelease;
    public AudioClip audioPuzzleGoal;
    public AudioClip audioPuzzleComplete;

    public GameObject currentPuzzle;
    private PuzzleTimer puzzleTimer;
    private bool puzzleActive = false;
    public int puzzlePartCount;
    public int puzzlePartAtGoal = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference for puzzle timer
        puzzleTimer = GameObject.Find("PuzzleTimer").GetComponent<PuzzleTimer>();

        // Spawn puzzles
        SpawnPuzzle();
    }

    public void SpawnPuzzle()
    {
        currentPuzzle = Instantiate(puzzles[currentPuzzleID]);
        currentPuzzle.gameObject.SetActive(true);
        UpdatePuzzlePartText();
    }

    public void CleanPuzzle()
    {
        currentPuzzle.GetComponent<Puzzle>().CleanPuzzles();
    }

    public void ResetPuzzle()
    {
        CleanPuzzle();
        Destroy(currentPuzzle.gameObject);
        SpawnPuzzle();
        puzzlePartAtGoal = 0;
        UpdatePuzzlePartText();
        ClearCompleteText();
    }

    public void ChangePuzzle(int index)
    {
        currentPuzzleID = index;
        ResetPuzzle();
        UpdatePuzzleCollection();
        puzzlePartAtGoal = 0;
        UpdatePuzzlePartText();
    }

    public void UpdatePuzzleCollection()
    {
        collectionSpawn.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    public void UpdatePuzzlePartText()
    {
        puzzlePartText.text = (puzzlePartAtGoal + " / " + puzzlePartCount);
    }

    public void CheckPuzzleComplete()
    {
        if(puzzlePartCount == puzzlePartAtGoal)
        {
            textPuzzleComplete.text = "PUZZLE COMPLETE CONGRATS!!!";
            var audio = this.gameObject.GetComponent<AudioSource>();

            audio.PlayOneShot(audioPuzzleComplete);
        }
    }

    public void ClearCompleteText()
    {
        textPuzzleComplete.text = "";
        // puzzleTimer.ResetTimer();
    }

    public void UpdateCurrentPuzzleMaterials()
    {
        currentPuzzle.gameObject.GetComponent<Puzzle>().UpdatePieceMaterial();
    }

}

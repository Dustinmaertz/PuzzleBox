using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class PuzzleManager : MonoBehaviour
{
    public int currentPuzzleID = 0;
    public GameObject[] puzzles;
    public GameObject collectionSpawn;
    public Transform puzzleSpawn;

    private GameObject currentPuzzle;
    private PuzzleTimer puzzleTimer;
    private bool puzzleActive = false;

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
    }

    public void ChangePuzzle(int index)
    {
        currentPuzzleID = index;
        ResetPuzzle();
        UpdatePuzzleCollection();

    }

    public void UpdatePuzzleCollection()
    {
        collectionSpawn.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}

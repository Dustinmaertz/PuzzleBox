using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int currentPuzzleID = 0;
    public GameObject[] puzzles;
    public GameObject collectionSpawn;
    public Transform puzzleSpawn;

    private GameObject currentPuzzle;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material[] puzzleMaterials;

    private PuzzleManager puzzleManager;

    void Start()
    {
        puzzleManager = GameObject.Find("GameManagers").GetComponent<PuzzleManager>();
    }

    public void ChangeMaterial(int index)
    {
        puzzleManager.currentPuzzle.GetComponent<Puzzle>().puzzleMaterial = puzzleMaterials[index];
        puzzleManager.currentPuzzle.GetComponent<Puzzle>().UpdatePieceMaterial();
    }
}

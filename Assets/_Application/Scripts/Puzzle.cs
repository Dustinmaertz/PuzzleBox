using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;


public class Puzzle : MonoBehaviour
{
    public GameObject puzzleWhole;
    public GameObject puzzleParts;
    public Transform puzzleSpawn;
    public Transform puzzlePartsGoal;
    public Transform puzzleGoalLocation;
    public Transform collectionSpawn;
    public GridObjectCollection puzzleCollection;
    public List<Transform> puzzleGoalTransform = new List<Transform>();

    void Start()
    {
        // Collect puzzle part goal locations
        SpawnPuzzleParts();

        // Spawn Ghost version of puzzle object
        CreatePuzzleGhost();
    }

    public void SpawnPuzzleParts()
    {
        // Clone puzzle pieces for goal transforms
        foreach (Transform child in puzzleParts.transform)
        {
            // Instatiate clone of puzzle part
            Instantiate(child, collectionSpawn);

            // Remove mesh filter and renderer from puzzle goal transforms 
            var meshFilter = child.gameObject.GetComponent<MeshFilter>();
            Destroy(meshFilter);
            var meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
            Destroy(meshRenderer);
        }

        // Add manipulation handeler and mesh collider to puzzle pieces.
        foreach (Transform child in puzzleCollection.transform)
        {
            child.gameObject.AddComponent<BoxCollider>();
            child.gameObject.AddComponent<ManipulationHandler>();
            child.gameObject.AddComponent<NearInteractionGrabbable>();
            child.gameObject.AddComponent<PuzzleGoalLocation>();

            var  puzzleGoal = child.gameObject.GetComponent<PuzzleGoalLocation>();
            puzzleGoal.GetGoalLocation();

        }

        // Update collection
        puzzleCollection.UpdateCollection();
    }

    public void CreatePuzzleGhost()
    {
        Instantiate(puzzleWhole, puzzleSpawn);
        puzzleGoalLocation.position = puzzleSpawn.position;
    }
}

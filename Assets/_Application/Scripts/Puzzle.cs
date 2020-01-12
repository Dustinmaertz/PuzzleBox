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
    public Material ghostMaterial;
    public Transform puzzleSpawn;
    public Transform puzzlePartsGoal;
    public Transform puzzleGoalLocation;
    public Transform collectionSpawn;
    public GridObjectCollection puzzleCollection;
    public List<Transform> puzzleGoalTransform = new List<Transform>();
    public Vector3 colliderSize;

    public bool flipSpawnDir = false;
    public bool randomizePartDir = false;

    void Start()
    {
        // Collect puzzle part goal locations
        SpawnPuzzleParts();

        // Spawn Ghost version of puzzle object
        CreatePuzzleGhost();

        SetupPuzzleManip();
    }

    public void SpawnPuzzleParts()
    {
        // Clone puzzle pieces for goal transforms
        foreach (Transform child in puzzleParts.transform)
        {
            // Instatiate clone of puzzle part
            Instantiate(child, collectionSpawn);

            var meshRenderer = child.gameObject.GetComponent<Renderer>();
            meshRenderer.material = ghostMaterial;

            //ChangeMaterial(ghostMaterial);




        }

        // Add manipulation handeler and mesh collider to puzzle pieces.
        foreach (Transform child in puzzleCollection.transform)
        {
            child.gameObject.AddComponent<BoxCollider>();
            child.gameObject.AddComponent<ManipulationHandler>();
            child.gameObject.AddComponent<NearInteractionGrabbable>();
            child.gameObject.AddComponent<PuzzleGoalLocation>();

            var manip = child.gameObject.GetComponent<ManipulationHandler>();
            manip.ManipulationType = ManipulationHandler.HandMovementType.OneHandedOnly;


            var  puzzleGoal = child.gameObject.GetComponent<PuzzleGoalLocation>();
            puzzleGoal.GetGoalLocation();

            if(!randomizePartDir)
            {
                child.transform.rotation = Random.rotation;
            }

        }

        // Update collection
        puzzleCollection.UpdateCollection();

    }

    // Move puzzle to corret location
    public void CreatePuzzleGhost()
    {
        //Instantiate(puzzleWhole, puzzleSpawn);
        puzzleGoalLocation.position = puzzleSpawn.position;
        puzzleGoalLocation.rotation = puzzleSpawn.rotation;
    }

    // Set up Puzzle goal object manipulation
    public void SetupPuzzleManip()
    {
        // Add components
        puzzleParts.AddComponent<BoxCollider>();
        puzzleParts.AddComponent<ManipulationHandler>();
        puzzleParts.AddComponent<NearInteractionGrabbable>();

        // Configure Manipulation Handeler settings
        var manip = puzzleParts.gameObject.GetComponent<ManipulationHandler>();
        manip.ManipulationType = ManipulationHandler.HandMovementType.OneHandedOnly;
        manip.TwoHandedManipulationType = ManipulationHandler.TwoHandedManipulation.MoveRotate;

        // Get and Set collider size
        var col = puzzleParts.GetComponent<BoxCollider>();
        col.size = colliderSize;

        // Change all renderer sub materials to ghost mat.
        ChangeMaterial(ghostMaterial);

    }


    void ChangeMaterial(Material newMat)
    {
        Renderer[] children;
        children = puzzleParts.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }


}

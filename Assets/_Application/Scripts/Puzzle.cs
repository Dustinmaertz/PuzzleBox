using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;


public class Puzzle : MonoBehaviour
{
    public GameObject puzzleParts;
    public Material puzzleMaterial;
    public Material puzzleIntMaterial;
    public Material ghostMaterial;
    public Transform puzzleSpawn;
    public Transform puzzlePartsGoal;
    public Transform puzzleGoalLocation;
    public Transform collectionSpawn;
    public GridObjectCollection puzzleCollection;
    public List<Transform> puzzleGoalTransform = new List<Transform>();
    public Vector3 colliderSize;
    public GameObject WidgetPos;

    public bool flipSpawnDir = false;
    public bool randomizePartDir = false;
    public PuzzleManager puzzleManager;

    void Start()
    {
        GatherReferences();
        SpawnPuzzleParts();
        SetupPuzzleManip();
    }

    public void GatherReferences()
    {
        // Add puzzle spawn transform
        Transform puzzleSpwn = GameObject.Find("PuzzleSpawn").transform;
        puzzleSpawn = puzzleSpwn;
        // Add Puzzle grid collection
        GridObjectCollection gridColl = GameObject.Find("PuzzleCollection").GetComponent<GridObjectCollection>();
        puzzleCollection = gridColl;
        // Puzzle grid collection transform
        Transform gridColTrans = GameObject.Find("PuzzleCollection").GetComponent<Transform>();
        collectionSpawn = gridColTrans;
    }

    public void SpawnPuzzleParts()
    {
        PuzzleManager puzMng = GameObject.Find("GameManagers").GetComponent<PuzzleManager>();
        puzzleManager = puzMng;

        puzzleManager.puzzlePartCount = 0;
        // Clone puzzle pieces for goal transforms
        foreach (Transform child in puzzleParts.transform)
        {
            // Instatiate clone of puzzle part
            Instantiate(child, collectionSpawn);
            //SpawnPuzzleWidgets();
            var meshRenderer = child.gameObject.GetComponent<Renderer>();
            meshRenderer.material = ghostMaterial;
            puzzleManager.puzzlePartCount++;
        }

        UpdatePieceMaterial();


        // Add manipulation handeler and mesh collider to puzzle pieces.
        foreach (Transform child in collectionSpawn.transform)
        {
            // Add components
            child.gameObject.AddComponent<BoxCollider>();
            child.gameObject.AddComponent<ManipulationHandler>();
            child.gameObject.AddComponent<NearInteractionGrabbable>();
            child.gameObject.AddComponent<PuzzleGoalLocation>();
            child.gameObject.AddComponent<AudioSource>();

            // Configure Manipulation Handeler settings
            var manip = child.gameObject.GetComponent<ManipulationHandler>();
            manip.ManipulationType = ManipulationHandler.HandMovementType.OneHandedOnly;
            var near = child.gameObject.GetComponent<NearInteractionGrabbable>();
            near.ShowTetherWhenManipulating = true;

            //Add Manipulation handeler events to track if a part is grabbed to start goal location script
            manip.OnManipulationStarted.AddListener((ManipulationEventData) => child.GetComponent<PuzzleGoalLocation>().isGrabbed = true);
            manip.OnManipulationEnded.AddListener((ManipulationEventData) => child.GetComponent<PuzzleGoalLocation>().isGrabbed = false);
            // Add audio source to chunk
            manip.OnManipulationStarted.AddListener((ManipulationEventData) => child.GetComponent<AudioSource>().PlayOneShot(puzzleManager.audioPuzzleGrab));
            manip.OnManipulationEnded.AddListener((ManipulationEventData) => child.GetComponent<AudioSource>().PlayOneShot(puzzleManager.audioPuzzleRelease));

            // Get puzzle part goal location
            var puzzleGoal = child.gameObject.GetComponent<PuzzleGoalLocation>();
            puzzleGoal.GetGoalLocation();

            // Instatiate widget in puzzle chunk
            Instantiate(WidgetPos, child.transform);

            // Check if random dir is checked
            if (!randomizePartDir)
            {
                child.transform.rotation = Random.rotation;
            }

            if (!flipSpawnDir)
            {
                var flippedAngle = child.transform.eulerAngles + 180f * Vector3.up;
                child.transform.eulerAngles = flippedAngle;
            }
        }
        // Update collection
        puzzleCollection.UpdateCollection();

        // Move puzzle to corret location
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
        puzzleParts.AddComponent<AudioSource>();

        var sound = GetComponent<AudioSource>();

        // Configure Manipulation Handeler settings
        var manip = puzzleParts.gameObject.GetComponent<ManipulationHandler>() as ManipulationHandler;
        manip.ManipulationType = ManipulationHandler.HandMovementType.OneHandedOnly;
        var near = puzzleParts.gameObject.GetComponent<NearInteractionGrabbable>();
        near.ShowTetherWhenManipulating = true;

        // Get and Set collider size
        var col = puzzleParts.GetComponent<BoxCollider>();
        col.size = colliderSize;

        // Change all renderer sub materials to ghost mat.
        ChangeMaterial(ghostMaterial);

        // Add axis widget to puzzle pieces
        foreach (Transform child in puzzleParts.transform)
        {
            Instantiate(WidgetPos, child.transform);
        }
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

    public void ResetPuzzle()
    {
        GatherReferences();
        SpawnPuzzleParts();
        SetupPuzzleManip();
    }

    public void CleanPuzzles()
    {
        GatherReferences();
        foreach (Transform child in collectionSpawn)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdatePieceMaterial()
    {
        GatherReferences();
        foreach (Transform child in collectionSpawn.transform)
        {
            var meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
            var mats = meshRenderer.materials;
            mats[0] = puzzleMaterial;
            //mats[1] = puzzleIntMaterial;
        }
    }

    public void WidgetHide()
    {
        WidgetPos.SetActive(false);
    }
    public void WidgetShow()
    {
        WidgetPos.SetActive(true);
    }
}

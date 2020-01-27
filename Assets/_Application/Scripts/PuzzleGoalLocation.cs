using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;


public class PuzzleGoalLocation : MonoBehaviour
{
    public GameObject goalLocation;
    public float snapPosDistance = 0.05f;
    public float snapRotDistance = 20.0f;
    public float releaseTime = 1.0f;
    public bool isAtGoal = false;
    public bool isGrabbed = false;

    public PuzzleManager puzzleManager;

    public GameObject posWidget;

    void Awake()
    {
        PuzzleManager puzMng = GameObject.Find("GameManagers").GetComponent<PuzzleManager>();
        puzzleManager = puzMng;
    }

    void Update()
    {
        // Check to see if puzzle is grabbed by the user.
        if (isGrabbed == true)
        {
            // Check if puzzle is at goal location
            if (!isAtGoal)
            {
                CheckPuzzleLocatoin();
            }
        }
    }

    public void CheckPuzzleLocatoin()
    {
        GetGoalLocation();
        Vector3 targetDir = goalLocation.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.up);

        // AngleCheck Debug
        //if (angle < snapRotDistance)
            //print("close");

        // Check is piece is at goal location
        if ((transform.position - goalLocation.transform.position).sqrMagnitude <= (snapPosDistance * snapPosDistance) && angle < snapRotDistance)
        {
            SnapInPuzzlePiece();
        }
    }

    public void SnapInPuzzlePiece()
    {
        Debug.Log(this.name + " Has been placed in the correct position");

        // Snap transforms
        transform.position = goalLocation.transform.position;
        transform.rotation = goalLocation.transform.rotation;

        // Remove unneeded components
        var manip = this.gameObject.GetComponent<ManipulationHandler>();
        Destroy(manip);
        var nearint = this.gameObject.GetComponent<NearInteractionGrabbable>();
        Destroy(nearint);
        var meshFilter = goalLocation.gameObject.GetComponent<MeshFilter>();
        Destroy(meshFilter);
        var meshRenderer = goalLocation.gameObject.GetComponent<MeshRenderer>();
        Destroy(meshRenderer);

        GameObject widget = this.transform.GetChild(0).gameObject;
        Destroy(widget);

        this.transform.SetParent(goalLocation.transform);
        isAtGoal = true;

        // Play puzzle goal audio
        var audio = this.gameObject.GetComponent<AudioSource>();
        audio.PlayOneShot(puzzleManager.audioPuzzleGoal);

        puzzleManager.puzzlePartAtGoal++;
        puzzleManager.UpdatePuzzlePartText();
        puzzleManager.CheckPuzzleComplete();
    }

    public void GetGoalLocation()
    {
        var name = this.name;
        name = name.Remove(name.Length - 7);
        goalLocation = GameObject.Find(name);

        GameObject goalLoc = GameObject.Find(name);
        goalLocation = goalLoc;
        //var widget = goalLocation.gameObject.GetChild(0).gameObject;
        //posWidget = gameObject.GetComponentInChildren<trans>
    }

    public void GrabStart()
    {
        foreach (Transform child in this.transform)
        {
            posWidget = child.gameObject;
        }

        //posWidget = this.transform.GetChild(0).gameObject;
        posWidget.SetActive(false);

        puzzleManager.currentPuzzle.GetComponent<Puzzle>().WidgetHide();
        isGrabbed = true;
    }

    public void GrabEnd()
    {
        foreach (Transform child in this.transform)
        {
            posWidget = child.gameObject;
        }

        //posWidget = this.transform.GetChild(0).gameObject;
        posWidget.SetActive(true);

        puzzleManager.currentPuzzle.GetComponent<Puzzle>().WidgetShow();
        isGrabbed = false;
    }

    public void WidgetHide()
    {

    }
}

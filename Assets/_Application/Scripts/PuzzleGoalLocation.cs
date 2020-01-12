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
    public bool isAtGoal = false;

    public void GetGoalLocation()
    {
        var name = this.name;
        name = name.Remove(name.Length - 7);
        goalLocation = GameObject.Find(name);
    }

    void Update()
    {
        if (!isAtGoal)
        {
            Vector3 targetDir = goalLocation.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.up);

            if (angle < snapRotDistance)
                print("close");

            if ((transform.position - goalLocation.transform.position).sqrMagnitude <= (snapPosDistance * snapPosDistance) && angle < snapRotDistance)
            {

                    Debug.Log("Matched " + this.name + " Removing Mapitulatioin controls");

                    transform.position = goalLocation.transform.position;
                    transform.rotation = goalLocation.transform.rotation;
                    var manip = this.gameObject.GetComponent<ManipulationHandler>();
                    Destroy(manip);
                    var nearint = this.gameObject.GetComponent<NearInteractionGrabbable>();
                    Destroy(nearint);
                    isAtGoal = true;
            }
            
            
            /*
            if ((transform.position - goalLocation.transform.position).sqrMagnitude <= (snapPosDistance * snapPosDistance) && (transform.eulerAngles - goalLocation.transform.eulerAngles).sqrMagnitude <= (snapRotDistance + snapRotDistance))
            {
                Debug.Log("Matched " + this.name + " Removing Mapitulatioin controls");

                transform.position = goalLocation.transform.position;
                var manip = this.gameObject.GetComponent<ManipulationHandler>();
                Destroy(manip);
                var nearint = this.gameObject.GetComponent<NearInteractionGrabbable>();
                Destroy(nearint);
                isAtGoal = true;
            }
            */
        }
       // Debug.Log(goalLocation.name);
    }
}

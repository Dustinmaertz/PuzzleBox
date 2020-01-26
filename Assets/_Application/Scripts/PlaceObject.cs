using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class PlaceObject : MonoBehaviour
{
    public GameObject placementDisc;
    public GameObject playSpace;

    private bool discPlaced = false;
    private bool discMoving = false;

    public void SpawnOnPointer(MixedRealityPointerEventData eventData)
    {
        if (discPlaced != true)
        {
            if (placementDisc != null)
            {
                var result = eventData.Pointer.Result;
                Instantiate(placementDisc, result.Details.Point, Quaternion.LookRotation(result.Details.Normal));
                discPlaced = true;
            }
        }
    }

    public void UpdateDiscLocation(MixedRealityPointerEventData eventData)
    {
        if (discPlaced == true)
        {
            discMoving = true;
            if (discMoving == true)
            {
                var result = eventData.Pointer.Result;
                placementDisc.transform.position = result.Details.Point;
                placementDisc.transform.rotation = Quaternion.LookRotation(result.Details.Normal);
            }
        }
    }
}

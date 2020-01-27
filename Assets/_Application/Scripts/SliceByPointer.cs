using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class SliceByPointer : MonoBehaviour
{
    public Angle angle;


    public void SliceOnPoint(MixedRealityPointerEventData eventData)
    {
        var result = eventData.Pointer.Result;
        var hit = result.Details.Point;

        GameObject victim = result.Details.Object;
        if (victim.tag != "Safe")
        {

            if (angle == Angle.Up)
            {
                Cutter.Cut(victim, hit, Vector3.up, null, true, false, true, true);
            }
            else if (angle == Angle.Forward)
            {
                Cutter.Cut(victim, hit, Vector3.forward, null, true, false, true, true);
            }
        }
    }
}

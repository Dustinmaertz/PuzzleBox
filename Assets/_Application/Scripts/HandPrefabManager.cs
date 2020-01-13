using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class HandPrefabManager : MonoBehaviour
{
    public int currentHandMaterialID = 0;
    public Material[] handMaterial;
    public GameObject HandMeshPrefab;

    public void ChangeHandPrefab(int index)
    {
        var rend = HandMeshPrefab.gameObject.GetComponentsInChildren<Renderer>();
        rend[0].material = handMaterial[index];

        // Second idea
        //var rend1 = GameObject.Find("HandMesh.Triangles").gameObject.GetComponentsInChildren<Renderer>();
        //rend1[0].material = handMaterial[index];


        currentHandMaterialID = index;
    }
}

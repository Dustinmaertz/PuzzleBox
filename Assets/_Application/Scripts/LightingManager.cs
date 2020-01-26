using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public Light dirLight;
    public GameObject dirLightPrefab;
    public Transform dirLightSpawn;
    public ReflectionProbe cubemap;
    public GameObject hdriSpherePrefab;

    [SerializeField]
    private Cubemap[] hdriTextures;
    [SerializeField]
    private Material[] hdriMaterials;
    private Color dirLightColor;
    private float dirLightIntensity = 1.0f;

    void Start()
    {
        // Gather HDRI textures from Resources folder
        hdriTextures = Resources.LoadAll<Cubemap>("Textures/HDRI");
        hdriMaterials = Resources.LoadAll<Material>("Materials/HDRI");
    }

    void SetupSceneLighting()
    {
        Instantiate(dirLight, dirLightSpawn);
    }

    void SetDirLightInitTransform()
    {
        //dirLightPrefab.transform = 
    }

    public void ChangeCubemap(int cubemapID)
    {
        cubemap.customBakedTexture = hdriTextures[cubemapID];
    }


}

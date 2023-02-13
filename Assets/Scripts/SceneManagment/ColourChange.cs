using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    public Light directionalLight;

    void Start()
    {
        // Change the color of all materials in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = directionalLight.color;
            }
        }

        // Change the color of all terrain layers in the scene
        Terrain terrain = FindObjectOfType<Terrain>();
        TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
        int numLayers = terrainLayers.Length;

        for (int i = 0; i < numLayers; i++)
        {
            TerrainLayer layer = terrainLayers[i];
            layer.diffuseRemapMax = directionalLight.color;
        }

        // Change the color of all tree instances in the scene
        Color treeColor = directionalLight.color;
        TreeInstance[] treeInstances = terrain.terrainData.treeInstances;
        int numTrees = treeInstances.Length;

        for (int i = 0; i < numTrees; i++)
        {
            TreeInstance tree = treeInstances[i];
            tree.color = treeColor;
            terrain.terrainData.SetTreeInstance(i, tree);
        }

        // Change the color of all detail prototypes in the scene
        Color detailColor = directionalLight.color;
        DetailPrototype[] detailPrototypes = terrain.terrainData.detailPrototypes;
        int numDetailPrototypes = detailPrototypes.Length;

        for (int i = 0; i < numDetailPrototypes; i++)
        {
            DetailPrototype detailPrototype = detailPrototypes[i];
            Texture2D prototypeTexture = detailPrototype.prototypeTexture;

            if (prototypeTexture != null)
            {
                // Create a new texture with the desired color
                int width = prototypeTexture.width;
                int height = prototypeTexture.height;
                Color[] pixels = new Color[width * height];
                for (int j = 0; j < pixels.Length; j++)
                {
                    pixels[j] = detailColor;
                }

                Texture2D newTexture = new Texture2D(width, height);
                newTexture.SetPixels(pixels);
                newTexture.Apply();

                // Assign the new texture to the prototype
                detailPrototype.prototypeTexture = newTexture;
                terrain.terrainData.detailPrototypes[i] = detailPrototype;
            }
        }

        // Change the color of all prefab materials in the scene
        MeshRenderer[] prefabRenderers = FindObjectsOfType<MeshRenderer>();
        foreach (MeshRenderer prefabRenderer in prefabRenderers)
        {
            Material[] prefabMaterials = prefabRenderer.materials;
            for (int i = 0; i < prefabMaterials.Length; i++)
            {
                prefabMaterials[i].color = directionalLight.color;
            }
        }
    }
}



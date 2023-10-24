using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMapMode {NoiseMap, MatMap, Mesh};
    public DrawMapMode drawMapMode;

    const int mapChunkSize = 241;
    public int octaves;
    public int seed;

    public UnityEngine.Vector2 offset;

    public float noiseScale;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public float meshHeightMultiplier;

    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;


    public void GenerateMap()
    {

        float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, octaves, noiseScale, persistance, lacunarity, offset);

        Color[] matMap = new Color[mapChunkSize * mapChunkSize];

        for (int y =0; y < mapChunkSize; y++)
        {

            for (int x =0; x < mapChunkSize; x++)
            {

                float currentHeight = noiseMap[x,y];

                for (int i = 0; i < regions.Length; i++)
                {

                    if (currentHeight <= regions [i].tHeight)
                    {

                        matMap[y * mapChunkSize + x] = regions [i].tMaterial;

                        break;

                    }

                }

            }

        }

        MapDisplay display = FindObjectOfType<MapDisplay> ();
        if (drawMapMode == DrawMapMode.NoiseMap)
        {

        display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));

        }
        else if (drawMapMode == DrawMapMode.MatMap)
        {

            display.DrawTexture (TextureGenerator.TextureFromColorMap(matMap, mapChunkSize, mapChunkSize));


        }
        else if(drawMapMode == DrawMapMode.Mesh)
        {

            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(matMap, mapChunkSize, mapChunkSize));

        }

    }

    // making sure values that break below a certain value dont go below that value
     void OnValidate() 
    {
        
        if (mapChunkSize < 1)
        {

            mapChunkSize = 1;

        }
        if (mapChunkSize < 1)
        {

            mapChunkSize = 1;

        }
        if (lacunarity < 1)
        {

            lacunarity = 1;

        }
        if (octaves < 0)
        {

            octaves = 0;

        }

    }

}

// make different types of terrain in unity inspector
 [System.Serializable]
public struct TerrainType
{

    public string tName;
    public float tHeight;
    public Color tMaterial;

}
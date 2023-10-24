using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Noise 
{

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, int octaves, float scale, float persistance, float lacunarity, Vector2 offset)
    {

        // Faux Random Number Generator
        System.Random frng = new System.Random (seed);

        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {

            float offsetX = frng.Next(-100000, 100000) + offset.x;
            float offsetY = frng.Next(-100000, 100000) + offset.y;

            octaveOffsets [i] = new Vector2 (offsetX, offsetY);

        }

        float[,] noiseMap = new float[mapWidth,mapHeight];
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        if (scale <= 0)
        {

            scale = 0.0001f;

        }

        for (int y = 0; y < mapHeight; y++)
        {

            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequencey = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {

                float sampleX = ((x - halfWidth) / scale - octaveOffsets[i].x) * frequencey;
                float sampleY = ((y - halfHeight) / scale - octaveOffsets[i].y) * frequencey;

                float perlinVal = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;

                noiseHeight += perlinVal * amplitude;

                amplitude *= persistance;
                frequencey *= lacunarity;

                }

                if (noiseHeight > maxNoiseHeight)
                {

                    maxNoiseHeight = noiseHeight;

                }
                else if (noiseHeight < minNoiseHeight)
                {

                    minNoiseHeight = noiseHeight;

                }

                noiseMap [x, y] = noiseHeight; 

            }

        }

         for (int y = 0; y < mapHeight; y++)
        {

            for (int x = 0; x < mapWidth; x++)
            {

                noiseMap[x,y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);

            }

        }

        return noiseMap;

    }

}

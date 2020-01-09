using UnityEngine;

public static class Noise
{
   
    public static float [,] CreateNoiseMap(int xSize, int zSize, float scale)
    {
        float[,] noiseMap = new float [xSize, zSize];

        if(scale <= 0)
            scale = 0.0001f;

        for (int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                float xScaled = xSize / scale;
                float zScaled = zSize / scale;

                noiseMap[x,z] = Mathf.PerlinNoise(xScaled, zScaled);
            }
        }
        return noiseMap;
    }


}

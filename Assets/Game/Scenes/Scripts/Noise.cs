using UnityEngine;

public static class Noise
{
   
    public static float [,] GenerateNoise(int width, int length, float scale)
    {
        float[,] noisemap = new float [width, length];

        if(scale <= 0)
            scale = 0.0001f;

        for (int x = 0; x < width; x++)
        {
            for(int z = 0; z < length; z++)
            {
                float scaledx = x / scale;
                float scaledz = x / scale;

                float noisevalue = Mathf.PerlinNoise(scaledx, scaledz);
                noisemap[x,z] = noisevalue;

            }
        }
        return noisemap;
    }


}

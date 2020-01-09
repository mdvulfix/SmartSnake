using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartSnake
{
    
    public class CustomMesh
    {

        string name;
        
        int xSize;
        int zSize;
    
        float scale;

        public CustomMesh(string name = "Custom Mesh", int xSize = 1, int zSize = 1, float scale = 1)
        {

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            this.name = name;
            this.xSize = xSize;
            this.zSize = zSize;
            this.scale = scale;
        }


        public Mesh CreateMesh()
        {

            Mesh mesh = new Mesh();
            mesh.name = name;
            
            List<Vector3> vList = new List<Vector3>();
            for(int z = 0; z < zSize + 1; z++ )
                for(int x = 0; x < xSize + 1; x++)
                    vList.Add(new Vector3(x, 0, z));

            mesh.vertices = vList.ToArray();

            
            List<Triangle> triangles = new List<Triangle>();
            for(int z = 0; z < zSize; z++)
            {
                for(int x = 0; x < xSize; x++)
                {
                    triangles.Add(new Triangle((z + 0) * (xSize + 1) + x, (z + 1) * (xSize + 1) + x, (z + 1) * (xSize + 1) + x + 1));
                    triangles.Add(new Triangle((z + 0) * (xSize + 1) + x, (z + 1) * (xSize + 1) + x + 1, (z + 0) * (xSize + 1) + x + 1));
                }
            }
            
            List<int> tList = new List<int>();
            for(int i = 0; i < triangles.Count; i++)
            {
                tList.Add(triangles[i].v1);
                tList.Add(triangles[i].v2);
                tList.Add(triangles[i].v3);
            }
            
            mesh.triangles = tList.ToArray();
            
            List<Vector2> uvList = new List<Vector2>();
            for(int y = 0; y < zSize + 1; y++ )
                for(int x = 0; x < xSize + 1; x++)
                    uvList.Add(new Vector2((float)x / xSize, (float)y/zSize));

            mesh.uv = uvList.ToArray();

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            //mesh.RecalculateTangents();    

        
            return mesh;

        }
    }
}

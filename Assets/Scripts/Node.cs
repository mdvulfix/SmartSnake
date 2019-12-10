using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SmartSnake
{

    public class Node
    {

        public int x {get; private set;}
        public int y {get; private set;}


        GameObject obj;


        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }



        public void SetObject(GameObject obj)
        {
            this.obj = obj;
            this.obj.transform.position = new Vector3(x, y, 0f);
        }
        
        public GameObject GetObject()
        {
            return obj;
        }

        void RemoveObject()
        {
            this.obj = null;

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    

    public class Node2
    {
        private Vector2 position;
        //private GameObject obj;

       
       public Node2 (Vector2 position)
        {
            SetNodePosition(position);
        }
        /*
        public Node (Vector2 position, GameObject obj)
        {
            SetObject(obj);
            SetPositionOnMap(position);
        }


        public void SetObject(GameObject obj)
        {
            this.obj = obj;
            this.obj.transform.position = new Vector3 (position.x, position.y, 0f);
        }
        */

        
        
        public void SetNodePosition(Vector2 position)
        { 
            this.position = position;

            //Vector3 positionInWorld = obj.transform.position;
            //this.obj.transform.position = new Vector3 (position.x, position.y, positionInWorld.z);
        }

        public Vector2 GetNodePosition()
        {
            return position;
        }
        
        /*
        public GameObject GetObject()
        {
           return obj;
        }
        */





    }

}
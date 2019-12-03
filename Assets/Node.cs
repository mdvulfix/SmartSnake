using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    

    public class Node
    {
        private Vector2 position = Vector2.zero;
        private GameObject obj;

       
       public Node (Vector2 position)
        {
            SetPosition(position);
        }

        public Node (Vector2 position, GameObject obj)
        {
            SetObject(obj);
            SetPosition(position);
        }

        public void SetObject(GameObject obj)
        {
            this.obj = obj;
            this.obj.transform.position = new Vector3 (position.x, position.y, 0f);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
            this.obj.transform.position = new Vector3 (position.x, position.y, 0f);
            
         
        }

        public GameObject GetObject()
        {
           return obj;
        }

        public Vector2 GetPosition()
        {
            return position;
        }


        public bool ThisNodeHaveObject()
        {
            bool haveObj = false;
            if (obj) {
                haveObj = true;
            }
            else {
                haveObj = false;
            }
            return haveObj;
        }
    }
}

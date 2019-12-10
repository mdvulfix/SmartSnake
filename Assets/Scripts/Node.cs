using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SmartSnake
{

    public class Node
    {

        public float x {get; private set;}
        public float y {get; private set;}

        GameObject obj;


        public Node(Vector2 position)
        {
            SetPosition(position);
            
        }

        public void SetPosition(Vector2 position)
        {
            x = position.x;
            y = position.y;
        } 

        public Vector2 GetPosition()
        {
            return new Vector2 (x, y);

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
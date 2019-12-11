using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SmartSnake
{

    public class Node
    {

        private Position3D position;

        GameObject obj;


        public Node(Position3D aPosition)
        {
            SetPosition(aPosition);
            
        }

        public void SetPosition(Position3D aPosition)
        {
            this.position = aPosition;
        } 

        public Position3D GetPosition()
        {
            return this.position;

        }    
            

        public void SetObject(GameObject obj)
        {
            this.obj = obj;
            this.obj.transform.position = position.ToVector3();
        }
        
        public GameObject GetObject()
        {
            return this.obj;
        }

        void RemoveObject()
        {
            this.obj = null;

        }
    }
}
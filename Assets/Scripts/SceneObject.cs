using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    
    public class SceneObject
    {

        Position position;
        GameObject obj;

        public SceneObject(GameObject obj, Position position)
        {   
            SetObject(obj);
            SetPosition(position);
        }

        public virtual void SetPosition(Position position)
        {
            this.position = position;
        } 

        public Position GetPosition()
        {
            return position;
        } 

        public void SetObject(GameObject obj, string parent = "Scene")
        { 
            this.obj = obj;
            this.obj.transform.SetParent(GameObject.Find(parent).transform);

        }

        public GameObject GetObject()
        {
            return obj;
        }
    }
}

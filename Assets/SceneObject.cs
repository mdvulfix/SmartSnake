using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneObject
    {
        private Node node;
        private GameObject obj;

        
        public SceneObject(string name, GameObject obj, Vector2 position)
        {   
            SetObject(name, obj);
            SetPositionOnMap(position);

        }


        public void SetObject(string name, GameObject obj)
        { 
            this.obj = obj;
            this.obj.name = name;

        }

        public void SetPositionOnMap(Vector2 position)
        { 
           node = CreateNode(position);
        }



        private Node CreateNode(Vector2 position)
        { 
            Node node = new Node(position, obj);
            return node;
        }

   

        public Node GetNode()
        {
            return node;
        }
        

        public GameObject GetObject()
        {
            return obj;
        }
        


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneObject
    {
        protected Node node;
        protected GameObject obj;

        public SceneObject()
        {   


        }

        public SceneObject(string name, GameObject obj)
        {   
            SetObject(name, obj);
            SetPositionInWorld(Vector3.zero);

        }

        public SceneObject(string name, GameObject obj, Node node)
        {   
            SetObject(name, obj);
            SetNode(node);

        }

        public void SetObject(string name, GameObject obj)
        { 
            this.obj = obj;
            this.obj.name = name;

        }

        public GameObject GetObject()
        {
            return obj;
        }


  
        public void SetNode(Node node)
        {
            this.node = node;
            Vector3 newPositionInWorld = new Vector3 (node.GetNodePosition().x, node.GetNodePosition().y, GetPositionInWorld().z);
            SetPositionInWorld(newPositionInWorld);       
        }

        public Node GetNode()
        {
            return node;
        }
        
        public void SetPositionInWorld(Vector3 position)
        { 
           
           this.obj.transform.position = position;

        }

        public Vector3 GetPositionInWorld()
        { 
           
           return this.obj.transform.position;

        }
        


        


    }







}

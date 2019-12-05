using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneObject
    {
        private Node node;
        private GameObject obj;
        private SceneObject child;



        public SceneObject(string name, GameObject obj)
        {   
            SetObject(name, obj);
            SetPositionInWorld(obj, Vector3.zero);

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
            Vector3 newPositionInWorld = new Vector3 (node.GetNodePosition().x, node.GetNodePosition().y, GetPositionInWorld(obj).z);
            SetPositionInWorld(obj, newPositionInWorld);
        }

        public void SetChildNode(Node node)
        {
            this.child.SetNode(node);
            Vector3 newPositionInWorld = new Vector3 (node.GetNodePosition().x, node.GetNodePosition().y, GetPositionInWorld(this.child.GetObject()).z);
            SetPositionInWorld(this.child.GetObject(), newPositionInWorld);
        }

        public void GetParentNodeAndSetChildNode(Node parent)
        {
            Node child = this.node;
            this.node = parent;
            SetNode(parent);
            SetChildNode(child);
            
        }

        public void SetChild(SceneObject child)
        {
            this.child = child;
    
        }

        public Node GetNode()
        {
            return node;
        }
        
        public void SetPositionInWorld(GameObject obj, Vector3 position)
        { 
           
           obj.transform.position = position;

        }

        public Vector3 GetPositionInWorld(GameObject obj)
        { 
           
           return obj.transform.position;

        }
        


        


    }







}

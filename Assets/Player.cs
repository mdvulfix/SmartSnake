using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    
    static class Player
    {

        static Position3D position;
        static GameObject camera;
        
        

        public static void SetPosition(Position3D aPosition)
        {
            position = aPosition;
            SetCamera(camera);
        } 

        public static void SetCamera(GameObject aCamera)
        {
            camera = aCamera;
            camera.transform.position = new Vector3 (position.ToVector3().x, position.ToVector3().y, camera.transform.position.z);

        } 

        public static Position3D GetPosition()
        {
            return position;
        }  




        /*
        public static Node GetNode()
        {
            return Player.node;
        }
        */
        /*
        public SceneObject(Node node)
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
        


        
        */

    }







}

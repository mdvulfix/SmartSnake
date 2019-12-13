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

        public void SetPosition(Position position)
        {
            this.position = position;
            this.obj.transform.position = position.ToVector3();
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

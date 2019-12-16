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

        public void SetObject(GameObject obj)
        { 
            this.obj = obj;
        }

        public GameObject GetObject()
        {
            return obj;
        }
    }
}

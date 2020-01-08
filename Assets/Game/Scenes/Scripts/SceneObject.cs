using UnityEngine;


namespace SmartSnake
{
    public struct Position
    {
        public int x;
        public int y;
        public int z;

        public Position (int x, int y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;

        }       
        public Position (int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }
            
        public Vector3 ToVector3()
        {
            return new Vector3 ((float)x, (float)y, (float)z);

        }

        public static Position operator + (Position a, Position b) 
        {  
            return new Position 
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z
            };
        }  

        public static Position operator - (Position a, Position b) 
        {  
            return new Position 
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z
            };
        }


        public override bool Equals(System.Object obj)
        {
            if (! (obj is Position)) return false;
        
            Position position = (Position) obj;
            return x == position.x && y == position.y && z == position.z;
        }
    
        public override int GetHashCode()
        { 
            return (x ^ y) ^ z;
        } 


        public static bool operator == (Position lhs, Position rhs) 
        {             
            return lhs.Equals(rhs);
        }

        public static bool operator != (Position lhs, Position rhs) 
        {             
            return !lhs.Equals(rhs);
        } 
    } 
    
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

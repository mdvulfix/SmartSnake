using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartSnake
{

    public class Snake: SceneObject
    {
        private Snake parent;

        public Snake(string name, GameObject obj)
        {   
            SetObject(name, obj);
            SetPositionInWorld(Vector3.zero);

        }





        public void SetParent(Snake parent, Node node)
        {

            this.parent = parent;
            this.node = node;

        }


    }
}

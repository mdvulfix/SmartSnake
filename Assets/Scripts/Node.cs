using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace SmartSnake
{

    public class Node
    {

        int x;
        int y;
        bool active;

        GameObject obj;


        public Node(int x, int y)
        {
            SetPosition(x, y);
            SetActive(false);
        }

        void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        void SetActive(bool active)
        {
            this.active = active;
        }

        public void SetObject(GameObject obj)
        {
            this.obj = obj;
            this.obj.transform.position = new Vector3(x, y, 0f);
            SetActive(true);
        }
        
        public GameObject GetObject()
        {
            return obj;
        }

        void RemoveObject()
        {
            this.obj = null;
            SetActive(false);

        }
    }
}
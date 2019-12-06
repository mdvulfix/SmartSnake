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


        public Node(int x, int y)
        {
            SetPosition(x, y);
            SetActive(false);
        }

        public Node(int x, int y, bool active)
        {
            SetPosition(x, y);
            SetActive(active);
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

        
    }
}
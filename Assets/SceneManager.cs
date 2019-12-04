using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneManager : MonoBehaviour
    {
  

        public SceneObject map;        
        public SceneObject player;
        public SceneObject[] apples;

        public Node [,] nodes;
        public List<Node> availableNodes;

        private int mapWidth = 20;
        private int mapHeight= 20;

        public GameObject       cameraObj;
     

        private List<GameObject> objectsOnMap;
        

        #region START 
        /////////////      

        // Запускаем игру:
        // 1. Создаем карту;
        // 2. Создаем играка;
        private void Start() 
        {
            
            
            

            CreateMap(mapWidth, mapHeight);
            CreateApple(15);
            CreatePlayer();
            StartCoroutine(Routine(0.035f));   
        }


        // Создаем карту:
        // 1. Создаем спрайт карты;
        // 2. Создаем сетку на карте;

        private void CreateMap(int width, int height)
        {
            Debug.Log("Создаем карту...");
            CreateGridOfNodes(nodes, mapWidth, mapHeight);
            map = CreateObjectOnScene("Map", width, height, Color.white, 0);
                        
        }

        public void CreatePlayer()
        {
            Debug.Log("Создаем игрока...");
            player = CreateObjectOnScene("Player", 1, 1, Color.black, 2);
            player.SetNode(FindAvailablePosition());
            SetNodeAvailability(player.GetNode(), false);

        }


        private void CreateApple(int amount)
        {
            Debug.Log("Создаем яблоки...");
            apples = new SceneObject[amount];
            
            for (int i = 0; i < amount; i++)
            {               
                apples[i] = CreateObjectOnScene("Apple" + i, 1, 1, Color.green, 1);
                apples[i].SetNode(FindAvailablePosition());
                SetNodeAvailability(apples[i].GetNode(), false);
                
            }
        }


        

        #endregion
        #region UPDARTE 
        ///////////////

        // Обрабатываем события нажатия кнопок
        // 1. Определяем направление движения по нажатой кнопке
        // 2. Двигаем игрока по направлению движения
        
        private IEnumerator Routine (float updateTime) 
        {
            WaitForSeconds time = new WaitForSeconds (updateTime);
            while(true)
            {
                yield return time;
                MovePlayer((int)GetDirection());
            }
        }

        enum Direction {up, down, left, right, noDirection}
        
        private Direction GetDirection()
        {
            Direction direction = Direction.noDirection;

            if(Input.GetButton("Up")){
                direction = Direction.up;
            } 

            if(Input.GetButton("Down")){
                direction =  Direction.down;                   
            }  

            if(Input.GetButton("Left")){
                direction = Direction.left;
            }

            if(Input.GetButton("Right")){
                direction = Direction.right;
            }
        
            if(Input.GetButtonUp("Up") || Input.GetButtonUp("Down") || Input.GetButtonUp("Left") || Input.GetButtonUp("Right")){
                direction = Direction.noDirection;
            }
            return direction;
        }

        private void MovePlayer(int direction)
        {
            
            
            int x = 0;
            int y = 0;
            switch (direction)
            {   
                case 0: y = 1; break;
                case 1: y = -1; break;
                case 2: x = -1; break;
                case 3: x = 1; break;
                case 4: x = 0; y = 0; break;
            }
            
            
            
            Vector2 position = player.GetNode().GetNodePosition();
            x = (int) position.x + x; 
            y = (int) position.y + y;
            if (!(y > mapWidth -1 || y < 0 || x < 0 || x > mapHeight -1)){
                if (!CheckNodeAvailability (nodes[x,y]))
                {
                    SetNodeAvailability(player.GetNode(), true);
                    player.SetNode(nodes[x,y]);
                    SetNodeAvailability(player.GetNode(), false);
                    cameraObj.transform.position = new Vector3 (x, y,-15f);
                }
            }
        }
        #endregion
        #region UTILITY 
        ///////////////

        //Создаем объекты на сцене
        private SceneObject CreateObjectOnScene(string name, int width, int height, Color color, int layer)
        {
            GameObject obj = new GameObject(name);
            SceneObject sObj = new SceneObject(name, obj);

            SpriteRenderer objRndr = obj.AddComponent<SpriteRenderer>();
            objRndr.sprite = CreateSprite(width, height, color);
            objRndr.sortingOrder = layer;
            
            return sObj;
        }

        //Создаем спрайт карты
        private Sprite CreateSprite(int width, int height, Color txtrColor) 
        {   
            
            Texture2D txtr2D = new Texture2D (width, height);
            txtr2D.filterMode = FilterMode.Bilinear;
            
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    txtr2D.SetPixel(x, y, txtrColor);
                }
            }
            txtr2D.Apply();
            
            Rect rect = new Rect(0,0,width, height);
            return Sprite.Create(txtr2D,rect, Vector2.zero, 1,0, SpriteMeshType.FullRect);
        }
        
        private Node FindAvailablePosition()
        {
            int x;
            int y;
            
            bool isAvailable = false;
            do{
                x = Random.Range(0, mapWidth);
                y = Random.Range(0, mapHeight);

                if (availableNodes.Contains(nodes[x, y]))
                {
                    isAvailable = true;
                }

            } while (isAvailable);
            
            return nodes[x, y];
        }

        // Создаем сетку на карте
        
        private void CreateGridOfNodes(Node[,] nodes, int width, int height) 
        {         
            Debug.Log("Создаем сетку карты...");  
            availableNodes = new List<Node>();
            nodes  = new Node [width, height];
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Node node = new Node(new Vector2(x, y));
                    nodes[x,y] = node;
                    SetNodeAvailability(node, true);
                }
            }
            Debug.Log("Создано ячеек: " + availableNodes.Count);


        }

        public void SetNodeAvailability(Node node, bool trueOrFalse)
        {
            if(trueOrFalse){
                availableNodes.Add(node);
            }
            else{
                availableNodes.Remove(node);
            }
        }

        public bool CheckNodeAvailability(Node node)
        {
            bool isAvailable = false;
            if (availableNodes.Contains(node))
            {
                isAvailable = true;
            }
            return isAvailable;

        }

        #endregion

    }
}

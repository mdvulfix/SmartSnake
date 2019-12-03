using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneManager : MonoBehaviour
    {
  

        public SceneObject map;        
        public SceneObject player;

        private Node [,] grid;
        private List<Node> AvailableNodes;

        private int mapWidth = 20;
        private int mapHeight= 20;

        public GameObject       cameraObj;
     

        private List<GameObject> objectsOnMap;
        

        #region START /////////////////////////////       

        // Запускаем игру:
        // 1. Создаем карту;
        // 2. Создаем играка;
        private void Awake() 
        {
            
            //objectsOnMap = new List<GameObject>();
            CreateMap(mapWidth, mapHeight);
            CreateApple(5);
            CreatePlayer();
            StartCoroutine(Routine(0.035f));   
        }


        // Создаем карту:
        // 1. Создаем спрайт карты;
        // 2. Создаем сетку на карте;

        private void CreateMap(int width, int height)
        {

            grid = CreateGrid(width, height);
            map = CreateObjectOnScene("Map", new GameObject(), width, height, Color.white, 0, Vector2.zero);
            
        }


        private void CreateApple(int amount)
        {
            for (int i = 0; i < amount; i++)
            {               
                SceneObject apple = CreateObjectOnScene("Apple" + i,new GameObject(), 1, 1, Color.green, 1, FindAvailablePosition());
            }
        }

        public void CreatePlayer()
        {
            player = CreateObjectOnScene("Player", new GameObject(), 1, 1, Color.black, 1, FindAvailablePosition());
        }
        

        #endregion
    
        #region UPDARTE ///////////////////////////

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
            
            
            
            Vector2 position = player.GetNode().GetPosition();
            x = (int) position.x + x; 
            y = (int) position.y + y;
            if (!(y > mapWidth -1 || y < 0 || x < 0 || x > mapHeight -1)){
                Vector2 newPosition = new Vector2 (x, y);
                SetNodeAvailability(AvailableNodes, player.GetNode(), true);
                player.GetNode().SetPosition(newPosition);
                SetNodeAvailability(AvailableNodes, player.GetNode(), false);
                cameraObj.transform.position = new Vector3 (newPosition.x, newPosition.y,-15f);
            }
        }
        #endregion

        #region UTILITY ///////////////////////////

        //Создаем объекты на сцене
        private SceneObject CreateObjectOnScene(string name, GameObject obj, int width, int height, Color color, int layer, Vector2 position)
        {
            
            SceneObject sObj = new SceneObject(name, obj, position);

            SpriteRenderer objRndr = obj.AddComponent<SpriteRenderer>();
            objRndr.sprite = CreateSprite(width, height, color);
            objRndr.sortingOrder = layer;
            
            SetNodeAvailability(AvailableNodes, sObj.GetNode(), false);
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
        
        private Vector2 FindAvailablePosition()
        {
            Vector2 position;
            
            bool isAvailable = false;
            do{
                position = new Vector2()
                {  
                    x = Random.Range(0, mapWidth),
                    y = Random.Range(0, mapHeight)
                };

                Node node = new Node (position);
                if (AvailableNodes.Contains(node))
                {
                    isAvailable = true;
                }

            } while (isAvailable);
            
            return position;
        }

        // Создаем сетку на карте
        private Node[,] CreateGrid(int width, int height) 
        {           
            Node[,] grid  = new Node [width, height];
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Node node = new Node(new Vector2(x, y));
                    grid[x,y] = node;
                    //SetNodeAvailability(AvailableNodes, node, true);
                }
            }
            return grid;
        }

        public void SetNodeAvailability(List<Node> nodes, Node node, bool trueOrFalse)
        {
            if(trueOrFalse){
                nodes.Add(node);
            }
            else{
                nodes.Remove(node);
            }
        }

        #endregion


    }
}

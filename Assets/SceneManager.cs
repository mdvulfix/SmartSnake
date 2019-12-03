using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public class SceneManager : MonoBehaviour
    {
  

        public SceneObject map;        
        public SceneObject player;




        
        Node mapNode, appleNode, playerNode;
            
        Node [,]    mapGrid;
        private int mapWidth = 20;
        private int mapHeight= 20;

        public GameObject       cameraObj;
        //, appleObj, ;
        private SpriteRenderer  mapRndr, appleRndr, playerRndr;

        private List<GameObject> objectsOnMap;
        private List<Node> emptyNodes;

        #region START /////////////////////////////       

        // Запускаем игру:
        // 1. Создаем карту;
        // 2. Создаем играка;
        private void Awake() 
        {
            
            //objectsOnMap = new List<GameObject>();
            CreateMap(mapWidth, mapHeight);
            //CreateApple(5);
            CreatePlayer();
            StartCoroutine(Routine(0.035f));   
        }


        // Создаем карту:
        // 1. Создаем спрайт карты;
        // 2. Создаем сетку на карте;

        private void CreateMap(int width, int height)
        {
            map = CreateObjectOnScene("Map", new GameObject(), width, height, Color.white, 0, Vector2.zero);
            
            //mapObj = CreateObjectOnScene(mapObj, "Map", width, height, Color.white, 0);
            //mapGrid = CreateGrid(width, height);
        }

/* 
        private void CreateApple(int amount)
        {
            while(amount > 0)
            {
                
                
                GameObject newAppleObj = CreateObjectOnScene(new GameObject(), "Apple", 1, 1, Color.green, 1, new Vector2(Random.Range(0, mapWidth), Random.Range(0, mapHeight)));
                //appleNode = mapGrid[5, 5];
                Node newAppleNode = new Node(){x = Random.Range(-mapWidth, mapWidth), y= Random.Range(-mapHeight, mapHeight)};
                newAppleNode = SetPositionOnMap(newAppleNode, newAppleNode.x, newAppleNode.y);
                newAppleObj.transform.position = newAppleNode.GetPositionInWorld();
                amount--;
            }
        }
*/
        public void CreatePlayer()
        {
            player = CreateObjectOnScene("Player", new GameObject(), 1, 1, Color.black, 1, Vector2.zero);
            //playerObj.transform.position = playerNode.GetPositionInWorld();
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
                player.GetNode().SetPosition(newPosition);
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
            
            //objectsOnMap.Add(obj);
            //AddObjectToNode(obj, position); 

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
        /*
        // Создаем сетку на карте
        private Node[,] CreateGrid(int width, int height) 
        {           
            Node[,] grid  = new Node [width, height];
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Node node = new Node();
                    node.SetPositionOnMap(new Vector2(x, y));
                    grid[x,y] = node;
                    emptyNodes.Add(node);
                }
            }
            return grid;
        }
        */
/*
        // Добавляем объект к клетке
        private void AddObjectToNode(GameObject obj, Vector2 position)
        {
            Node node = new Node();
            node.SetPositionOnMap(position);
            node.SetObject(obj);
            emptyNodes.Remove(node);
            mapGrid[(int)position.x, (int)position.y] = node;
        }

*/
        #endregion


    }
}

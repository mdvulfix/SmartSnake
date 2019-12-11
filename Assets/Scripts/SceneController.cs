using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SmartSnake
{
    
    public struct Position3D
    {
        public int x;
        public int y;
        public int z;

        
        
        public Position3D (int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }
              
        public Vector3 ToVector3()
        {
            return new Vector3 ((float)x, (float)y, (float)z);

        }
    }
   
    public class SceneController : MonoBehaviour
    {
  
        [SerializeField]
        private int width, height;
        
        public GameObject map;
        public GameObject maincamera;
        
        GameObject snake;
        
        GameObject[] apples;

        Node[,] nodes;
        List<Node> availablenodes;

        enum Direction {up, down, left, right, noDirection}

    #region start
        
        private void Awake() 
        {
            
        }

        private void Start() 
        {
            CreateMap(width, height);
            
            Player.SetCamera(maincamera);
            Player.SetPosition(GetAvailableNode().GetPosition());
            
            
            CreateSnake();
            CreateApple(5);

        }

        void CreateMap(int width, int height)
        {
            // Создаем карту;
            CreateSprite(map, width, height, Color.white, 0);
            
            nodes = new Node [width, height];
            availablenodes = new List<Node>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodes [x,y]  = new Node(new Position3D(x, y, 0));
                    SetNodeAvailability(nodes [x,y].GetPosition(), true);
                }
            }   

                    
        }

        void CreateSnake()
        {
            // Создаем змейку;
            snake = CreateObject("Snake");
            CreateSprite(snake, 1, 1, Color.black, 2);
            SetObjectToNode(snake, Player.GetPosition());

        }


        private void CreateApple(int amount)
        {
 
            apples = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {               
                
                apples[i] = CreateObject("Apple" + i);
                CreateSprite(apples[i], 1, 1, Color.green, 1);               
                SetObjectToNode(apples[i] , GetAvailableNode().GetPosition());
                
            }
        }

    #endregion

    #region update

        // Обрабатываем события нажатия кнопок
        // 1. Определяем направление движения по нажатой кнопке
        // 2. Двигаем игрока по направлению движения
        
        private IEnumerator Routine (float updateTime) 
        {
            WaitForSeconds time = new WaitForSeconds (updateTime);
            while(true)
            {
                yield return time;
                MoveSnake((int)GetDirection());
            }
        }

        
        
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

        
        private void MoveSnake(int direction)
        {
            int x0 = 0;
            int y0 = 0;
            switch (direction)
            {   
                case 0: y0 = 1; break;
                case 1: y0 = -1; break;
                case 2: x0 = -1; break;
                case 3: x0 = 1; break;
                case 4: x0 = 0; y0 = 0; break;
            }
            
            int x1 = Player.GetPosition().x + x0; 
            int y1 = Player.GetPosition().y + y0;
            
            
            if (!(y1 > width -1 || y1 < 0 || x1 < 0 || x1 > height -1)){
                if (CheckNodeAvailability (nodes[x1, y1]))
                {
                    SetNodeAvailability(Player.GetPosition(), true);
                    Player.SetPosition(nodes[x1, y1].GetPosition());
                    SetObjectToNode(snake, Player.GetPosition());



                    /*
                    if(snake.Count==1)
                    {
                        snake.Last().SetNode(nodes[x,y]);
                    }
                    else
                    {
                        snake[snake.Count-1].GetParentNodeAndSetChildNode(nodes[x,y]);
                        snake[snake.Count-2].GetParentNodeAndSetChildNode(snake[snake.Count-1].GetNode());
                        //for (int i = snake.Count; i > 1; i--)
                        //{
                           // snake[i].GetParentNodeAndSetChildNode(snake[i-1].GetNode());
                        //}
                    }
                    SetNodeAvailability(snake.Last().GetNode(), false);
                    cameraObj.transform.position = new Vector3 (x, y,-15f);
                    */
                }
            }
            
        }

    #endregion

    #region functions

        //Создаем объект
        private GameObject CreateObject(string name, string parent = "Scene")
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(GameObject.Find(parent).transform);
            
            return obj;
        }

        //Создаем спрайт объекта
        private void CreateSprite(GameObject obj, int width, int height, Color color, int layer) 
        {   
            SpriteRenderer objRndr = obj.AddComponent<SpriteRenderer>();
            Texture2D texture = new Texture2D (width, height);
            texture.filterMode = FilterMode.Bilinear;
            
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            
            Rect rect = new Rect(0,0,width, height);
            objRndr.sprite =  Sprite.Create(texture, rect, Vector2.zero, 1,0, SpriteMeshType.FullRect);
        }

        public void SetCameraPosition(GameObject camera, GameObject obj)
        {
            camera.transform.position = new Vector3(obj.transform.position.x ,obj.transform.position.y, -15f);
        }




        private Node GetAvailableNode()
        {
            int randomNode = Random.Range(0, availablenodes.Count);
            return availablenodes[randomNode];

        }

        private void SetObjectToNode(GameObject obj, Position3D position)
        {
            Node node = nodes[position.x, position.y];
            node.SetObject(obj);
            SetNodeAvailability(position, false);
        }

        public bool CheckNodeAvailability(Node node)
        {
            bool isAvailable = false;
            if (availablenodes.Contains(node))
            {
                isAvailable = true;
            }
            return isAvailable;
        }

        public void SetNodeAvailability(Position3D position, bool trueOrFalse)
        {
            Node node = nodes[position.x, position.y];
            if(trueOrFalse){
                availablenodes.Add(node);
            }
            else{
                availablenodes.Remove(node);
            }
        }

    #endregion













/* 
        public SceneObject map;    
        public SceneObject[] apples;
        
        public SceneObject head; 
        public List<SceneObject> snake;

        public Node [,] nodes;
        public List<Node> availableNodes;

        private int mapWidth = 20;
        private int mapHeight= 20;

          

        #region START 
        /////////////      

        // Запускаем игру:
        // 1. Создаем карту;
        // 2. Создаем яблоки;
        // 3. Создаем змейку;
        // 4. Запускаем куратину;

        private void Start() 
        {
            
            CreateMap(mapWidth, mapHeight);
            CreateApple(15);
            CreateSnake();
            StartCoroutine(Routine(0.05f)); 
             
        }

   

        // Создаем карту:
        // 1. Создаем массив доступных ячеек;
        // 2. Все доступные ячейки записываем в лист достуаных ячеек;
        // 3. Заполняем массив и лист ячейками;
        // 4. Создаем карту;

        private void CreateMap(int width, int height)
        {
            
            Debug.Log("Создаем карту...");
            nodes  = new Node [width, height];
            availableNodes = new List<Node>();

            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    Node node = new Node(new Vector2(x, y));
                    nodes[x,y] = node;
                    SetNodeAvailability(node, true);
                }
            }
            map = CreateObjectOnScene("Map", width, height, Color.white, 0);
                        
        }

        // Создаем змейку;
        public void CreateSnake()
        {
            Debug.Log("Создаем змейку...");
            head = CreateObjectOnScene("Snake", 1, 1, Color.black, 2);
            
            snake = new List<SceneObject>();
            snake.Add(head);

            head.SetNode(GetAvailableNode());
            SetNodeAvailability(head.GetNode(), false);

            

        }

        // Создаем яблоки;
        private void CreateApple(int amount)
        {
            Debug.Log("Создаем яблоки...");
            apples = new SceneObject[amount];
            
            for (int i = 0; i < amount; i++)
            {               
                apples[i] = CreateObjectOnScene("Apple" + i, 1, 1, Color.green, 1);
                apples[i].SetNode(GetAvailableNode());
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
            
            
            Vector2 position = snake.Last().GetNode().GetNodePosition();
            x = (int) position.x + x; 
            y = (int) position.y + y;
            
            if (!(y > mapWidth -1 || y < 0 || x < 0 || x > mapHeight -1)){
                if (CheckNodeAvailability (nodes[x,y]))
                {
                    SetNodeAvailability(snake.Last().GetNode(), true);
                    
                    if(snake.Count==1)
                    {
                        snake.Last().SetNode(nodes[x,y]);
                    }
                    else
                    {
                        snake[snake.Count-1].GetParentNodeAndSetChildNode(nodes[x,y]);
                        snake[snake.Count-2].GetParentNodeAndSetChildNode(snake[snake.Count-1].GetNode());
                        //for (int i = snake.Count; i > 1; i--)
                        //{
                           // snake[i].GetParentNodeAndSetChildNode(snake[i-1].GetNode());
                        //}
                    }
                    SetNodeAvailability(snake.Last().GetNode(), false);
                    cameraObj.transform.position = new Vector3 (x, y,-15f);
                }
                else{

                    foreach (var apple in apples)
                    {
                        if (apple.GetNode() == nodes[x,y]){
                            
                            apple.SetNode(GetAvailableNode());
                            SetNodeAvailability(apple.GetNode(), false);
                                                       
                            SceneObject newHead = CreateObjectOnScene("Snake" + (snake.Count -1), 1, 1, Color.black, 2);
                            newHead.SetChild(snake.Last());
                            newHead.SetNode(nodes[x,y]);
                            
                            cameraObj.transform.position = new Vector3 (x, y,-15f);
                            snake.Add(newHead);
                        }
                        
                    }

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

        //Создаем спрайт объекта
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

        private Node GetAvailableNode()
        {
            int randomNode = Random.Range(0, availableNodes.Count);
            return availableNodes[randomNode];

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

        */

    }
}

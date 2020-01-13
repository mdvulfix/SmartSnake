using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SmartSnake
{
    #region struct
        public struct Triangle
        {
            public int v1;
            public int v2;
            public int v3;

            public Triangle (int v1, int v2, int v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }
    #endregion
    [ExecuteInEditMode]
    public class Controller : MonoBehaviour
    {
  

        int width = 1;
        int height = 1;

        
        [SerializeField]
        int xSize = 1;
        [SerializeField]
        int zSize = 1;
        [SerializeField]
        float scale = 1;
        
        
        int amountOfApples = 5;


        GameObject terrain;
        [SerializeField]
        Material materialOfTerrain;


        
        GameObject map2D; 
         
        public Position[,] gridOfMap2D;
        List<Position> allPositions;
        
        GameObject player;
        List<SceneObject> snake;
        List<SceneObject> apples;

        int direction = 0;

        bool startMove;

    #region start
        
        private void Awake() 
        {
            //CreateMap2D(width, height);
            //CreateSnake();
            //CreateApple(amountOfApples);

            CreateTerrain(xSize, zSize, scale);
        }

        // Запускаем игру:
        // 1. Создаем карту;
        // 2. Создаем яблоки;
        // 3. Создаем змейку;
        // 4. Запускаем куратину;
        void Start() 
        {

            //StartCoroutine(Routine(0.075f)); 

        }

        // Создаем карту:
        // 1. Создаем массив доступных ячеек;
        // 2. Все доступные ячейки записываем в лист достуаных ячеек;
        // 3. Заполняем массив и лист ячейками;
        // 4. Создаем карту;
        void CreateMap2D(int width, int height)
        {
            // Создаем карту;
            map2D = CreateObject("Map2D");
            CreateSprite(obj: map2D, width: width, height: height, layer: 1);
            
            gridOfMap2D = new Position[width, height];
            allPositions = new List<Position>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridOfMap2D [x,y]  = new Position(x, y);
                    allPositions.Add(gridOfMap2D [x,y]);
                }
            }  

            //GameObject objCamera = CreateObject("Camera", "Map");
           // objCamera.transform.localPosition = new Vector3 (width/2f, height/2f,-10f);
            //objCamera.AddComponent<Camera>().fieldOfView = 100f; 
        }
        
        // Создаем карту;
        void CreateTerrain(int xSize, int zSize, float scale)
        {
            
            
            
            
            
            terrain = CreateObject("Terrain");
            terrain.AddComponent<MeshRenderer>();
            terrain.AddComponent<MeshFilter>();

            Mesh objMesh = new CustomMesh(terrain.name, xSize, zSize, scale).CreateMesh();
            terrain.GetComponent<MeshFilter>().mesh = objMesh;



            //CreateMesh(obj: terrain, width: width, length: length, material: materialOfTerrain);
            
        }
        
        // Создаем змейку;
        void CreateSnake()
        {
            // Создаем змейку;
            CreateObject("Snake");

            SceneObject head = CreateObjectOnScene(CreateObject("Head", "Snake"), GetAvailablePosition());
            SetPositionAvailability(head.GetPosition(), false);
            CreateSprite(obj: head.GetObject(), color: Color.black, layer: 3);




            snake = new List<SceneObject>();
            snake.Add(head);
        }
        
        // Создаем яблоки;
        void CreateApple(int amount)
        {
 
            apples = new List<SceneObject>();
            for (int i = 0; i < amount; i++)
            {               
                SceneObject apple = CreateObjectOnScene(CreateObject("Apple" + i), GetAvailablePosition());
                SetPositionAvailability(apple.GetPosition(), false);
                CreateSprite(obj: apple.GetObject(), color: Color.green, layer: 2);   

                apples.Add(apple);

            }
        }

    #endregion

    #region update


        void Update() 
        {

        }



        // Обрабатываем события нажатия кнопок
        // 1. Определяем направление движения по нажатой кнопке
        // 2. Двигаем игрока по направлению движения

        
        IEnumerator Routine (float updateTime) 
        {
            WaitForSeconds time = new WaitForSeconds (updateTime);
            while(true)
            {
                yield return time;
                
                if (apples.Count == 0)
                    Debug.Log ("Winner winner chicken dinner!!");

                if (apples.Count == 0)
                    Debug.Log ("Winner winner chicken dinner!!");
                
                MoveSnake(GetDirection(0, out direction));



            }
        }

        int GetDirection(int start, out int finish)
        {
            int direction = start;
            //bool isMoving = true;
            if(Input.GetButton("Up")){
                direction = 1;
            } 
            else if(Input.GetButton("Down")){
                direction = 2;                   
            }  
            else if(Input.GetButton("Left")){
                direction = 3;
            }
            else if(Input.GetButton("Right")){
                direction = 4;
            }
            else if(Input.GetButtonUp("Up") || Input.GetButtonUp("Down") || Input.GetButtonUp("Left") || Input.GetButtonUp("Right"))
            {
                direction = 0;
            }
            
            
            finish = direction;
            return direction;
        }

        void MoveSnake(int direction)
        {

            Position step = new Position();
            switch (direction)
            {   
                case 1: step.y = 1; break;
                case 2: step.y = -1; break;
                case 3: step.x = -1; break;
                case 4: step.x = 1; break;
                case 0: step.x = 0; step.y = 0; break;
            }
            
             Position position = snake[0].GetPosition() + step;
           
            
            if (!(position.y > width -1 || position.y < 0 || position.x < 0 || position.x > height -1)){
                if (CheckPositionAvailability(position))
                {

                    for (int i = 0; i < snake.Count; i++)
                    {               
                        Position startPosition = snake[i].GetPosition();
                        SetSceneObjectToPosition(snake[i], position);
                        position = startPosition;               
                    }

                }
                else 
                {

                    
                    SceneObject sobj;
                    if(SeachForSceneObject(position, out sobj))
                    {
                        
                        if (allPositions.Count >= apples.Count)
                            SetSceneObjectToPosition(sobj, GetAvailablePosition()); 
                        else
                        {
                            sobj.GetObject().SetActive(false);
                            SetPositionAvailability(sobj.GetPosition(), true);
                            apples.Remove(sobj);
                            Debug.Log(apples.Count);
                            
                        }
                            
                        for (int i = 0; i < snake.Count; i++)
                        {               
                            Position startPosition = snake[i].GetPosition();
                            SetSceneObjectToPosition(snake[i], position);
                            position = startPosition;               
                        }

                        SceneObject tail = CreateObjectOnScene(CreateObject("Tail" + snake.Count, "Snake"), position);
                        SetPositionAvailability(tail.GetPosition(), false);
                        CreateSprite(obj: tail.GetObject(), color: Color.black, layer: 2);
                        
                        snake.Add(tail);
                        
                    }

                }
               
            }
        }  
        

    #endregion

    #region functions

        //Создаем объекты на сцене
        SceneObject CreateObjectOnScene(GameObject obj, Position position)
        {          
            return new SceneObject(obj, position);
        }

        //Создаем объект
        GameObject CreateObject(string name, string parent = null)
        {          
            GameObject obj = new GameObject(name);
            if (parent != null)
                obj.transform.parent = GameObject.Find(parent).transform;
            else
                obj.transform.parent = this.transform;           

            return obj;
        }

        //Создаем спрайт объекта
        void CreateSprite(GameObject obj, int width = 1, int height = 1, Color color = default(Color), int layer = 0) 
        {   
            SpriteRenderer objSR = obj.AddComponent<SpriteRenderer>();
            Texture2D texture = new Texture2D (width, height);
        
            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    if (color == default(Color))
                    {   
                        Color custom;
                        
                        if((x + y) % 2 == 0)
                            custom = Color.gray;
                        else
                            custom = Color.white;
                        
                        texture.SetPixel(x, y, custom);

                    }
                    else 
                        texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            texture.filterMode = FilterMode.Point;
            
            Rect rect = new Rect(0,0,width, height);
            objSR.sprite =  Sprite.Create(texture, rect, Vector2.zero, 1,0, SpriteMeshType.FullRect);
            objSR.sortingOrder = layer;
        }


        Position GetAvailablePosition()
        {
            
            int position = Random.Range(0, allPositions.Count);
            return allPositions[position];

        }

        void SetSceneObjectToPosition(SceneObject sobj, Position position)
        {
            SetPositionAvailability(sobj.GetPosition(), true);
            sobj.SetPosition(position);
            SetPositionAvailability(position, false);
        }

        bool CheckPositionAvailability(Position position)
        {
            bool isAvailable = false;
            if (allPositions.Contains(position))
            {
                isAvailable = true;
            }
            return isAvailable;
        }
        
        /*
        int FindAvailablePositions(Position position)
        {
            int availableDirection = 0;
            for (int i = 1; i < 5; i++)
            {
                switch (i)
                {   
                    case 1: position.y += 1; break;
                    case 2: position.y -= 1; break;
                    case 3: position.x -= 1; break;
                    case 4: position.x += 1; break;
                }

                if (!(position.y > width -1 || position.y < 0 || position.x < 0 || position.x > height -1))
                {
                    SceneObject apple;                    
                    if(SeachForSceneObject(position, out apple))
                    {
                        availableDirection = i;
                        continue;
                    }
                }
                
            }
            return availableDirection;
        }
        */

        void SetPositionAvailability(Position position, bool trueOrFalse)
        {
            if(trueOrFalse){
                allPositions.Add(position);
            }
            else{
                allPositions.Remove(position);
            }
        }

        bool SeachForSceneObject(Position position, out SceneObject sobj)
        {

            bool trueOrFalse = false;
            sobj = null;
            foreach (SceneObject apple in apples)
            {
                if(apple.GetPosition() == position)
                {
                    sobj = apple;
                    trueOrFalse = true;
                    
                    //apple.GetObject().SetActive(false);
                    //SetPositionAvailability(apple.GetPosition(), true);
                }
            }
            /*
            foreach (SceneObject tail in snake)
            {
                if(tail.GetPosition() == position)
                {
                    isSnake = true;
                    return tail;
                    //apple.GetObject().SetActive(false);
                    //SetPositionAvailability(apple.GetPosition(), true);
                }
            }
            */

            return trueOrFalse;
        }

    
    }
    #endregion
}

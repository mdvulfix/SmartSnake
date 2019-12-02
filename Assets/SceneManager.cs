using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    
    SceneObject map;
    
    
    void Awake()
    {


    }

    void Start()
    {
        
        map = new SceneObject(new GameObject(), "Map");
    }


    void Update()
    {
        
    }
}

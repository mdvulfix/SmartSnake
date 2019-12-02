using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    
    private string name;
    private GameObject obj;

    public SceneObject(GameObject obj, string name)
    {
        
        SetName(name);
        SetObject(obj);


    }

    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetObject(GameObject obj)
    {
        this.obj = obj;
        this.obj.name = GetName();
    }



    public string GetName()
    {
        return name;
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObjectInChangingScene : MonoBehaviour
{
    public string objectName;
    
    private void Awake()
    {
        Debug.Log(GameObject.Find(objectName));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

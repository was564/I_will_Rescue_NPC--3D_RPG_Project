using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStatus : MonoBehaviour
{
    public int HP = 100;
    public int MaxHP = 100;
    
    public int Power = 10;

    public GameObject lastAttackTarget = null;
    
    public string characterName = "Player";

    public bool battleMode = false;
    public bool attacking = false;
    public bool damaged = false;
    public bool died = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

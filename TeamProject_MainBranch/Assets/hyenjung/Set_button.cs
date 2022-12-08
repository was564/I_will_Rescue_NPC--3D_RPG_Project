using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_button : MonoBehaviour
{
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else if (gameObject.activeSelf == false)
        {
            Time.timeScale = 1;
        }
    }
}

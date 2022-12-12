using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public GameObject gameObject;
    int show = 0;
    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && show == 0)
        {
            Debug.Log("충돌 처리는 됐음");
            gameObject.SetActive(true);
            Debug.Log("셋 엑티브 지나감");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            gameObject.SetActive(false);
            show = 1;
        }
    }
}

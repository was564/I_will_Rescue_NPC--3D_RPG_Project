using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{



    private void Awake()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        // 태그나 레이어를 통해서 Enemy를 구분
        //other.SendMessage();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int hp = 100;
    [SerializeField]
    int damage = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealPlayer(int value)
    {
        hp += value;
        if (hp > 100)
            hp = 100;
    }

    // 데미지를 입을 함수 설정
    void AttackPlayer(int damage)
    {
        hp -= damage;
        if (hp <= 0) 
        { 
            hp = 0; 
        }
    }
}

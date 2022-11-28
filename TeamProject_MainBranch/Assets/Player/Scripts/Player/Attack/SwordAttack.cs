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
        AttackInfo attack = new AttackInfo();

        attack.attackPower = 5;
        attack.attacker = this.transform;

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.SendMessage("Damage", attack);
            Debug.Log(other.name + "À» ¶§¸²");
        }
    }

}

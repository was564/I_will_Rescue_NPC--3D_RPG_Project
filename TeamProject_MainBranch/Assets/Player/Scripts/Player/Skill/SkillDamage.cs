using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        AttackInfo attack = new AttackInfo();

        attack.attackPower = 10;
        attack.attacker = this.transform;

        if (other.gameObject.tag.Equals("Enemy"))
        {
            other.SendMessage("Damage", attack);
            Debug.Log(other.name + "À» ¶§¸²");
        }
    }
}

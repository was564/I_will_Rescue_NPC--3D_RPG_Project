using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SimpleRangeMobAnimation : MonoBehaviour
{
    Animator animator;
    private AttackArea attackArea;
    ObjectStatus status;
    private SimpleRangeMobCtrl enemyCtrl;
    Vector3 prePosition;
    bool isDown = false;
    bool attacked = false;
    private bool isDamage = false;
    private bool isBattle = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<ObjectStatus>();
        enemyCtrl = GetComponent<SimpleRangeMobCtrl>();
        prePosition = transform.position;
        attackArea = GetComponentInChildren<AttackArea>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta_position = transform.position - prePosition;
        if (delta_position != Vector3.zero)
        {
            animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
        }

        if (attacked && !status.attacking)
        {
            attacked = false;
        }
        animator.SetBool("Attack1", (!attacked && status.attacking && !enemyCtrl.usingBullet.activeSelf));
        

        if(!isDown && status.died)
        {
            isDown = true;
            animator.SetTrigger("Down");
        }

        if (!isBattle && status.battleMode)
        {
            isBattle = true;
        }
        animator.SetBool("Battle", (isBattle && status.battleMode));

        if (!isDamage && status.damaged)
        {
            isDamage = true;
            status.damaged = false;
            animator.SetTrigger("Damage");
        }
        
        prePosition = transform.position;
    }
    
    public bool IsAttacked()
    {
        return attacked;
    }
	
    void StartAttackHit()
    {
        attackArea.SendMessage("OnAttack");
    }
	
    void EndAttackHit()
    {
        attackArea.SendMessage("OnAttackTermination");
    }
	
    void EndAttack()
    {
        attacked = true;
    }

    void EndHit()
    {
        isDamage = false;
    }
}

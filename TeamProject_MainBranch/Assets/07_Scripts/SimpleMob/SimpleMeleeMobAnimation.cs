using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SimpleMeleeMobAnimation : MonoBehaviour
{
    Animator animator;
    private AttackArea attackArea;
    public float range = 4.0f;
    ObjectStatus status;
    private SimpleMeleeMobCtrl enemyCtrl;
    Vector3 prePosition;
    bool isDown = false;
    bool attacked = false;
    private bool isDamage = false;
    private bool isBattle = false;

    private bool hasTwoAttacks;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        status = GetComponent<ObjectStatus>();
        enemyCtrl = GetComponent<SimpleMeleeMobCtrl>();
        prePosition = transform.position;
        attackArea = GetComponentInChildren<AttackArea>();
        
        animator.SetBool("Attack2", true);
        if (!animator.GetBool("Attack2")) hasTwoAttacks = false;
        else hasTwoAttacks = true;
        
        if(hasTwoAttacks) animator.SetBool("Attack2", false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta_position = transform.position - prePosition;
        animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);

        
        
        float distance = Single.PositiveInfinity;
        if(enemyCtrl.attackTarget != null) distance = Vector3.Distance(enemyCtrl.attackTarget.transform.position, transform.position);
        
        if(attacked && !status.attacking)
        {
            attacked = false;
        }
        if (distance <= range/2)
        {
            animator.SetBool("Attack1", (!attacked && status.attacking));
            if(hasTwoAttacks)
                animator.SetBool("Attack2", !(!attacked && status.attacking));
        }
        else if (distance <= range)
        {
            if (hasTwoAttacks)
            {
                animator.SetBool("Attack2", (!attacked && status.attacking));
                animator.SetBool("Attack1", !(!attacked && status.attacking));
            }

            if(!hasTwoAttacks)
                    animator.SetBool("Attack1", false);
        }

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
        status.damaged = false;
    }
}

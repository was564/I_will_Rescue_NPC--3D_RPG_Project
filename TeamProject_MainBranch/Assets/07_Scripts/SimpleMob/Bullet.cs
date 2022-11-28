using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Collider attackCollider;

    private Transform attackTarget;

    public Vector3 currentDirection;

    private ObjectStatus objectStatus;
    
    
    
    public float velocity = 2.0f;
    public float maxRotate = 0.3f;
    public float maxAliveTime = 5.0f;
    
    private float aliveTime = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        currentDirection = (attackTarget.position - transform.position);
        currentDirection.y = 0;
        currentDirection = currentDirection.normalized;
        attackCollider = GetComponent<Collider>();
        objectStatus = GetComponent<ObjectStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackCollider.enabled) attackCollider.enabled = true;
        
        transform.position += currentDirection * (velocity * Time.deltaTime);
        
        Vector3 goalDirection = attackTarget.position - transform.position;
        goalDirection.y = 0;
        goalDirection = goalDirection.normalized;
        
        currentDirection = Vector3.RotateTowards(
            currentDirection, goalDirection,
            maxRotate * Time.deltaTime, -maxRotate * Time.deltaTime);
        currentDirection = currentDirection.normalized;
        
        transform.Rotate(10, 10, 10);
        
        aliveTime += Time.deltaTime;
        if ( objectStatus.lastAttackTarget || aliveTime >= maxAliveTime)
        {
            aliveTime = 0.0f;
            objectStatus.lastAttackTarget = null;
            this.gameObject.SetActive(false);
        }

    }

    void SetTarget(Transform target)
    {
        attackTarget = target;
    }
    
}

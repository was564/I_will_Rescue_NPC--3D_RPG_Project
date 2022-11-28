using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAVA_Animation : MonoBehaviour
{
    Animator animator;
    Vector3 prePosition;
    bool isattacking = false;
    bool isdamaged = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        prePosition = transform.position;
    }
    void Update()
    {
        Vector3 delta_postion = transform.position - prePosition;
        animator.SetFloat("speed", delta_postion.magnitude / Time.deltaTime);

        if(Input.GetKeyDown("z"))
        {
            isattacking = true;
        }
        animator.SetBool("isattacking", isattacking);

        if(Input.GetKeyDown("x"))
        {
            isdamaged = true;
        }
        animator.SetBool("isdamaged", isdamaged);

        prePosition = transform.position;
    }
    

}

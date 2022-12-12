using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAVA_Animation : MonoBehaviour
{
    Animator animator;
    Vector3 prePosition;
    bool isattacking = false;
    bool isdamaged = false;
    public GameObject gameObject;
    public followPlayer followPlayer;
    public creatBaby creatBaby;
    public float time;
    bool timecheck = false;
  

    private void Start()
    {
        animator = GetComponent<Animator>();
        prePosition = transform.position;
        creatBaby = GetComponent<creatBaby>();
    }
    void Update()
    {      
       time += Time.deltaTime;
        

        Vector3 delta_postion = transform.position - prePosition;
        animator.SetFloat("speed", delta_postion.magnitude / Time.deltaTime);
        if(followPlayer.isplayer==true)
        {
            if (followPlayer.dist <= 5.0f)
            {
                isattacking = true;
            }
            animator.SetBool("isattacking", isattacking);
            isattacking = false;
        }
        else
        {
            animator.SetFloat("speed", 0);
        }
    

        if(time >= 5.0f )
        {

            if(timecheck == false)
            {
                timecheck = true;
                creatBaby.babycreate();
                
            }

            if (time >= 20.0f)
            {
                time = 0;
                timecheck = false;
            }

        }


        //if (gameObject == "Player")
        //{
        //    isattacking = true;
        //}
        //animator.SetBool("isattacking", isattacking);



        //if (Input.GetKeyDown("x"))
        //{
        //    isdamaged = true;
        //}
        //animator.SetBool("isdamaged", isdamaged);

        //prePosition = transform.position;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        isattacking = true;
    //    }
    //    animator.SetBool("isattacking", isattacking);
    //}

}

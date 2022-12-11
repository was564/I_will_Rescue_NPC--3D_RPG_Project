using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject gameObject;
    public float speed;
    public float dist;
    public bool isplayer=false;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
     
        if (other.tag == "Player")
        {
            dist = Vector3.Distance(transform.position, gameObject.transform.position);
            if(dist >= 5.0f && dist <= 20.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, speed);
            }
            transform.LookAt(gameObject.transform);       
            isplayer = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        isplayer = false;
    }
}

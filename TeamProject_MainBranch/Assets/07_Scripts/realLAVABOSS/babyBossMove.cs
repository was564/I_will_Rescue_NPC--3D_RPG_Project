using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyBossMove : MonoBehaviour
{
    public GameObject playerObject;
    bool isplayer = false;
    public float time;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
    }
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, 0.02f);
        transform.LookAt(playerObject.transform);
        if(time >= 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}

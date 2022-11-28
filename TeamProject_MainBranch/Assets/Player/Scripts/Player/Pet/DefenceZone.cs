using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceZone : MonoBehaviour
{
    float lifeTime = 0.0f;

    private void Update()
    {
        if (lifeTime > 10.0f)
        {
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<PlayerStatus>().isInDefenceArea = false;

            Destroy(this.gameObject);
        }
        else
            lifeTime += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().isInDefenceArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerStatus>().isInDefenceArea = false;
        }
    }
}

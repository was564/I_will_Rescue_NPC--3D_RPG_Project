using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
       // if(other.tag == "Player")
        Debug.Log("파티클 충돌");
    }
}

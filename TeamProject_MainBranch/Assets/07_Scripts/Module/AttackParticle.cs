using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public ParticleSystem particle;

    public Collider attackArea;

    private bool runningParticle = false;
    
    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.parent.GetComponent<Collider>();
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (runningParticle && !attackArea.enabled)
        {
            runningParticle = false;
            particle.Stop();
        }

        if (!runningParticle && attackArea.enabled)
        {
            runningParticle = true;
            particle.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecognizeEnemy : MonoBehaviour
{
    GameObject Pet;

    private void Awake()
    {
        Pet = GameObject.Find("Pet");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("Enemy"))
        {
            Pet.GetComponent<PetAI>().RecognizeEnemy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Pet.GetComponent<PetAI>().RecognizeEnemy = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float MaxHp = 100f;

    public float HP = 100f;

    private void Start()
    {
        HP = MaxHp;
    }

    void Update()
    {
        Debug.Log(HP);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (HP == 0)
            return;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            if(HP>0)
            {
                HP -= 10;
            }
            else
            {
                Debug.Log("체력이 없습니다.");
            }
        }
    }
}
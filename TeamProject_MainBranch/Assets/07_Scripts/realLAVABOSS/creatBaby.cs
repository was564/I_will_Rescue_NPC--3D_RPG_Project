using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatBaby : MonoBehaviour
{
    public GameObject rangeObject;
    public GameObject baby;
    public BoxCollider rangeCollider;
    bool isCoroutineStop = false;
    public float time;
    public List<GameObject> creatList;


    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
     public void babycreate()
    {
        //rangeCollider = GetComponent<BoxCollider>();
        Vector3 respon = Return_RandomPosition();
        GameObject instantCapsul = Instantiate(baby, Return_RandomPosition(), Quaternion.identity); // 1마리
        instantCapsul = Instantiate(baby, Return_RandomPosition(), Quaternion.identity); //2마리
        instantCapsul = Instantiate(baby, Return_RandomPosition(), Quaternion.identity); //3마리
    }
}

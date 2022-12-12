using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class responFire : MonoBehaviour
{
    public GameObject rangeObject;
    public GameObject fire;
    BoxCollider rangeCollider;
    bool isCoroutineStop = false;
    public float time;
    public List<GameObject> creatList;
    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

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
    void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time > 7f)
        {
            isCoroutineStop = true;
            StopCoroutine(RandomRespawn_Coroutine());
        }

        if(time >= 15f)
        {
            for(int i = 0; i<creatList.Count; i++)
            {
                Destroy(creatList[i]);
            }
            creatList.Clear();
        }

        if(time >=50f)
        {
            time = 0;
            isCoroutineStop = false;
            StartCoroutine(RandomRespawn_Coroutine());
        }
        
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while (true)
        {
            if(isCoroutineStop == true)
            {
                break;
            }
            yield return new WaitForSeconds(0.7f);

            // 생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입
            GameObject instantCapsul = Instantiate(fire, Return_RandomPosition(), Quaternion.identity);
            creatList.Add(instantCapsul);
        }

    }

 
}

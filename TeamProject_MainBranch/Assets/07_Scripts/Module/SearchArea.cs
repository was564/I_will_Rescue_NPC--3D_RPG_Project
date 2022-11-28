using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    public float giveUpChaseTime = 8.0f;
    
    private SimpleMeleeMobCtrl enemyCtrl;

    private bool notExistPlayerInCollider;
    private float exitTime;
    // Start is called before the first frame update
    void Start()
    {
        exitTime = 0.0f;
        notExistPlayerInCollider = true;
        enemyCtrl = transform.root.GetComponent<SimpleMeleeMobCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notExistPlayerInCollider && enemyCtrl.attackTarget)
        {
            if (exitTime >= giveUpChaseTime)
            {
                enemyCtrl.attackTarget = null;
                exitTime = 0.0f;
            }
            exitTime += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyCtrl.SetAttackTarget(other.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notExistPlayerInCollider = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notExistPlayerInCollider = true;
        }
    }
}

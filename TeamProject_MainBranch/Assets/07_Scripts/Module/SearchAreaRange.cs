using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAreaRange : MonoBehaviour
{
    public float giveUpChaseTime = 8.0f;
    
    private SimpleRangeMobCtrl enemyCtrl;
    
    private bool notExistPlayerInCollider;
    private float exitTime;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = true;
        exitTime = 0.0f;
        notExistPlayerInCollider = true;
        enemyCtrl = transform.root.GetComponent<SimpleRangeMobCtrl>();
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
        if (!other) return;
        if(other.tag == "Player") enemyCtrl.SetAttackTarget(other.transform);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
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

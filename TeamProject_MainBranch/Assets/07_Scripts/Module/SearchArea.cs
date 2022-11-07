using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    private SimpleMeleeMobCtrl enemyCtrl;
    // Start is called before the first frame update
    void Start()
    {
        enemyCtrl = transform.root.GetComponent<SimpleMeleeMobCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player") enemyCtrl.SetAttackTarget(other.transform);
    }
}

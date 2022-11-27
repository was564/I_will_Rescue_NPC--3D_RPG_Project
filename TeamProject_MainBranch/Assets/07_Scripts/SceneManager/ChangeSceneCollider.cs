using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneCollider : MonoBehaviour
{
    private GameSceneManager gameSceneManager;

    public string bossSceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameSceneManager = transform.parent.GetComponent<GameSceneManager>();

        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameSceneManager.SendMessage("ActivateScene", gameObject.name);
    }
}

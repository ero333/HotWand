using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class changeToLevel1 : MonoBehaviour {

    public GameObject cutsceneManager;

    void Update()
    {
        if (cutsceneManager.GetComponent<SceneSequence>().cutsceneStep == 4)
        {
            GetComponent<Animator>().SetBool("Can Continue", true);
        }
    }
    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (cutsceneManager.GetComponent<SceneSequence>().cutsceneStep == 4)
        {
            if (ChangeScene.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(4);
                Destroy(gameObject);
            }
        }
    }
}
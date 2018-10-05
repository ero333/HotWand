using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevel : MonoBehaviour {

    public GameObject cutsceneManager;
    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (cutsceneManager.GetComponent<SceneSequence>().cutsceneStep == 4)
        {
            if (ChangeScene.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(3);
                Destroy(gameObject);
            }
        }
    }
}
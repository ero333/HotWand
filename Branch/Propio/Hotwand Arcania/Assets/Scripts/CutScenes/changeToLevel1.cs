using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

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
        if (cutsceneManager.GetComponent<SceneSequence>().cutsceneStep == 5)
        {
            if (ChangeScene.gameObject.CompareTag("Player"))
            {
                Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
                {
                    {"nivel", 1}
                });
                SceneManager.LoadScene("CutPostTutorial");
                Destroy(gameObject);
            }
        }
    }
}
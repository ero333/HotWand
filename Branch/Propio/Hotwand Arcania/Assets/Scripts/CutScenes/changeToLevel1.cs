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
                Debug.Log("TerminarNivel");
                Debug.Log("nivel 0");
                Debug.Log("tiempo " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel));
                Debug.Log("puntos" + (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score));
                Debug.Log("muertes " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes);
                Analytics.CustomEvent("TerminarNivel", new Dictionary<string, object>
                {
                    {"nivel", 0},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"puntos", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) },
                    {"muertes", GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes }
                });

                Debug.Log("muertes ");
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
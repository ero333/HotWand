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
                Debug.Log("Evento TerminarNivel <"
                +"nivel 0"
                +"tiempo " + (Time.time - Globales.tiempoLevel)
                +"puntos" + (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score)
                +"muertes " + Globales.muertes+">");
                Analytics.CustomEvent("TerminarNivel", new Dictionary<string, object>
                {
                    {"nivel", 0},
                    {"tiempo", (Time.time - Globales.tiempoLevel) },
                    {"puntos", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) },
                    {"muertes", Globales.muertes }
                });

                Debug.Log("Evento EmpezarNivel < nivel: 1>");
                Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
                {
                    {"nivel", 1}
                });
                Globales.muertes = 0;
                Globales.tiempoLevel = Time.time;
                SceneManager.LoadScene("CutPostTutorial");
                Destroy(gameObject);
            }
        }
    }
}
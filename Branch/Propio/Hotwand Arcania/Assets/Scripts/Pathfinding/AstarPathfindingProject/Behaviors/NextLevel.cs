using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class NextLevel : MonoBehaviour {

	public int enemiesAlive = 0;
	[SerializeField] public int nextLevel;

	void Start()
	{
		//enemiesAlive = 0;
	}
    void Update()
    {
		if (enemiesAlive <= 0)
		{
			GetComponent<Animator>().SetBool("Can Continue", true);
		}
    }
    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (enemiesAlive <= 0)
        {
            if (ChangeScene.gameObject.CompareTag("Player"))
            {
                

                Debug.Log("Evento TerminarNivel <"
                +" nivel " +(nextLevel - 1) 
                +" tiempo " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)  
                +" puntos" + (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) 
                +" muertes " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes +">");
                Analytics.CustomEvent("TerminarNivel", new Dictionary<string, object>
                {
                    {"nivel", nextLevel - 1},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"puntos", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) },
                    {"muertes", GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes }
                });                
                Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
                {
                    {"nivel", nextLevel}
                });


                SceneManager.LoadScene(nextLevel+5);
                Destroy(gameObject);
            }
        }
    }
}
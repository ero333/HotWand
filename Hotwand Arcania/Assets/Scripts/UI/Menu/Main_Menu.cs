using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Main_Menu : MonoBehaviour {

	

	public void PlayGame()
	{
        print("EmpezarNivel");
        print(SceneManager.GetActiveScene().buildIndex + 2);
		Analytics.CustomEvent("EmpezarNivel", new Dictionary<string,object> 
		{
			{"nivel", SceneManager.GetActiveScene().buildIndex + 2}
		});
		Globales.muertes = 0;
        Globales.tiempoLevel = Time.time;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

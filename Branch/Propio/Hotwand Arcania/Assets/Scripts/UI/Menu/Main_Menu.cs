using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Main_Menu : MonoBehaviour {

	

	public void PlayGame()
	{
		Analytics.CustomEvent("EmpezarNivel", new Dictionary<string,object> 
		{
			{"nivel", SceneManager.GetActiveScene().buildIndex + 2}
		});
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}

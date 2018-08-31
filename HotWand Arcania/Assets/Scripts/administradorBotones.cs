using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class administradorBotones : MonoBehaviour {

	public void botonJugar() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Testing");
	}

	public void botonLogros() {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Logros");
	}

	public void botonSalir() {
		UnityEngine.Application.Quit ();
	}

}

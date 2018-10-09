using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level)
    {
        Analytics.CustomEvent("SeleccionarNivel", new Dictionary<string, object>
        {
            {"nivel seleccionado", level-3}
        });

        Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
        {
            {"nivel", level-3}
        });

        SceneManager.LoadScene(level); 
    }
}

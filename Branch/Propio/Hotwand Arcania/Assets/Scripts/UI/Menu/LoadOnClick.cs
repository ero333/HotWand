using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level)
    {
        //print("LoadOnClick");
        //print(level);
        if (level >2)        
        {
            //print("EmpezarNivel");
            //print(level);
            Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
            {
                {"nivel", level-3}
            });
        }
        SceneManager.LoadScene(level); 
    }

    public void SelectLevel(int level)
    {
       // print("SelectLevel");
        //print(level);
        if (level > 2)
        {
            //print("NivelSeleccionado");
            //print(level-3);
            Analytics.CustomEvent("SeleccionarNivel", new Dictionary<string, object>
            {
                {"nivel", level-3}
            });

           // print("EmpezarNivel");
            //print(level-3);
            Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
            {
                {"nivel", level-3}
            });

        }
        SceneManager.LoadScene(level);
    }

}

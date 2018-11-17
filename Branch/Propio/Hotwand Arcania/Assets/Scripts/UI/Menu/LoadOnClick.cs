using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

    private string sceneName;

   

    public void LoadScene(int level)
    {
        print("LoadOnClick");
        print(level);
        if (level >4)        
        {
            Debug.Log("EmpezarJugar");
            Globales.EmpezarJugar++;
            print("vez " + Globales.EmpezarJugar);
           
            Analytics.CustomEvent("EmpezarJugar", new Dictionary<string, object>
            {
                {"vez", Globales.EmpezarJugar},
            });
            

            print(level);
            Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
            {
                {"nivel", level-3}
            });
        }
        SceneManager.LoadScene(level); 
    }

    public void SelectLevel(int level)
    {
        print("SelectLevel");
        print(level);
        if (level > 2 && level < 15)
        {
            print("NivelSeleccionado");
            print(level-5);
            Analytics.CustomEvent("SeleccionarNivel", new Dictionary<string, object>
            {
                {"nivel", level-5}
            });

            print("EmpezarNivel");
            print(level-5);
            Analytics.CustomEvent("EmpezarNivel", new Dictionary<string, object>
            {
                {"nivel", level-5}
            });

        }
        Globales.muertes = 0;
        Globales.tiempoLevel = Time.time;
        SceneManager.LoadScene(level);
    }

}

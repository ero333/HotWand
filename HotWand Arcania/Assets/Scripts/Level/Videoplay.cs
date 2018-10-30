using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class Videoplay : MonoBehaviour {
    public static VideoPlayer videoInicial;
    private string sceneName;
    public GameObject camAnimacion;
    [SerializeField] private int VerCreditos;
    public float tiempoLevel;

    // Use this for initialization
    void Start()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        videoInicial = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        camAnimacion.SetActive(true);
        tiempoLevel = Time.time;

        switch (sceneName)
        {
            case "Creditos":
                videoInicial.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CREDITOSFINAL.mp4");
                break;
            case "CutIntro":
                videoInicial.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CUT1.mp4");
                break;
            case "CutPostTutorial":
                videoInicial.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CUT2.mp4");
                break;
            case "CutPreLevel1":
                videoInicial.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CUT3.mp4");
                break;
            case "FinalCut":
                videoInicial.url = System.IO.Path.Combine(Application.streamingAssetsPath, "FINALCUT.mp4");
                break;
            default:
                break;
        }

        videoInicial.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("Tutorial");
            if (sceneName == "Creditos")
                Analytics.CustomEvent("VerCreditos", new Dictionary<string, object>
                {
                    {"saltear", Time.time - tiempoLevel},
                });
            {
                SceneManager.LoadScene("Calificar");
                }
            if (sceneName == "CutIntro")
                {
                SceneManager.LoadScene("Tutorial");
                }             
            if (sceneName == "CutPostTutorial")
                {
                SceneManager.LoadScene("CutPreLevel1");
                }
            if (sceneName == "CutPreLevel1")
                {
                SceneManager.LoadScene("Lvl1");
                }
            if (sceneName == "FinalCut")

                {
                SceneManager.LoadScene("Creditos");
                Analytics.CustomEvent("VerCreditos", new Dictionary<string, object>
                {
                    {"vez", VerCreditos+=1},
                });
            }
            }
          }
       }
    


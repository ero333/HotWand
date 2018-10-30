using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    [SerializeField] private int VerCreditos;
    public float tiempoLevel;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        tiempoLevel = Time.time;

        Analytics.CustomEvent("VerCreditos", new Dictionary<string, object>
                {
                   {"vez", VerCreditos+=1},
                   {"saltear", Time.time - tiempoLevel}
                });
        SceneManager.LoadScene(SceneName);

    }
}

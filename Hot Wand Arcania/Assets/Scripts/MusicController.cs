using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
	AudioSource aus;
	// Use this for initialization
	void Start () {
		aus = this.GetComponent<AudioSource> ();
		aus.volume = PauseMenu.musicVal;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

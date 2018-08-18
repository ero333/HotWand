using UnityEngine;
using System.Collections;

public class VolumeCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<AudioSource> () != false) {
			if (this.GetComponent<MusicController> () != false) {
				this.gameObject.GetComponent<AudioSource> ().volume = PauseMenu.musicVal;
			} else {
				this.gameObject.GetComponent<AudioSource> ().volume = PauseMenu.sfxVal;
			}
			Destroy(this);
		} else {
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

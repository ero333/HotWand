using UnityEngine;
using System.Collections;

public class DogAnimate : MonoBehaviour {
	DogAI dai;
	public Sprite[] dogSprites;
	SpriteRenderer sr;
	float timer = 0.1f;
	float timerResetVal = 0.1f;
	int counter=0;
	// Use this for initialization
	void Start () {
		dai = this.GetComponent<DogAI> ();
		sr = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedCheck ();
		animate ();
	}

	void speedCheck()
	{
		if (dai.pursuingPlayer == true) {
			timerResetVal = 0.05f;
		} else {
			timerResetVal = 0.1f;
		}
	}

	void animate()
	{
		sr.sprite = dogSprites [counter];
		timer -= Time.deltaTime;
		if (timer <= 0) {
			if (counter < dogSprites.Length - 1) {
				counter++;
			} else {
				counter = 0;
			}
			timer = timerResetVal;
		}
	}
}

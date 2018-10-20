using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	public int score;
	public Text scoreText;
	public int ammo;
	public Text ammoText;


	// Use this for initialization
	void Start () {
		scoreText.text = "Score: " + score + "\n\nAmmo: " + ammo;
	}
	
	// Update is called once per frame
	void Update () {
		//puntaje += puntaje;
		scoreText.text = "Score: " + score.ToString() + "\n\nAmmo: " + ammo.ToString();
	}
}

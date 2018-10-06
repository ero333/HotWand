using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	private float puntaje = 0.0f;
	public Text scoreText;

	

	void Awake()
	{
	
	}

	// Use this for initialization
	void Start () {
		scoreText.text = "Score: " + puntaje;
	}
	
	// Update is called once per frame
	void Update () {

		
		puntaje += puntaje;
		scoreText.text = "Score: " + ((int)puntaje).ToString(); 
	}
}

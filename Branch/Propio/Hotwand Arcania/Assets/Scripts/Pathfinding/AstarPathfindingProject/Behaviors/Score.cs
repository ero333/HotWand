using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	public int score;
	public Text scoreText;
	public int ammo;
	public Text ammoText;
    public int muertes;
    public float tiempoLevel;
    public GameObject lastWeaponUsed;

    // Use this for initialization
    void Start () {
		scoreText.text = "Score: " + score + "\n\nAmmo: " + ammo;

        muertes = 0;
        tiempoLevel = Time.time;
        lastWeaponUsed = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().equippedWeapon;
    }
	
	// Update is called once per frame
	void Update () {
		//puntaje += puntaje;
		scoreText.text = "Score: " + score.ToString() + "\n\nAmmo: " + ammo.ToString();
	}
}

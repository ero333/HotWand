using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour {

	public GameObject player;
	public GameObject cutscene1;
	public GameObject cutscene2;
	public GameObject cutscene3;
	public GameObject cutscene4;
	public GameObject cutscene5;
	public GameObject cutscene6;
	public GameObject enemigo1;
	public GameObject enemigo2;
	public int cutsceneStep;
	private bool canContinue;
	[SerializeField] private float timeBetweenCutscenes;
	public float timeLeft;
	// Use this for initialization
	void Start () {
		cutsceneStep = 0;
		timeLeft = timeBetweenCutscenes;
	}
	
	// Update is called once per frame

	void Update () {		
		switch (cutsceneStep)
			{
				case 0:
					if ((cutscene1 == null))		
					{
						timeLeft -= Time.deltaTime;
						if (timeLeft < 0) 
						{
							if (cutscene2 != null) cutscene2.SetActive(true);
							cutsceneStep = 1;

							timeLeft = timeBetweenCutscenes;
						}
					
					}
				break;

				case 1:
					if (player.GetComponent<Equipment>().equippedWeapon != null)
					{
						if ((player.GetComponent<Equipment>().equippedWeapon.GetComponent<Weapon>().weaponName == "Wand"))		
						{
							timeLeft -= Time.deltaTime;
							if (timeLeft <= 0) 
							{
								if (cutscene3 != null) cutscene3.SetActive(true);
								cutsceneStep = 2;

								timeLeft = timeBetweenCutscenes;
							}
							
						}
					}
				break;

				case 2:
					if ((enemigo1.GetComponent<EnemyHealth>().dead))		
					{
						timeLeft -= Time.deltaTime;
						if (timeLeft < 0) 
						{						
							if (cutscene4 != null) cutscene4.SetActive(true);
							cutsceneStep = 3;

							timeLeft = timeBetweenCutscenes;
						}
						
					}
				break;

				case 3:
					if (player.GetComponent<Equipment>().equippedWeapon != null)
					{
						if ((player.GetComponent<Equipment>().equippedWeapon.GetComponent<Weapon>().weaponName == "Sword"))		
						{
							timeLeft -= Time.deltaTime;
							if (timeLeft < 0) 
							{
								if (cutscene5 != null) cutscene5.SetActive(true);
								cutsceneStep = 4;

								timeLeft = timeBetweenCutscenes;
							}
							
						}
					}
				break;

				case 4:
					if ((enemigo2.GetComponent<EnemyHealth>().dead))		
					{
						timeLeft -= Time.deltaTime;
						if (timeLeft < 0) 
						{
							if (cutscene6 != null) cutscene6.SetActive(true);
							cutsceneStep = 5;

							timeLeft = timeBetweenCutscenes;
						}
					
					}
				break;
			}
	}
}

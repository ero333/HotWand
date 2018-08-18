using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {
	public GameObject[] weapons;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		weapons = GameObject.FindGameObjectsWithTag ("Weapon");
	}

	public GameObject[] getWeapons()
	{
		return weapons;
	}
}

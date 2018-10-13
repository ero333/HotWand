﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private GameObject portal;
	// Use this for initialization
	void Start () {
		portal = GameObject.FindGameObjectWithTag("Portal");
		if (portal != null) portal.GetComponent<NextLevel>().enemiesAlive += 1;
	}
}
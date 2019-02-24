using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBar : MonoBehaviour {

	void onTriggerEnter2d ()
	{
		HealthBar.health -= 10f;
	}

}

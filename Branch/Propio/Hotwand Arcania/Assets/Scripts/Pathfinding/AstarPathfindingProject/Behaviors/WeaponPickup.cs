﻿using UnityEngine;
using System.Collections;

namespace Pathfinding {
	public class WeaponPickup : VersionedMonoBehaviour {
		/** The object that the AI should move to */
		public GameObject weaponEquipped;
		Transform target;
		IAstarAI ai;

		void Start()
		{
			GameObject[] weapons;
			weapons = GameObject.FindGameObjectsWithTag("Weapon");
			GameObject closestWeapon = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
			foreach (GameObject weapon in weapons)
			{
				Vector3 diff = weapon.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance)
				{
					closestWeapon = weapon;
					distance = curDistance;
				}
			}

			target = closestWeapon.transform;
		}
		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/** Updates the AI's destination every frame */
		void Update () {
			if (target != null && ai != null) ai.destination = target.position;
		}
	}
}

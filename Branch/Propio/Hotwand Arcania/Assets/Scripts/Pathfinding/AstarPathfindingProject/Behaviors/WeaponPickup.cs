using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
	public class WeaponPickup : VersionedMonoBehaviour {
		/** The object that the AI should move to */
		public GameObject weaponEquipped;
		public GameObject target;
		Transform targetPos;
		GameObject targetWeapon;
		IAstarAI ai;
		public void FindClosestWeapon()
		{
			float distanceToClosestWeapon = Mathf.Infinity;
			GameObject closestWeapon = null;
			GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("Weapon");

			foreach (GameObject currentWeapon in allWeapons) {
				float distanceToEnemy = (currentWeapon.transform.position - this.transform.position).sqrMagnitude;
				if (distanceToEnemy < distanceToClosestWeapon) {
					distanceToClosestWeapon = distanceToEnemy;
					closestWeapon = currentWeapon;
				}
			}
		
			if (closestWeapon != null ) {
                targetPos = closestWeapon.transform;
            }

            if (closestWeapon != null ) {
                target = closestWeapon;
            }
        }
		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) {
                ai.onSearchPath += Update;
            }
        }

		void OnDisable () {
			if (ai != null) {
                ai.onSearchPath -= Update;
            }
        }

		/** Updates the AI's destination every frame */
		void Update () {
			FindClosestWeapon();
			if (targetPos != null && ai != null) {
                ai.destination = targetPos.position;
            }
        }


	}
}

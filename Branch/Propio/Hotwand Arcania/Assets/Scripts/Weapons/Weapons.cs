using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapons : ScriptableObject {

	public new string name;
	public string weaponType;

	public Sprite sprite;

	public int damage;
	public int attackRate;
}

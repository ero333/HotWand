using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelEscapeController : MonoBehaviour {
	public GameObject col,van;
	public Sprite doorsOpen;
	SpriteRenderer sr;
	public GameObject[] enemies;
	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();

		GameObject[] dogs = GameObject.FindGameObjectsWithTag ("Dog");
		GameObject[] heavies = GameObject.FindGameObjectsWithTag ("Heavy");
		GameObject[] enemies1 = GameObject.FindGameObjectsWithTag ("Enemy");
		enemies = new GameObject[dogs.Length + heavies.Length + enemies1.Length];
		dogs.CopyTo (enemies, 0);
		heavies.CopyTo (enemies, dogs.Length);
		enemies1.CopyTo (enemies, (dogs.Length) + (heavies.Length ));

	}

	void Update () {
		if (areAllEnemiesDead () == true && sr.sprite != doorsOpen) {
			openCar ();
		}


	}

	public void shotFired()
	{
		foreach (GameObject enemy in enemies) {
			if (enemy.GetComponent<EnemyAI> () != null ) {
				if (enemy.GetComponent<EnemyAI> ().enabled == true) {
						//enemy.GetComponent<EnemyAI> ().heardGunshot (GameObject.FindGameObjectWithTag ("Player").transform.position);
				}
			}
		}
	}

	public bool areAllEnemiesDead()
	{
		for (int x = 0; x < enemies.Length; x++) {
			if (enemies [x].tag != "Dead") {
				return false;
			}
		}
		return true;
	}

	void openCar()
	{
		Destroy (col);
		sr.sprite = doorsOpen;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			
			MenuScreen menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuScreen>();
			menu.saveHighScore ();
			menu.display = true;
			SceneManager.LoadScene ("Menu");


		}
			
	}
}

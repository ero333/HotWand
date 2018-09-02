using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	int score = 0;
	int currentMultiplier = 1;
	float comboTimer = 0.0f;
	int tempScoreHold = 0;

	float originalWidth = 1920.0f; //turn these to floats to fix placement issue
	float originalHeight = 1080.0f;
	Vector3 scale;
	public GUIStyle text;
	public Texture2D bg;
	public GameObject fiveHun,thou;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		comboCountdown ();
	}

	public void AddScore(int val,Vector3 position)
	{
		tempScoreHold += val;
		Vector3 spawnPos = position;
		spawnPos.y += 2;

		if (val == 500) {
			Instantiate (fiveHun, spawnPos, fiveHun.transform.rotation);
		} else if (val == 1000) {
			Instantiate (thou, spawnPos, fiveHun.transform.rotation);
		}
	}

	public void increaseMultiplier()
	{
		Debug.Log ("Increased multiplier to " + currentMultiplier);
		currentMultiplier++;
		comboTimer = 3.5f;
	}

	public int getScore()//new
	{
		return score;
	}

	void comboCountdown()
	{
		
		if (tempScoreHold>0) {
			comboTimer -= Time.deltaTime;

			if (comboTimer <= 0) {
				score+= (tempScoreHold*currentMultiplier);
				tempScoreHold = 0;
				currentMultiplier = 1;
			}
		}
	}

	public void scoreReset(int val)//new
	{
		score = val;
	}

	void OnGUI()
	{
		GUI.depth = 0;
		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z =1;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero,Quaternion.identity,scale);
		if (CutsceneDisplay.anyCutsceneDisplaying == false) {
			Rect scorePos = new Rect (originalWidth - 500, (originalHeight - originalHeight) + 50, 200, 100);
			Rect multiPos = new Rect (originalWidth - 500, (originalHeight - originalHeight) + 100, 200, 100);
			Rect bgPos = new Rect (originalWidth - 750, (originalHeight - originalHeight) + 50, 700, 250);
			GUI.DrawTexture (bgPos, bg);
			if (PlayerHealth.dead == false) {//changed for ep on cutscenes
				GUI.Box (scorePos, "Score: " + score, text);
				GUI.Box (multiPos, "Combo: " + currentMultiplier + " * " + tempScoreHold + " - " + (int)comboTimer, text);
			} else if (PlayerHealth.dead == true) {
				GUI.Box (scorePos, "You Died", text);
			}
		}
		GUI.matrix = svMat;
	}
}

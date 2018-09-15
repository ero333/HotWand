using UnityEngine;
using System.Collections;

public class CutsceneDisplay : MonoBehaviour {
	public CutsceneContainer[] cutsceneBits;
	CutsceneContainer activeCutscene;
	public Texture2D activeFace;
	int faceAnimCounter = 0;
	int cutsceneCounter = 0;
	float faceAnimateTimer=0.15f,faceReturn=0.15f;
	public Texture2D bg;
	public GUIStyle text;

	float originalWidth = 1920.0f; //turn these to floats to fix placement issue
	float originalHeight = 1080.0f;
	Vector3 scale;
	bool display = false;
	public static bool anyCutsceneDisplaying = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		inputControl ();
		animateFace ();
	}

	void animateFace()
	{
		faceAnimateTimer -= Time.deltaTime;
		activeFace = activeCutscene.faces [faceAnimCounter];
		if (faceAnimateTimer <= 0) {
			if (faceAnimCounter < activeCutscene.faces.Length - 1) {
				faceAnimCounter++;
			} else {
				faceAnimCounter = 0;
			}
		}
	}

	void inputControl()
	{
		activeCutscene = cutsceneBits [cutsceneCounter];
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (cutsceneCounter < cutsceneBits.Length-1) {
				cutsceneCounter++;
			} else {

				anyCutsceneDisplaying = false;
				Time.timeScale = 1;
				Destroy (this.gameObject);


			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			display = true;
			Time.timeScale = 0;
			anyCutsceneDisplaying = true;
		}
	}

	void OnGUI()
	{
		GUI.depth = 0;
		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z =1;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero,Quaternion.identity,scale);
		if (display == true) {
			GUI.DrawTexture (new Rect (0, 0, originalWidth, originalHeight), bg);
			GUI.DrawTexture (new Rect (originalWidth - 500, originalHeight / 2-400, 500, 500), activeFace);
			GUI.Box (new Rect (originalWidth / 2-500, originalHeight - 150, 1000, 100), activeCutscene.Text,text);
		}
		GUI.matrix = svMat;
	}
}

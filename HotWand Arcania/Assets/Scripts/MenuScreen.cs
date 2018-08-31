using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuScreen : MonoBehaviour {

	static public MenuScreen mu;//new menu 2


	float originalWidth = 1920.0f; //turn these to floats to fix placement issue
	float originalHeight = 1080.0f;
	Vector3 scale;
	public bool display = false;

	public GUIStyle text,titleText,titleShadow;
	public Texture2D bg;

	bool playSelect=true,exitSelect=false,play=false,menu=true;
	public LevelStore[] levels;
	int levelSelectCount = 0;



	static public string curLevel; //new for menu 2
	// Use this for initialization

	void awake()
	{
		if (mu ==null) {//new for menu part 2
			mu = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(this);
		}

	
	}

	void Start () {
		if (SceneManager.GetActiveScene ().name.Equals ("Menu")) {
			display = true;
		} else {
			display = false;
		}
		checkForLevelUnlocked ();
	}

	public void saveHighScore()//new for menu 2
	{
		
		LevelStore ls = new LevelStore ();
		for (int x = 0; x < levels.Length; x++) {
			if (levels [x].levelName==curLevel) {
				ls = levels [x];


			}
		}

		ScoreController sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScoreController> ();
		Debug.Log ("Saving high score " + sc.getScore () + " For level " + curLevel);
		ls.save (sc.getScore ());
		checkForLevelUnlocked ();
	}
	
	// Update is called once per frame
	void Update () {
		if (display == true) {//all display stuff new for menu part 2
			inputController ();
		}




	}

	void checkForLevelUnlocked()
	{
		for (int x = 1; x <= levels.Length; x++) {
			if (levels [x - 1].highScore > 0) {
				levels [x].unlocked = true;
			}
		}
	}

	void inputController()
	{
		if (menu == true) {
			if (Input.GetKeyDown (KeyCode.S) && playSelect == true || Input.GetKeyDown (KeyCode.W) && playSelect == true) {
				exitSelect = true;
				playSelect = false;
			} else if (Input.GetKeyDown (KeyCode.S) && exitSelect == true || Input.GetKeyDown (KeyCode.W) && exitSelect == true) {
				exitSelect = false;
				playSelect = true;
			}

			if (Input.GetKeyDown (KeyCode.Return) && playSelect == true) {
				menu = false;
				play = true;
			}
			else if(Input.GetKeyDown (KeyCode.Return) && exitSelect==true)
			{
				Application.Quit ();
			}

		} else if (play == true) {
			curLevel = levels [levelSelectCount].levelName;
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				play = false;
				menu = true;
			}

			if (Input.GetKeyDown (KeyCode.D) && levelSelectCount < levels.Length - 1) {
				levelSelectCount++;
				checkForLevelUnlocked ();
			}

			if (Input.GetKeyDown (KeyCode.A) && levelSelectCount > 0) {
				levelSelectCount--;
				checkForLevelUnlocked ();
			}

			if (Input.GetKeyDown (KeyCode.Return) && levels [levelSelectCount].unlocked == true) {
				
				SceneManager.LoadScene (levels [levelSelectCount].sceneManagerName);
				display = false;
			}
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
			Rect titlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight, 800, 300);
			if (menu == true) {
				play = false;
				titlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight, 800, 300);
				GUI.Box (titlePos, "", titleShadow);


				titlePos = new Rect (originalWidth / 2 - 405, originalHeight - originalHeight - 5, 800, 300);
				GUI.Box (titlePos, "", titleText);






			} else if (play == true) {
				Rect backToRet = new Rect (originalWidth - originalWidth + 200, originalHeight - 200, 800, 200);
				GUI.DrawTexture (backToRet, bg);
				GUI.Box (backToRet, "Press backspace to return to menu", text);

				titlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight, 800, 300);
				GUI.Box (titlePos, "", titleShadow);


				titlePos = new Rect (originalWidth / 2 - 405, originalHeight - originalHeight - 5, 800, 300);
				GUI.Box (titlePos, "", titleText);

				if (levels [levelSelectCount].unlocked == true) {
					Rect levelTitlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight + 200, 800, 200);
					GUI.Box (levelTitlePos, levels [levelSelectCount].levelName, text);

					levelTitlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight + 300, 800, 200);
					GUI.Box (levelTitlePos, "High Score : " + levels [levelSelectCount].highScore, text);

					levelTitlePos = new Rect (originalWidth / 2 - 250, originalHeight - originalHeight + 400, 500, 500);
					GUI.DrawTexture (levelTitlePos, levels [levelSelectCount].levelIcon);
				} else {
					Rect levelTitlePos = new Rect (originalWidth / 2 - 400, originalHeight - originalHeight + 200, 800, 200);
					GUI.Box (levelTitlePos, "Level Locked", text);
					levelTitlePos = new Rect (originalWidth / 2 - 250, originalHeight - originalHeight + 400, 500, 500);
					GUI.DrawTexture (levelTitlePos, bg);
				}

			}
		}
		GUI.matrix = svMat;
	}
}

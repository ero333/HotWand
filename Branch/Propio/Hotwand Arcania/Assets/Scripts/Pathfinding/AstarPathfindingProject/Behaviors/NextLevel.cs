using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	public int enemiesAlive = 0;
	[SerializeField] private int nextLevel;

	void Start()
	{
		//enemiesAlive = 0;
	}
    void Update()
    {
		if (enemiesAlive <= 0)
		{
			GetComponent<Animator>().SetBool("Can Continue", true);
		}
    }
    void OnTriggerEnter2D(Collider2D ChangeScene)
    {
        if (enemiesAlive <= 0)
        {
            if (ChangeScene.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextLevel+3);
                Destroy(gameObject);
            }
        }
    }
}
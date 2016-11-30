using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	GameObject player;
	PlayerController pc;

	public string lastSceneName;
	public string currentSceneName;

	void Awake ()
	{
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		Object.DontDestroyOnLoad(this);
	}

	void Start () 
	{
		lastSceneName = "loadingscreennight";
	}
	

	void Update () 
	{
		currentSceneName = SceneManager.GetActiveScene().name;
	}
}

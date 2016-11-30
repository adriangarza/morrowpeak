using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Transitions between scenes, calls ScreenFader.
 */

public class TransitionController : MonoBehaviour {

	PlayerController pc;
	GameManager gm;
	ScreenFader sf;

	string[] nightscenes; // List of scenes that "occur" at night.

	void Awake ()
	{
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
		gm = GetComponent<GameManager>();
		sf = GetComponent<ScreenFader>();
	}

	void Start () {
		nightscenes = new string[] {"party", "partyoutside"};
	}
		
	public void Transition(string nextScene) {
		// Update player's destination.
		pc.destination = nextScene;

		// Check if the destination is a "night scene."
		// If yes, use loadingscreennight. Else, -day.
		bool temp = false;
		foreach (string scene in nightscenes)
		{
			if (pc.destination.Equals(scene))
			{
				temp = true;
			}
		}
		if (temp)
		{
			gm.lastSceneName = "loadingscreennight";
			sf.EndScene("loadingscreennight");
		}
		else
		{
			gm.lastSceneName = "loadingscreenday";
			sf.EndScene("loadingscreenday");
		}
	}
}

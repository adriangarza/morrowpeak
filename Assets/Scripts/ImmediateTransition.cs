using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Special script used to transition from "start" scene to the first level.
 */

public class ImmediateTransition : MonoBehaviour {

	ScreenFader sf;

    public string scene;

    void Awake () 
	{
		sf = GameObject.Find("GameManager").GetComponent<ScreenFader>();
	}

	void Start () 
	{
		sf.EndScene("loadingscreennight");
	}
	

	void Update () 
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	SpecialFader sf;
	GameObject gm;
	// Use this for initialization
	void Awake () {
		sf = GetComponent<SpecialFader>();
		gm = GameObject.Find("GameManager");
	}

	void Start () {
		if (gm != null)
		{
			GameObject.Destroy(gm);
		}
	}
			
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && !sf.sceneStarting)
		{
			sf.EndScene("start");
		}
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Transform t;
	float speed;
	PlayerController pc;
	GameManager gm;
	// Use this for initialization
	void Awake ()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	void Start () {
        //t = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.currentSceneName.Equals("loadingscreenday") || gm.currentSceneName.Equals("loadingscreennight"))
		{
            //move the player below the screen, this is undone in CarController in the next scene
			transform.position = new Vector3(0, 0, -2.7f);
		}
		else
		{
            //change camera for 2d or isometric viewpoint
			if (!pc.topdown)
			{
				transform.position = pc.transform.position + new Vector3(0, 0.85f, -10f);
			}
			else
			{
				transform.position = pc.transform.position + new Vector3(0, 0, -10f);
			}
		}
		//to rotate
		//t.Rotate(new Vector3(0, 0 speed));
	}
}

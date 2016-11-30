using UnityEngine;
using System.Collections;

public class FixPerspective : MonoBehaviour {

	PlayerController pc;
	// Use this for initialization
	void Start () {
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
		if (this.name.Equals("TOPDOWN"))
			pc.topdown = true;
		else 
			pc.topdown = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

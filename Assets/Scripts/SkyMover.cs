//this moves the stars behind the party

using UnityEngine;
using System.Collections;

public class SkyMover : MonoBehaviour {

    public Renderer rend;
    public float scrollSpeed;

    private float offset;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        if (scrollSpeed == 0) scrollSpeed = .001f;
	}
	
	// Update is called once per frame
	void Update () {
        offset += scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }
}

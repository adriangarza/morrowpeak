using UnityEngine;
using System.Collections;

/// <summary>
/// usage: attach this to one object, have another one
/// named the same thing but with "2" at the end, only
/// the position of the first one matters
/// 
/// this script takes two gameobjects and places them
/// side by side. when one moves off the screen it's
/// put in front of the other so they repeat
/// 
/// backgroundAnchor is what to use for the screen borders
/// 
/// </summary>

public class Repeater : MonoBehaviour {

    public float speed;
    public GameObject backgroundAnchor;

    //this overlaps the two sprites so there isn't any flickering 
    //if they move slowly, .002 is usually fine
    public float overlap;

    float horizMin;
    GameObject other;
    SpriteRenderer sr;

	// Use this for initialization
    void Start () {

        if (other == null) other = GameObject.Find(name + "2");
        if (backgroundAnchor == null) backgroundAnchor = GameObject.Find("bg");
      
        //the left edge of the camera
        horizMin = backgroundAnchor.GetComponent<SpriteRenderer>().bounds.min.x;

        //pin the second sprite to the right edge of the first sprite
        float length = other.GetComponent<SpriteRenderer>().bounds.extents.x;
        float newX = other.transform.position.x;

        sr = other.GetComponent<SpriteRenderer>();
        //other.transform.position = new Vector3(2 * (temp.bounds.max.x - temp.bounds.min.x), this.transform.position.y, this.transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
	    if (speed != 0)
        {
            gameObject.transform.Translate(-speed, 0, 0);
            other.transform.Translate(-speed, 0, 0);

            //if one is off the screen move it up
            if (gameObject.GetComponent<SpriteRenderer>().bounds.max.x < horizMin)
            {
                sr = gameObject.GetComponent<SpriteRenderer>();
                float length = sr.bounds.max.x - sr.bounds.min.x;
                gameObject.transform.Translate(2 * length - overlap, 0, 0);
            }

            if (other.GetComponent<SpriteRenderer>().bounds.max.x < horizMin)
            {
                sr = other.GetComponent<SpriteRenderer>();
                float length = sr.bounds.max.x - sr.bounds.min.x;
                other.transform.Translate(2 * length - overlap, 0, 0);
            }

        }

    }
}

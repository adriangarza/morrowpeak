using UnityEngine;
using System.Collections;

/// <summary>
/// how this works: attach it to any GameObject and set a parallax amount
/// a value of 1 means it moves with the player, less than one means it 
/// just moves a little bit. 0.8 is a good value for distant backgrounds.
/// 
/// negative numbers make something seem closer to the camera than the player
/// so something like -0.2 is used for the chairs in the airport, for example
/// 
/// anything bigger than 1 is Bad
/// 
/// the Car importing has to do with the persistent Player object, because
/// the objects will be moved based on where he was in the loading screen
/// 
/// </summary>

public class Parallax : MonoBehaviour {

    public float amount;
	GameObject car;
    GameObject player;
    float playerOrigX;
    float origX;

    //new stuff
    float prevX;
    float currX;

	void Awake ()
	{
		car = GameObject.Find("Car");
	}
    void Start () {
        player = GameObject.Find("Player");
        currX = player.transform.position.x;
        prevX = player.transform.position.x;
	}
	
	void Update () {
        if (amount != 0 && player.transform.position.x > GameObject.Find("Walls").GetComponent<BoxCollider2D>().bounds.min.x)
        {

             currX = player.transform.position.x;

            this.transform.Translate(new Vector2(amount * (currX - prevX), 0));

            prevX = player.transform.position.x;

        }
	}
}

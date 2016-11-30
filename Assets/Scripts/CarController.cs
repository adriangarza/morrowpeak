///<summary>
///
/// attach this to the car so the player can get in and out
/// 
/// makes sounds and triggers transitions, also takes care of
/// hiding the player and opening the map
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour {

	GameManager gm;
	GameObject player;
	PlayerController pc;
    SpriteRenderer spr;
    Animator anim;
	MapManager mm;
    AudioSource audioSource;

	public float threshold; // How close the player has to be to GetIn()

	void Awake() 
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = GameObject.Find("Player");
		spr = player.GetComponent<SpriteRenderer>();
		pc = player.GetComponent<PlayerController>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		mm = GameObject.Find("GameManager").GetComponent<MapManager>();
	}

    void Start () 
	{
		if (threshold == 0) threshold = 0.4f;
		// Re-positions player after being moved off-screen during loadingscreen.
		if (gm.lastSceneName == "loadingscreennight" || gm.lastSceneName == "loadingscreenday")
		{
			pc.transform.position = this.transform.position + new Vector3(0, -0.34f, 0);
		}
    }

	void Update () {
		//Gauge player's distance from car
		if (Mathf.Abs(player.transform.position.magnitude - gameObject.transform.position.magnitude) < threshold)
        {
            //Check if car is visible and User is pressing space bar.
            if (Input.GetKeyDown(KeyCode.Space) && GameObject.Find("Car").GetComponent<SpriteRenderer>().enabled)
            {
				if (!pc.isCar)
                {
					GetIn();
                }
            }
        }
	}

	// Make the player invisible, trigger "get in" animation.
	public void GetIn()
	{
		anim.SetTrigger("get in");
		pc.isCar = true;
		Color temp = spr.color;
		temp.a = 0;
		spr.color = temp;
		audioSource.Play();
		mm.StartCoroutine("OpenMap");
	}

	// Make the player visible, trigger "get out" animation.
	public void GetOut()
	{
		pc.isCar = false;
		pc.isMapping = false;
		anim.SetTrigger("get out");
		Color temp = spr.color;
		temp.a = 255;
		spr.color = temp;
	}
}

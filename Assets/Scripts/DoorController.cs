using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * Allows player to move between locations near each other.
 */


public class DoorController : MonoBehaviour {

	GameObject player;
	PlayerController pc;
	GameManager gm;
	ScreenFader sf;
	TransitionController tc;

    public float distance;	 // Player must be certain distance away to access Door.
    public string levelName; // The scene which this instace of Door leads to.
    public bool transition;  // In certain instances, player goes directly from Door to driving, in which case, loading screen must be called.

	void Awake ()
	{
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		sf = GameObject.Find("GameManager").GetComponent<ScreenFader>();
        tc = GameObject.Find("GameManager").GetComponent<TransitionController>();
    }
		

    void Start () 
	{
		// Upon entering scene, player's position snaps immediately to the door's position, plus minor vertical offset.
		if (gm.lastSceneName.Equals(levelName) || gm.lastSceneName.Equals("mainmenu"))
		{
			pc.transform.position = this.transform.position + new Vector3(0, .6f, 0);
		}
    }

	void Update () 
	{
		if (levelName != "" && CheckDistance())
        {
            // Player cannot interact with other things while walking through Door.
			if (Input.GetKeyDown(KeyCode.Space) && !pc.frozen)
			{
                // Door sound effect.
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play();
                }

				// Update lastSceneName and player's new destination; fade out for transition.
				gm.lastSceneName = gm.currentSceneName;
				pc.destination = levelName;
                if (transition) tc.Transition(pc.destination);
                else sf.EndScene(pc.destination);
			}
        }
	}

	// Check if player is close enough to Door to access it.
    protected bool CheckDistance()
    {
		bool temp = false;
        Vector2 thisPos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 otherPos = new Vector2(pc.transform.position.x, pc.transform.position.y);
        float distance = Vector2.Distance(thisPos, otherPos);
        if (distance < this.distance)
        {
            temp = true;
        }
		return temp;
    }
}

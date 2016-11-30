///<summary>
///
/// this is the master script for the mountains/trees
/// loading scene, for both day and night variations
/// 
/// basically all the code is for moving everything
/// at different speeds, with some random generation
/// and global gamestate manipulation
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class LoadingBackground : MonoBehaviour {

	PlayerController pc;
	GameObject player;
	ScreenFader sf;
	SpriteRenderer psr; //player sprite renderer
	SpriteRenderer sr;  //multi-purpose sprite renderer
    
	GameObject land;
	GameObject land2;
    GameObject cloud;
    GameObject cloud2;
    GameObject trees;
    GameObject trees2;
    GameObject pwLines;

	public string origin;
	public string destination;

    public float landSpeed;
    public float cloudSpeed;
    public float treeSpeed;
    public float lineSpeed;
    public float houseSpeed;
    public float horizMin;

	void Awake ()
	{
		player = GameObject.Find("Player");
		sf = GameObject.Find("GameManager").GetComponent<ScreenFader>();
        psr = player.GetComponent<SpriteRenderer>();
		pc = player.GetComponent<PlayerController>();
		pc.isTransition = true;
	}

    // Use this for initialization
    void Start () {
		pc.isCar = false;
		pc.isMapping = false;
		Color temp = psr.color;
		temp.a = 255;
        psr.color = temp;
        //this moves the player below the loading screen, which is 
        //undone by CarController's positioning in the next scene
		pc.transform.position = new Vector3(-100, -100, 0);
        land2 = GameObject.Find("land2");
        trees = GameObject.Find("trees");
        trees2 = GameObject.Find("trees2");
        cloud = GameObject.Find("cloud1");
        cloud2 = GameObject.Find("cloud2");
        cloud.transform.position = new Vector3(Random.value, 0);
        cloud2.transform.position = new Vector3(Random.value * 5, 0);
        land = GameObject.Find("land");
        GameObject.Find("jet-trail-1").transform.Translate(Random.value, 0, 0);
        
        //this disgusting line just flips it right or left
        GameObject.Find("jet-trail-1").transform.localScale += new Vector3(Mathf.Clamp(Random.Range(-1,1),-1, 1), 0, 0);

        GameObject.Find("jet-trail-2").transform.Translate(Random.value * 2, 0, 0);

        sr = GameObject.Find("bg").GetComponent<SpriteRenderer>();
        horizMin = sr.bounds.min.x;

        pwLines = GameObject.Find("power-lines");
        pwLines.transform.Translate(5 + Mathf.Abs(Random.value * 15), 0, 0);


        foreach (GameObject house in GameObject.FindGameObjectsWithTag("Wall"))
        {
            sr = house.GetComponent<SpriteRenderer>();          
            sr.transform.Translate(Random.value * 15, 0, 0);
        }
		StartCoroutine(Wait(destination));
    }

	IEnumerator Wait(string destination) {
		yield return new WaitForSeconds(4);
		sf.EndScene(pc.destination);
		pc.isTransition = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		destination = pc.destination;
        cloud.transform.Translate(-cloudSpeed, 0, 0);
        cloud2.transform.Translate(-cloudSpeed, 0, 0);
        land.transform.Translate(-landSpeed, 0, 0);
        land2.transform.Translate(-landSpeed, 0, 0);
        trees.transform.Translate(-treeSpeed, 0, 0);
        trees2.transform.Translate(-treeSpeed, 0, 0);
        pwLines.transform.Translate(-lineSpeed, 0, 0);

        sr = trees.GetComponent<SpriteRenderer>();
        if (sr.bounds.max.x < horizMin)
        {
            trees.transform.Translate(13 + Mathf.Abs(Random.value * 3), 0, 0);
        }

        sr = trees2.GetComponent<SpriteRenderer>();
        if (sr.bounds.max.x < horizMin)
        {
            trees2.transform.Translate(13 + Mathf.Abs(Random.value * 3), 0, 0);
        }

        //if one is off the screen move it up
        if (land.GetComponent<SpriteRenderer>().bounds.max.x < horizMin)
        {
            SpriteRenderer temp = land.GetComponent<SpriteRenderer>();
            float length = temp.bounds.max.x - temp.bounds.min.x;
            land.transform.Translate(2 * length - 0.002f, 0, 0);
        }

        if (land2.GetComponent<SpriteRenderer>().bounds.max.x < horizMin)
        {
            SpriteRenderer temp = land2.GetComponent<SpriteRenderer>();
            float length = temp.bounds.max.x - temp.bounds.min.x;
            land2.transform.Translate(2 * length - 0.002f, 0, 0);
        }

        sr = pwLines.GetComponent<SpriteRenderer>();
        if (sr.bounds.max.x < horizMin)
        {
            pwLines.transform.Translate(20 + Random.value * 15, 0, 0);
        }

        //reusing the Wall tag just for this scene
        foreach (GameObject house in GameObject.FindGameObjectsWithTag("Wall"))
        {
            house.transform.Translate(-houseSpeed, 0, 0);
            sr = house.GetComponent<SpriteRenderer>();
            if (sr.bounds.max.x < horizMin)
            {
                sr.transform.Translate(7 + Random.value * 15, 0, 0);
            }
        }
    }
}

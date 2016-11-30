///<summary>
///
/// Mom hugs Alex when he gets near enough.
/// She also places a Warm Hug in his inventory.
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class MomController : MonoBehaviour {

    public float hugDistance;
    GameObject player;
    Animator an;
    protected TextBoxController tc;

    bool hugged;
    bool triggered;
    protected Inventory inv;

    //travelling stuff
    TransitionController tr;

    // Use this for initialization
    void Start () {
        if (hugDistance == 0) hugDistance = .5f;
        player = GameObject.Find("Player");
        hugged = false;
        an = GetComponent<Animator>();
        tc = GameObject.Find("GameManager").GetComponent<TextBoxController>();
        inv = GameObject.Find("GameManager").GetComponent<Inventory>();
        triggered = false;
        tr = GameObject.Find("GameManager").GetComponent<TransitionController>();
    }

    // Update is called once per frame
    void Update () {
        //mom will always be to the right of the player, so no mathf.abs
	    if (gameObject.transform.position.x - player.transform.position.x < hugDistance && hugged == false)
        {
            an.SetTrigger("hug");
            hugged = true;
            StartCoroutine(hidePlayer());
            
        }

        //check to see if she's done talking
        if (gameObject.GetComponent<NPCController>().talked && !triggered)
        {
            Debug.Log("MOM's done talking");
            triggered = true;
            tr.Transition("partyoutside");
        }
	}

    IEnumerator hidePlayer()
    {
        SpriteRenderer spr = player.GetComponent<SpriteRenderer>();
        Color temp = spr.color;
        temp.a = 0;
        spr.color = temp;
        player.GetComponent<PlayerController>().isTalking = true;
        giveHug();
        //and then show him again after 1.3 seconds, the duration of the hug
        yield return new WaitForSeconds(1.3f);
        player.GetComponent<PlayerController>().isTalking = false;
        temp = spr.color;
        temp.a = 255;
        spr.color = temp;
    }

    void giveHug()
    {
        tc.add("MOM gives you a Warm Hug.");
        inv.ReceiveItem("Warm Hug");
    }
}

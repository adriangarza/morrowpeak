///<summary>
///
/// this makes vertical walls fade in and out when
/// the player walks behind them
/// 
/// it's not currently used, since the effect is 
/// actually a little weird in gameplay
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class ExtraWallController : MonoBehaviour {

    GameObject player;
    SpriteRenderer thisSprite;
    Vector3 wallPos;
    Vector3 playerPos;
    public float xDiff;
    public float yDiff;

    // Use this for initialization
    void Start () {
        wallPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        thisSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        A complicated function for pretty fading thresholds
        */
        playerPos = player.transform.position;
        xDiff = Mathf.Abs(playerPos.x - wallPos.x);
        yDiff = Mathf.Abs(playerPos.y - wallPos.y);

        if (xDiff < 1 && yDiff < 1)
        {
            Color tmp = thisSprite.color;
            tmp.a = xDiff;
            thisSprite.color = tmp;
        } else if (thisSprite.color.a < 255)
        {
            Color tmp = thisSprite.color;
            tmp.a = 255;
            thisSprite.color = tmp;
        }

	}
}

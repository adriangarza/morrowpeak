///<summary>
///
/// this controls every wall in a 2.5d scene
/// 
/// it fades them in or out depending on whether the player is
/// above or below them
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {
    GameObject player;
    float playerY;
    float playerBottom;
    SpriteRenderer[] sprites;
    SpriteRenderer sprite;
    SpriteRenderer thisSprite;
    float wallBottom;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        sprites = GetComponentsInChildren<SpriteRenderer>();
        thisSprite = GetComponent<SpriteRenderer>();
        wallBottom = GetComponent<SpriteRenderer>().bounds.min.y;
        //call this once so things aren't screwy

        float absDistance = (wallBottom - playerBottom);

        Color tmp = thisSprite.color;
        tmp.a = absDistance * 10;
        thisSprite.color = tmp;


        foreach (SpriteRenderer sprite in sprites)
        {
            tmp = sprite.color;
            tmp.a = absDistance * 10;
            sprite.color = tmp;
            if (wallBottom > playerBottom)
            {
                //sprite.sortingOrder = 5;
                sprite.sortingLayerName = "Behind";
            }
            else {
                //sprite.sortingOrder = 15;
                sprite.sortingLayerName = "Front";
            }
            
        }
    }

    // Update is called once per frame
    void Update () {
        playerBottom = player.GetComponent<SpriteRenderer>().bounds.min.y;
        float absDistance = (wallBottom - playerBottom);
        if (absDistance < 5)
        {
            Color tmp = thisSprite.color;
            tmp.a = absDistance * 10;
            thisSprite.color = tmp;


            foreach (SpriteRenderer sprite in sprites)
            {
                tmp = sprite.color;
                tmp.a = absDistance * 10;
                sprite.color = tmp;
                if (wallBottom > playerBottom)
                {
                    //sprite.sortingOrder = 5;
                    sprite.sortingLayerName = "Behind";
                }
                else {
                    //sprite.sortingOrder = 15;
                    sprite.sortingLayerName = "Front";
                }
            }
        }
    }
}

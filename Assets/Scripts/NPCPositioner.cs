///<summary>
///
/// Moves NPCs in front of or behind the player depending on 
/// whether he's above or below them 
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class NPCPositioner : MonoBehaviour {

    GameObject player;
    float playerY;
    GameObject[] npcList;
    GameObject[] objList;
    SpriteRenderer sprite;
    float playerBottom;
    float objBottom;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		npcList = GameObject.FindGameObjectsWithTag("NPC");
		objList = GameObject.FindGameObjectsWithTag("SceneObject");
	}
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        playerBottom = player.GetComponent<SpriteRenderer>().bounds.min.y;
        foreach (GameObject npc in npcList)
        {
            sprite = npc.GetComponent<SpriteRenderer>();
            objBottom = sprite.bounds.min.y;
            if (objBottom > playerBottom) {
                //sprite.sortingOrder = 5;
                sprite.sortingLayerName = "Behind";
            } else {
                //sprite.sortingOrder = 15;
                sprite.sortingLayerName = "Front";
            }
        }
        
        foreach (GameObject obj in objList)
        {
           sprite = obj.GetComponent<SpriteRenderer>();
           objBottom = sprite.bounds.min.y;
           if (objBottom > playerBottom) {
               //sprite.sortingOrder = 5;
               sprite.sortingLayerName = "Behind";
            } else {
               //sprite.sortingOrder = 15;
               sprite.sortingLayerName = "Front";
            }
        }
       

    }
}

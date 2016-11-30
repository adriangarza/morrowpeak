///<summary>
///
/// This is attached to every object, it 
/// has functions for the player interacting
/// with them and receiving items
/// 
/// </summary>


using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    public string message;
    public float distance;
    public string itemName;

    protected TextBoxController tc;
    protected GameObject player;
    protected PlayerController pc;
    protected Inventory inv;
    // Use this for initialization

	void Awake ()
	{
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		inv = GameObject.Find("GameManager").GetComponent<Inventory>();
		tc = GameObject.Find("GameManager").GetComponent<TextBoxController>();
	}
    void Start () {
        if (distance == 0) distance = .3f;
        
    }

    // Update is called once per frame
    void Update () {
		if (CheckDistance())
        {
            //you can't interact with invisible objects
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            bool visible = sprite.color.a > 0;
            if (Input.GetKeyDown(KeyCode.Space) && pc.isTalking == false && pc.isChecking == false && visible)
            {
                tc.add(message);

                if (itemName != "")
                {
					inv.ReceiveItem(this.name, itemName);
					if (inv.itemdb.getItemByName(itemName).essential) itemName = "";
                }
            } 
        }
    }

    public bool CheckDistance()
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

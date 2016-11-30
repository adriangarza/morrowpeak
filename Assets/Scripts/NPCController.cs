///<summary>
///
/// this script is attached to most NPCs in the game
/// 
/// it lets them interact with the player, and receive
/// quest items and random items, as well as have an
/// item that they'll give to the player
/// 
/// it also loads their dialogue from their text file
/// the line starting with ! is their quest item receive text
/// the line starting with ? is their random item receive text
/// they should both be at the start of the text file
/// 
/// they also have a "talked" boolean that is only true when the NPC
/// has talked once (bypassing single lines) and the render queue
/// is empty, so if something is set to load once they're finished 
/// it won't interrupt dialogue
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCController : ObjectController {
    //this is the one that uses the rendering queue
    public bool isTalking;

    public string wantedItem;
    public string receiveText;
    public bool receivedItem;
	public bool receivedWantedItem;

    private string standardReceiveText;
	private string essentialReceiveText;

    private string textPath;
    private int currentConversation;
	protected Item NPCItem;
    private string[] conversations;
	public float speed;
	public bool endLevel;

    private Animator anim;

    //if they've said all their dialogue
    public bool talked;
    private bool talkedOnce;
    public bool triggered;
    protected TextBoxController tc;

	protected void Start () {
        
		endLevel = false;

		if (distance == 0) distance = .5f;
		speed = .008f;

        //path to current folder text
        textPath = SceneManager.GetActiveScene().name + "/" + name;
        currentConversation = -1;
        //loads lines of conversations into an array
        conversations = createDialogue(loadText(textPath));

        //object seeker stuff here
        if (wantedItem == "") receivedItem = true;

        anim = gameObject.GetComponent<Animator>();

        talkedOnce = false;
        talked = false;
        triggered = false;
        tc = GameObject.Find("GameManager").GetComponent<TextBoxController>();
    }

    // Update is called once per frame
    protected void Update () {

		if (receivedWantedItem && tc.renderQueue.Count == 0 && endLevel)
		{
			Move();
		}
		if (CheckDistance() && GetComponent<SpriteRenderer>().color.a > 0)
		{
			if (Input.GetKeyDown(KeyCode.Space) && !pc.isTalking && !pc.isChecking && tc.renderQueue.Count == 0)
			{
                isTalking = true;
				if (receivedWantedItem)
				{
					tc.add(essentialReceiveText);
				}
				else
				{
                	Talk(); //we only want this to be called once now because it pushes all the dialogue at once
					if (itemName != "" && !inv.contains(itemName))
					{
						tc.add(inv.ReceiveItem(this.name, itemName));
						NPCItem = null;
                        if (!inv.itemdb.getItemByName(itemName).essential) itemName = "";
					}
				}
			}
		}

        //if they've said everything and the player has seen all their dialogue
        if (!talked &&
            currentConversation == conversations.Length - 1  && 
            tc.renderQueue.Count == 0 &&
            talkedOnce)
        {
            talked = true;
        }

    }

	#region Talk
	protected void Talk() 
	{
        //don't change these lines, the NPC.talked property depends on it
        currentConversation++;
        if (currentConversation > conversations.Length - 1) currentConversation--;

        talkedOnce = true;
		pc.isTalking = true;

        //turn the first conversation into an array of text lines to be displayed one at a time
        string[] conversation = conversations[currentConversation].Split('\n');
        tc.add(conversation);

    }
	#endregion
	
	public void Move()
	{
        anim.SetTrigger("walk down");
		GameObject door = GameObject.Find("Door");
		if (this.transform.position.x < door.transform.position.x)
		{
			transform.Translate(speed, -speed, 0);
		}
		else if (this.transform.position.y > door.transform.position.y)
		{
			transform.Translate(0, -speed, 0);
		}
		if (Vector2.Distance(this.transform.position, door.transform.position) < .3f)
		{
			ScreenFader sf = GameObject.Find("GameManager").GetComponent<ScreenFader>();
			sf.EndScene("party");
		}
	}

    //this should separate text file lines by a single line that says "---"
    string[] createDialogue(TextAsset textFile)
    {
        string[] temp = textFile.text.Split('\n');
        int startIndex = 0;
        for (int count = 0; count < temp.Length; count++)
        {
            //if the line begins with !, it's the standard object received dialogue
            //this means all the special lines have to be at the beginning
            if (temp[count][0] == '!')
            {
                //set it mane
                standardReceiveText = temp[count].Substring(1);
            }
			else if (temp[count][0] == '?')
			{
				essentialReceiveText = temp[count].Substring(1);
			}
            else {
                break;
            }
            startIndex += temp[count].Length;
        }

        if (startIndex != 0) startIndex++;
        
        return textFile.text.Substring(startIndex).Split(new string[] { "---" }, System.StringSplitOptions.None);
        
    }

    //load textfile, with error handling
    TextAsset loadText(string textPath)
    {
        TextAsset textFile = Resources.Load(textPath, typeof(TextAsset)) as TextAsset;
        if (textFile != null)
        {
                return textFile;
        }
        else
        {
            Debug.LogError("Failed to load text file: " + textPath);
            return null;
        }
    }

    public bool isSeeking(string itemName)
    {
        return itemName == wantedItem;
    }

	public void ReceiveItem(string itemName)
    {
		Item item = inv.itemdb.getItemByName(itemName);
		string giveText = "You give " + name + " " + item.article + " " + item.name + ".";
        if (isSeeking(item.name))
        {
			tc.add(giveText);
            tc.add(receiveText);
			receivedWantedItem = true;
            inv.RemoveItem(item);
        }
        else if (!item.essential)
        {
            if (standardReceiveText != "" && standardReceiveText != null)
            {
				tc.add(giveText);
                tc.add(standardReceiveText);
                inv.RemoveItem(item);
            } else
            {
				tc.add(giveText);
                inv.RemoveItem(item);
            }
        } else
        {
            tc.add("You decide you probably need the " + item.name + ".");
        }
    }

}

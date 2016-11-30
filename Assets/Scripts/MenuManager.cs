///<summary>
///
/// this script shows and navigates the menus,
/// including inventory, locations, and quit
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script used to display the menu
//Currently, the menu is just inventory

public class MenuManager : MonoBehaviour {

	public GameObject menuBox;
	public GameObject menu;
	public Text theText;
	public Inventory inv;
	GameObject player;
	PlayerController pc;
	TextBoxController textController;
	GameObject map;
    int currentItemNum;
	string currentItem;
	int currentOptionNum;
    int numItems;
    public string[] options;
	public string[] locations;
	int currentLocationNum;
    bool selected;

	void Awake ()
	{
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		inv = GetComponent<Inventory>();
		menuBox = GameObject.Find("MenuBox");
		menu = GameObject.Find("Menu");
		textController = GetComponent<TextBoxController>();
		menu.GetComponent<Text>().font = Resources.Load("Fonts/thefont") as Font;
		map = GameObject.Find("Map");
	}

	void Start () {
		numItems = inv.itemList.Count;
		options = new string[3]{"Use", "Give", "Drop"};
		menuBox.SetActive(false);
	}
	
	//player presses I to toggle menu on & off
	void Update () {
		#region TOGGLE MENU WITH I
		if (Input.GetKeyDown(KeyCode.I))
        {
			if (!menuBox.activeSelf && !pc.frozen)
            {
                menuBox.SetActive(true);
				numItems = inv.itemList.Count;
                DisplayInventory();
                pc.isChecking = true;
            }
            else
            {
                menuBox.SetActive(false);
                pc.isChecking = false;
            }
        }
		#endregion

		if (menuBox.activeSelf)
		{
			currentItem = inv.itemList[currentItemNum].name;
			#region NAVIGATE MENU WITH ARROW KEYS
	        //moving down through list or options
	        //these if statements are to keep it within the bounds of the list
	        if (Input.GetKeyDown(KeyCode.DownArrow))
	        {
	            if (currentItemNum < numItems - 1 && !selected)
	            {
	                currentItemNum++;
	                DisplayInventory();
	            }
	            else if (currentOptionNum < options.Length - 1 && selected)
	            {
	                currentOptionNum++;
						DisplayOptions(currentItem);
	            }
	        }
	        //moving up through list or options
	        //again, same thing
	        else if (Input.GetKeyDown(KeyCode.UpArrow))
	        {
	            if (currentItemNum > 0 && !selected)
	            {
	                currentItemNum--;
	                DisplayInventory();

	            } else if (currentOptionNum > 0 && selected)
	            {
	                currentOptionNum--;
					DisplayOptions(currentItem);
	            }
	        } 
			#endregion
			//selecting an item and displaying options
			if (Input.GetKeyDown(KeyCode.Space))
	        {
				#region NAVIGATE OPTIONS
				currentItem = inv.itemList[currentItemNum].name;
	            //making sure the box isn't already in "options" mode
	            if (!selected) 
					DisplayOptions(currentItem);
	            else
	            {
					//making sure only one message is in the queue at a time
					if (textController.renderQueue.Count > 0)
					{
						textController.renderQueue.Dequeue();
					}
					if (currentOptionNum == 0) //the "use" option
					{
						textController.add(inv.UseItem(currentItem));
		            }
					else if (currentOptionNum == 1) //the "give" option
		            {
						textController.add(inv.GiveItem(currentItem));
		            }
					else if (currentOptionNum == 2) //the "drop" option
					{
						textController.add(inv.TossItem(currentItem));
					}
					if (inv.itemList.Count != numItems)
					{
						ResetInventory();
					}
				}
				#endregion
	        }
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Escape))
            //go back to the main inventory page
	        {
	            DisplayInventory();
	        }
		}
    }
		
	//gets list of items from inventory gameobject
	//prints each item on a new line
	void DisplayInventory()
	{
        currentOptionNum = 0; 
        selected = false;
		theText.text = "";
		for (int i = 0; i < numItems; i++)
		{
            Item temp = inv.itemList[i];
            if (currentItemNum == i)
            {
                theText.text += "> " + temp.name + '\n';
            } 
			else
            {
                theText.text += temp.name + '\n';
            }
		}
	}

    //same as display inventory 
    void DisplayOptions(string itemName)
    {
        selected = true;
        theText.text = itemName + "\n\n";
        for (int i=0; i < options.Length; i++)
        {
            if (currentOptionNum == i)
            {
                theText.text += "> " + options[i] + '\n';
            } else
            {
                theText.text += options[i] + '\n';
            }
        }
    }

	void ResetInventory() 
	{
		if (currentItemNum > 0)
			currentItemNum--;
		currentOptionNum = 0;
		numItems = inv.itemList.Count;
		DisplayInventory();
	}
}

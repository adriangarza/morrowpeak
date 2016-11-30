///<summary>
///
/// This is the frontend of the inventory system
/// 
/// it works with the inventoryDB as a backend, and
/// is the program sending the list to the UI system
/// 
/// it also keeps track of the player's current inventory,
/// since inventoryDB is just a list of everything in the game
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour{

	public List<Item> itemList;
	string[] textItems;
	public bool isFull;
	string text;
	string path;
    public bool updateFile;

    public InventoryDB itemdb;

	public void Awake ()
	{
		itemdb = GetComponent<InventoryDB>();
        TextAsset tempAsset = Resources.Load("global/INVENTORY", typeof(TextAsset)) as TextAsset;
        text = tempAsset.text;

		itemList = new List<Item>();
	}

	public void Start () 
	{
        //load the current player inventory
		isFull = false;
        path = "/Resources/global/INVENTORY.txt";
		textItems = text.Split('\n');
        itemdb.Init();
        foreach (string item in textItems)
        {
            if (item != "")
            {
                Item tempItem = itemdb.getItemByName(item);
                itemList.Add(tempItem);
            }
        }
    }

    public bool contains(string itemName)
    {
		bool temp = false;
        foreach (Item item in itemList)
        {
            if (item.name == itemName)
            {
				temp = true;
            }
        }
        return temp;
    }

	public void CheckIfFull ()
	{
		bool temp = itemList.Count >= 12;
		isFull = temp;
	}

	/*
	void SwitchItems(int a, int b)
	{
		Item temp = itemList[a];
		itemList[a] = itemList[b];
		itemList[b] = temp;
	}
	*/

    //dealing with the new item text file
	public string ReceiveItem(string name, string itemName)
    {
        Item item = itemdb.getItemByName(itemName);
		string temp = "";
		if (isFull)
		{
			temp = "You have too many items.";
		}
		else if (!itemList.Contains(item))
		{
			temp = name + " gives you " + item.article + " " + itemName + ".";
			itemList.Add(item);
			UpdateTextFile();
		}
		return temp;
    }

    public string ReceiveItem(string itemName)
    {
        Item item = itemdb.getItemByName(itemName);
        string temp = "";
        if (isFull)
        {
            temp = "You have too many items.";
        }
        else if (!itemList.Contains(item))
        {
            temp = name + " gives you " + item.article + " " + itemName + ".";
            itemList.Add(item);
            UpdateTextFile();
        }
        return temp;
    }

    public string GiveItem(string itemName)
	{
		string temp = "";
		bool npcFound = false;
		foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
		{
			if (npc.GetComponent<NPCController>().CheckDistance())
			{
				npcFound = true;
				npc.GetComponent<NPCController>().ReceiveItem(itemName);
				break;
			}
		}
		if (!npcFound)
			temp = "To whom?";
		return temp;
	}

	public string UseItem(string itemName)
	{
		Item item = itemdb.getItemByName(itemName);
		string temp = "";
		if (!item.usable)
			temp = "You can't use that right now.";
		else {
			temp = item.useText;
			if (!item.essential)
			{
				RemoveItem(item);
			}
		}
		return temp;
	}

	public string TossItem(string itemName)
	{
		Item item = itemdb.getItemByName(itemName);
		string temp = "";
		if (item.essential)
		{
			temp = "You can't throw that away.";
		}
		else
		{
			temp = "You throw away the " + item.name + ".";
			if (!item.essential)
			{
				RemoveItem(item);
			}
		}
		return temp;
	}

    public void RemoveItem(Item item)
    {   
		itemList.Remove(item);
		UpdateTextFile();
    }

	public void UpdateTextFile()
	{
		if (!updateFile) {} 
        else 
		{
			File.WriteAllText(Application.dataPath + path, "");
			StreamWriter writer = new StreamWriter(Application.dataPath + path);
        	foreach (Item item in itemList)
			{
            	writer.WriteLine(item.name);
        	}
			writer.Close();
		}
	}
	#region debugging
	public void DebugTextFile()
	{
		StreamReader reader = new StreamReader(Application.dataPath + path);
		text = reader.ReadToEnd();
		reader.Close();
		textItems = text.Split('\n');
		foreach (string item in textItems)
		{
			if (item != "")
			{
				Debug.Log(item);
			}
		}
	}

	public void DebugItemList()
	{
		foreach (Item item in itemList)
		{
			Debug.Log(item.name);
		}
	}
	#endregion
}
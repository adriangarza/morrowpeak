///<summary>
///
/// this is the global list of every item in the game,
/// and its properties (usable, givable, essential, etc)
/// 
/// when the game loads, it reads everything into memory
/// which will then be used by Inventory.cs
/// 
/// most of the weird code here is for sanitizing the text
/// file with like ten different types of newline
/// 
/// </summary>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class InventoryDB : MonoBehaviour {

    public string path;

    //using an array here because we're not modifying it
    public Item[] itemList;

	// Use this for initialization
	public void Init () {
        //load the file
        path = "global/ITEMS";
        TextAsset itemFile = Resources.Load(path, typeof(TextAsset)) as TextAsset;
        if (itemFile == null)
        {
            Debug.LogError("Failed to load item database file!\n" + path);
        }

        //allocate array space
        string[] rawData = itemFile.text.Split('\n');
        itemList = new Item[rawData.Length];

        //parse the file into the item array
        string[] properties = new string[4];
        for (int i = 0; i < rawData.Length; i++)
        {
            properties = rawData[i].Split('/');
            Item tempItem;

            //accounting for whether the item is usable and has useText 
            if (properties.Length == 4)
            {
                tempItem = new Item(properties[0], bool.Parse(properties[1]), bool.Parse(properties[2]), properties[3]);
            } else if (properties.Length == 3)
            {
                tempItem = new Item(properties[0], bool.Parse(properties[1]), bool.Parse(properties[2]));
            } else
            {
                Debug.LogError("Invalid item properties for " + properties[0] + "!");
                break;
            }
            itemList[i] = tempItem;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    public Item getItemByName(string itemName)
    {
        //sanitize the god damn input holy shit
        string sanitizedName = new string(itemName.Where(c => !char.IsControl(c)).ToArray());

        foreach (Item it in itemList)
        {
            if (it.name == sanitizedName) return it;
        }
        return null;
    }
}

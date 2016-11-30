using UnityEngine;
using System.Collections;

public class Item {

	public string name;
	public string article;
	public bool usable;
    public bool essential;
    public string useText;
	string vowels = "AEIOUaeiou";

	void Start () {
	}

	void Update () {
	}
    
    //constructors
    public Item(string name, bool usable, bool essential)
    {
        this.name = name;
        this.usable = usable;
        this.essential = essential;
		SetArticle();
	}

    public Item(string name, bool usable, bool essential, string useText)
    {
        this.name = name;
        this.usable = usable;
        this.essential = essential;
        this.useText = useText;
		SetArticle();
    }

	public void SetArticle()
	{
		if (this.essential) 					            article = "the";
		else if (vowels.Contains(name.Substring(0, 1))) 	article = "an";
		else 									            article = "a";
	}

}

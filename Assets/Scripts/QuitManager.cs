using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script used to display the menu
//Currently, the menu is just inventory

public class QuitManager : MonoBehaviour {

	public GameObject quitBox;
	public GameObject message;
	public Text theText;
	GameObject player;
	PlayerController pc;
	TextBoxController textController;
	ScreenFader sf;
	int currentOptionNum;
	public string[] options;

	void Awake ()
	{
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		sf = GameObject.Find("GameManager").GetComponent<ScreenFader>();
		quitBox = GameObject.Find("QuitBox");
		message = GameObject.Find("Message");
		textController = GetComponent<TextBoxController>();
		message.GetComponent<Text>().font = Resources.Load("Fonts/thefont") as Font;
	}

	void Start () {
		options = new string[2]{"Yes", "No"};
		quitBox.SetActive(false);
	}

	//player presses I to toggle menu on & off
	void Update () {
		#region TOGGLE MENU WITH ESCAPE
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!quitBox.activeSelf && !pc.frozen)
			{
				Open();
			}
			else
			{
				Close();
			}
		}
		#endregion

		if (quitBox.activeSelf)
		{
			DisplayOptions();
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				currentOptionNum = 1;
			}
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				currentOptionNum = 0;
			} 
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (currentOptionNum == 1)
				{
					Close();	
				}
				else
				{
					Quit();
				}
			}
		}
	}

	void Quit()
	{
		Close();
		sf.EndScene("mainmenu");
	}

	//same as display inventory 
	void DisplayOptions()
	{
		theText.text = "Would you like to quit?" + "\n\n";
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

	void Open()
	{
		currentOptionNum = 0;
		quitBox.SetActive(true);
		DisplayOptions();
		pc.isQuitting = true;
	}

	public void Close()
	{
		quitBox.SetActive(false);
		pc.isQuitting = false;
	}
}

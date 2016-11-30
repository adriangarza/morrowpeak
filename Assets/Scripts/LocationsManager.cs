using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocationsManager : MonoBehaviour {

	public GameObject locMenu;
	public GameObject locText;
	GameManager gm;
	public Text theText;
	GameObject player;
	PlayerController pc;
	GameObject map;
	TransitionController tc;
	public string[] locations;
	int currentLocationNum;

	void Awake ()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = GameObject.Find("Player");
		pc = player.GetComponent<PlayerController>();
		tc = GetComponent<TransitionController>();
		locMenu = GameObject.Find("LocationsMenu");
		locText = GameObject.Find("LocationsText");
		locText.GetComponent<Text>().font = Resources.Load("Fonts/thefont") as Font;
		map = GameObject.Find("Map");
	}
	// Use this for initialization
	void Start () {
		locations = new string[5]{"airport", "coffeeshop", "bridge", "partyoutside", "cancel"};
		locMenu.SetActive(false);
		currentLocationNum = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		if (locMenu.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				if (currentLocationNum < locations.Length - 1)
				{
					currentLocationNum++;
					if (locations[currentLocationNum].Equals(gm.currentSceneName))
						currentLocationNum++;
					DisplayLocations();
				}
			}
			else if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (currentLocationNum == 1 && locations[0].Equals(gm.currentSceneName))
				{}
				else if (currentLocationNum > 0)
				{
					currentLocationNum--;
					if (locations[currentLocationNum].Equals(gm.currentSceneName))
						currentLocationNum--;
					DisplayLocations();
				}
			} 
			//selecting an item and displaying options
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (currentLocationNum == locations.Length - 1)
				{
					Close();
					Cancel();
				}
				else
				{
					tc.Transition(locations[currentLocationNum]);
					Close();
				}
			}
		}
	}

	public void Open()
	{
		pc.isMapping = true;
		currentLocationNum = 0; 
		if (locations[currentLocationNum].Equals(gm.currentSceneName))
			currentLocationNum++;
		locMenu.SetActive(true);
		DisplayLocations();
	}
	void Close()
	{
		pc.isMapping = false;
		locMenu.SetActive(false);
		map.SetActive(false);
	}

	void Cancel()
	{
		CarController cc = GameObject.Find("Car").GetComponent<CarController>();
		cc.GetOut();
	}

	void DisplayLocations()
	{
		theText.text = "";
		for (int i = 0; i < locations.Length; i++)
		{
			if (locations[i].Equals(gm.currentSceneName))
			{
			}
			else
			{
				if (currentLocationNum == i)
				{
					theText.text += "> " + locations[i] + '\n';
				} 
				else
				{
					theText.text += locations[i] + '\n';
				}
			}
		}
	}
}

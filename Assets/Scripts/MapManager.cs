using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour {

	PlayerController pc;
	GameObject map;
	LocationsManager lm;

	void Awake() 
	{
		pc = GameObject.Find("Player").GetComponent<PlayerController>();
		map = GameObject.Find("Map");
		lm = GetComponent<LocationsManager>();
	}
	void Start () {
		//mapImage.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		map.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M) && !pc.isCar)
		{
			if (!map.activeSelf && !pc.frozen)
			{
				Open();
			}
			else
			{
				Close();
			}
		}
	}

	void Open()
	{
		pc.isMapping = true;
		map.SetActive(true);
	}
	void Close()
	{
		map.SetActive(false);
		pc.isMapping = false;
	}
	IEnumerator OpenMap()
	{
		yield return new WaitForSeconds(1);
		Open();
		lm.Open();
	}
}

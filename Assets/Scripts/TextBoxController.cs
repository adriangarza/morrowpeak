using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextBoxController : MonoBehaviour {

    #region fields

    public GameObject textBox;
    public Text theText;

    public Queue<string> renderQueue;
    GameObject player;
    PlayerController pc;
    #endregion

    // Use this for initialization
    void Start () {
		GameObject.Find("Dialogue").GetComponent<Text>().font = Resources.Load("Fonts/thefont") as Font;
        textBox.SetActive(false);
        renderQueue = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	    //as long as the queue isn't empty, always render the top element
        //if the player presses enter, pop the first element, or close the text box if it's null
        if (renderQueue.Count > 0)
        {
            pc.isTalking = true;

            //scan the next line to see if it's printable
            if (checkPrint(renderQueue.Peek()) == false)
            {
                renderQueue.Dequeue();
            }

            //account for potential dequeues happening above
            if (renderQueue.Count > 0) Render(renderQueue.Peek());

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                renderQueue.Dequeue();
            }
        } else
        {
            Close();
            pc.isTalking = false;
        }
	}

    //public render function
    public void Render(string inputText)
    {
        this.Open();
        theText.text = inputText;
    }

    public void Close()
    {
        textBox.SetActive(false);
    }

    public void Open()
    {
        textBox.SetActive(true);
    }

    public void add(string text)
    {
        renderQueue.Enqueue(text);
    }

    public void add(string[] textarr)
    {
        foreach (string str in textarr)
        {
            renderQueue.Enqueue(str);
        }
    }

    bool checkPrint(string line)
    {
        foreach (char c in line)
        {
            if (!char.IsControl(c))
            {
                return true;
            }
        }
        return false;
    }

}

using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

    Animator anim;
    GameObject player;
    public float threshold;

    bool played;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < threshold)
        {
            anim.SetTrigger("playerNear");
            if (!played)
            {
                audioSource.Play();
                played = true;
            }
        }
	}
}

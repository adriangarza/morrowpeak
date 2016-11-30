using UnityEngine;
using System.Collections;

/*
 * Control's player's movement and interaction states.
 */

[RequireComponent (typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour 
{
	#region fields
	Animator anim;

	public float speed;
	public bool isTalking;
	public bool isChecking;
	public bool isMapping;
	public bool isQuitting;
	public bool isCar;
	public bool isTransition;
	public bool frozen;
	public Vector3 direction; 
	public bool facingRight = true;
	public bool topdown; // If scene is topdown, player's vertical movement is not restricted.
	public string destination; // Where player is headed; necessary for transitions.

	#endregion

	void Awake ()
	{
		anim = GetComponent<Animator>();
	}

	#region start
	void Start () 
	{
		speed = .02f;
		isTalking = false;
		isChecking = false;
		isQuitting = false;
		isMapping = false;
		isCar = false;
		direction = new Vector3(0.0f, 0.0f, 0.0f);
        topdown = (GameObject.Find("TOPDOWN") != null);	
	}
	#endregion

	#region update
	void FixedUpdate () 
	{
		frozen = isChecking || isTalking || isMapping || isQuitting || isCar || isTransition;
        //detecting movement
        if (direction != Vector3.zero && !isTalking) {
            anim.SetBool("stationary", false);
        } else {
            anim.SetBool("stationary", true);
        }
		direction = new Vector3(Mathf.Clamp(0.0f, -1.0f, 1.0f), Mathf.Clamp(0.0f, -1.0f, 1.0f), Mathf.Clamp(0.0f, -1.0f, 1.0f));
		#region movement
		if (Input.GetKey(KeyCode.LeftArrow))
		{
            anim.SetBool("walksideways", true);
			transform.Translate(-speed, 0, 0);
			direction += new Vector3(-1, 0, 0);
			if (facingRight && !frozen) {
				Flip();
			}
		}         
		else if (Input.GetKey(KeyCode.RightArrow))
		{
            anim.SetBool("walksideways", true);
			transform.Translate(speed, 0, 0);
			direction += new Vector3(1, 0, 0);
			if (!facingRight && !frozen) {
				Flip();
			}
		} else anim.SetBool("walksideways", false);	
        
		if (Input.GetKey(KeyCode.UpArrow) && topdown)
		{
			anim.SetBool("walkup", true);           
			transform.Translate(0, speed, 0);
			direction += new Vector3(0, 1, 0);
		} else anim.SetBool("walkup", false);
        
		if (Input.GetKey(KeyCode.DownArrow) && topdown)
		{
			anim.SetBool("walkdown", true);
			transform.Translate(0, -speed, 0);
			direction += new Vector3(0, -1, 0);
		} else anim.SetBool("walkdown", false);
        
		#endregion
		#region stop movement if talking
		if (frozen)
		{
			speed = 0;
            anim.SetBool("frozen", true);
		}
		else 
		{
			speed = 0.02f;
            anim.SetBool("frozen", false);
		}
		#endregion
        
	}
	#endregion

	#region GetDirection
	Vector3 GetDirection ()
	{
		return direction;
	}
	#endregion
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//flip by scaling -1
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}

}
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody2D rb;
	private GameObject levelController;
	private bool waitInput;
	private bool moving;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody2D> ();
		levelController = GameObject.Find ("LevelController");
		//StartMovement (5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			ReadInput ();
		}
	}
	void ReadInput()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow) && Input.GetKeyDown (KeyCode.RightArrow)) {
			levelController.SendMessage("PlayerInput", 0);
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if(Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.LeftArrow)))
			   {
				levelController.SendMessage("PlayerInput", 0);
			}
			else {
				levelController.SendMessage("PlayerInput", 3);
			}
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if(Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.LeftArrow)))
			{
				levelController.SendMessage("PlayerInput", 0);
			}
			else {
				levelController.SendMessage("PlayerInput", 1);
			}
		}
	}
	public void StartMovement(float speed)
	{
		moving = true;
		rb.velocity = Vector2.right * speed;
	}

	public void StopMovement()
	{
		rb.velocity = Vector2.zero;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		levelController.SendMessage ("UpdateMark", other.tag);
	}

	void OnTriggerExit2D(Collider2D other)
	{

		//Debug.Log (other.tag);
		levelController.SendMessage ("OutOfMark");
	}


}

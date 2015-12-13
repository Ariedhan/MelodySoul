using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody2D rb;
	private GameObject levelController;
	private bool waitInput;
	private bool moving;

	public enum markType{
		attack, jump, dodge, end, start
		
	}

	markType currentMark;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody2D> ();
		levelController = GameObject.Find ("LevelController");
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
		if (other.tag == "Jump") {
			currrentMark = markType.jump;
		} else if (other.tag == "Attack") {
			currrentMark = markType.attack;
		} else if (other.tag == "Dodge") {
			currrentMark = markType.dodge;
		} 
		levelController.SendMessage ("UpdateMark", other.tag);

	}

	void OnTriggerExit2D(Collider2D other)
	{

		levelController.SendMessage ("OutOfMark");
	}

	void Sucesss()
	{
		if (currentMark == markType.jump) {
		} else if (currentMark == markType.dodge) {
		} else if (currentMark == markType.attack) {
		
		}

	}

	void Fail()
	{

	}

}

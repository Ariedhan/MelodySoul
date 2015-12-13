﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody2D rb;
	private GameObject levelController;
	private bool waitInput;
	private bool moving;
	public float waitAfterJump;
	private float currentVelocity;
	public AnimationCurve jumpCurve;

	public enum markType{
		attack, jump, dodge, end, start
		
	}

	private markType currentMark;

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
		currentVelocity = speed;
		rb.velocity = Vector2.right * speed;
	}

	public void StopMovement()
	{
		rb.velocity = Vector2.zero;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Jump") {
			currentMark = markType.jump;
		} else if (other.tag == "Attack") {
			currentMark = markType.attack;
		} else if (other.tag == "Dodge") {
			currentMark = markType.dodge;
		} 
		levelController.SendMessage ("UpdateMark", other.tag);

	}

	void OnTriggerExit2D(Collider2D other)
	{

		levelController.SendMessage ("OutOfMark");
	}

	IEnumerator Jump (float animationTime) {
		float elapsedTime = 0;
		float initY = transform.position.y;
		while (elapsedTime < animationTime) {
			transform.position = new Vector3(
				transform.position.x,
				Mathf.Lerp(initY, initY + 2, jumpCurve.Evaluate(elapsedTime/animationTime)),
				transform.position.z);
			elapsedTime += Time.deltaTime;
			yield return 0;
		}
		rb.velocity = Vector2.zero;
		transform.position = new Vector3 (transform.position.x, initY, transform.position.z);
		StartCoroutine ("ContinueAfterJump", waitAfterJump);
	}

	IEnumerator Dodge()
	{
		yield return 0;

	}

	IEnumerator Attack()
	{
		yield return 0;
	}

	public void Success()
	{
		Debug.Log ("success");
		if (currentMark == markType.jump) {
			StartCoroutine(Jump(1.75f));

		} else if (currentMark == markType.dodge) {
			StartCoroutine (Dodge());
		} else if (currentMark == markType.attack) {
			StartCoroutine (Attack());
		
		}

	}

	IEnumerator ContinueAfterJump(float time)
	{
		yield return new WaitForSeconds (time);
		rb.velocity = Vector2.right * currentVelocity;

	}

	void Fail()
	{

	}

}

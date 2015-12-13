using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody2D rb;
	private GameObject levelController;

	// Use this for initialization
	void Start () {

		rb = this.GetComponent<Rigidbody2D> ();
		levelController = GameObject.Find ("LevelController");
		//StartMovement (5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartMovement()
	{
		rb.velocity = Vector2.right * 5f;
	}

	public void LevelFailed()
	{
		levelController.SendMessage ("Failed");
	}


}

using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private GameObject player;
	private GameObject ghost;


	// Use this for initialization
	void Awake () {

		player = GameObject.Find ("Player");
		ghost= GameObject.Find ("Ghost");

		StartCoroutine ("StartGhost");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartGhost()
	{
		yield return new WaitForSeconds (1.0f);
		Debug.Log ("hello");
		ghost.SendMessage ("StartMovement");
	}

	public void Failed()
	{
	}

	void GhostTurn()
	{

	}

	public void GhostEnd()
	{
		Debug.Log ("GhostEnd");
	}

	public void PlayerEnd()
	{
	}

	void PlayerTurn()
	{

	}

	void LevelSuccess()
	{

	}

	void RepeatGhost()
	{

	}
}

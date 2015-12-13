using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private GameObject player;
	private GameObject ghost;
	private GameObject cam;
	private bool waitForInput;
	public enum markType{
		attack, jump, dodge, end, start
			
	}

	private AudioSource audioSource;
	public float levelSpeed;
	public float ghostWaitTime;
	private markType currrentMark;
	private markType playerMark;
	private bool ghostTurn;

	private GameObject fader;

	// Use this for initialization
	void Awake () {
		audioSource = this.GetComponent<AudioSource> ();
		player = GameObject.Find ("Player");
		ghost= GameObject.Find ("Ghost");
		cam = GameObject.Find ("Main Camera");
		fader = GameObject.Find ("Fader");
		StartCoroutine ("StartGhost");

		ghost.SendMessage ("changeWaitTime", ghostWaitTime);

	
	}
	
	// Update is called once per frame
	void Update () {
		if (ghostTurn) {

			if (Input.GetKeyDown (KeyCode.LeftArrow) && Input.GetKeyDown (KeyCode.RightArrow)) {
				Debug.Log("WaitGhos");
				ghost.SendMessage ("SkipGhost");

			}
		}
	
	}

	IEnumerator StartGhost()
	{
		ghostTurn = true;
		yield return new WaitForSeconds (1.0f);
		Debug.Log ("hello");
		ghost.SendMessage ("StartMovement",levelSpeed);
		player.SendMessage ("StartMovement", levelSpeed);
		cam.SendMessage ("ChangeToFollow", ghost);
	}

	public void Failed()
	{

	}


	public void GhostEnd()
	{
		ghostTurn = false;
		StartCoroutine ("WaitCamToPlayer");


	}
	
	public void PlayerInput(markType input)
	{
		if (waitForInput) {

			playerMark=input;
			CompareInput();
		} else {
			Debug.Log("Failed No Input");
			Failed ();
		}
	}

	public void UpdateMark(string tag)
	{
		waitForInput = true;
		Debug.Log ("Waiting");
		if (tag == "Jump") {
			currrentMark = markType.jump;

		} else if (tag == "Attack") {
			currrentMark = markType.attack;
		} else if (tag == "Dodge") {
			currrentMark = markType.dodge;
		} else if (tag == "EndMark") {
			LevelSuccess();
			Debug.Log("Success");
		}
	}

	private void CompareInput()
	{
		Debug.Log(playerMark + " " + currrentMark + "sdfsfsad");
		if (playerMark == currrentMark) {
			Debug.Log("exito");
			//Player Play Animation
			player.SendMessage("Success");
			waitForInput=false;
		}
		else
			{
			Failed ();
			waitForInput=false;
			}
	}


	void LevelSuccess()
	{
		player.SendMessage ("StopMovement");
		StartCoroutine ("EndLevel");
	}

	IEnumerator EndLevel()
	{
		yield return new WaitForSeconds(1.0f);
		fader.SendMessage("CrossFade", false);

	}

	IEnumerator RestartLevel()
	{
		yield return new WaitForSeconds(1.0f);
		//SceneManager.LoadScene(""

	}


	IEnumerator WaitCamToPlayer()
	{
		yield return new WaitForSeconds(0.7f);

		cam.SendMessage ("ChangeToGoTo",  new Vector3(player.transform.position.x, 0f,-10f));
		StartCoroutine ("WaitPlayerStart");
	}

	IEnumerator WaitPlayerStart()
	{

		yield return new WaitForSeconds(1.0f);
		audioSource.Play ();
		cam.SendMessage ("ChangeToFollow",player);
		player.SendMessage ("StartMovement",levelSpeed);
	}

	public void OutOfMark()
	{
		Failed ();
		Debug.Log("Failed out of mark");
	}
}

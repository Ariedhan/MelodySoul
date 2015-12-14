using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager2 : MonoBehaviour 
{
	enum levelStates
	{
		init,
		ghostTurn,
		playerTurn,
		finish,
		waiting
	}
	levelStates state;

	public bool f,action;
	float mtime;
	public int nivel;
	MarkType.markType markAction;
	private InputController inputControl;
	GhostController ghost;
	CameraMovement cam;
	public float levelSpeed,ghostWaitTime;
	GameObject player;
	private AudioSource audioSource;
	private fader fade,texto;
	private GameObject gobj;
	private GameObject current;
	public AudioClip final;

	// Use this for initialization
	void Awake () 
	{
		state = levelStates.init;
		inputControl = GetComponent<InputController> ();
		ghost = GameObject.Find ("Ghost").GetComponent<GhostController> ();
		cam = GameObject.Find ("Main Camera").GetComponent<CameraMovement> ();
		player = GameObject.Find ("Player");
		audioSource = this.GetComponent<AudioSource> (); 

		fade = GameObject.Find("Fader").GetComponent<fader>();
		StartCoroutine (InitCoroutine());
		ghost.SendMessage ("changeWaitTime", ghostWaitTime);
		f = false;
		fade.gameObject.SetActive(true);
		mtime = 0;
		action = false;
	}

	public void CurrentMark(MarkType.markType mType, GameObject obj)
	{
		markAction = mType;
		current = obj;
	}

	IEnumerator InitCoroutine()
	{		
		yield return 0;
		fade.CrossFade(true);			
		yield return new WaitForSeconds (1.0f);
		state = levelStates.ghostTurn;
		StartCoroutine (GhostTurnCoroutine());
 
	}

	IEnumerator GhostTurnCoroutine()
	{
		//yield return new WaitForSeconds (1.0f);
		ghost.SendMessage ("StartMovement",levelSpeed);
		cam.SendMessage ("ChangeToFollow", ghost.gameObject);
		yield return 0;
	}

	IEnumerator PlayerTurnCoroutine()
	{
		player.SendMessage ("ChangeToMoving",levelSpeed);
		yield return 0;
	}

	IEnumerator FinishCoroutine()
	{
		//fadeOut sound
		yield return new WaitForSeconds(1.0f);
		fade.CrossFade(false);

		yield return new WaitForSeconds(1.0f); 
		if (nivel < SceneManager.sceneCountInBuildSettings - 1) {
			nivel++;
			SceneManager.LoadScene (nivel); 
		}
	}

	IEnumerator GhostEndCoroutine()
	{
		yield return new WaitForSeconds(0.7f);

		cam.SendMessage ("ChangeToGoTo",  new Vector3(player.transform.position.x, 0f,-10f));

		yield return new WaitForSeconds(1.0f);
		audioSource.Play ();
		cam.SendMessage ("ChangeToFollow",player);

		state = levelStates.playerTurn;
		StartCoroutine (PlayerTurnCoroutine());
	}

	public void GhostEnd()
	{
		state = levelStates.waiting;
		StartCoroutine (GhostEndCoroutine());
	}

	public void LevelSuccess()
	{
		state = levelStates.finish;
		player.SendMessage ("ChangeToWaiting");
		StartCoroutine (FinishCoroutine ());
	}

	void Update () {
		if (state == levelStates.ghostTurn) 
		{
			if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.RightArrow)) 
			{
				ghost.SendMessage ("SkipGhost");
			}
		}	
 

		if (state == levelStates.playerTurn) 
		{
			if (!f) 
			{
				if ((inputControl.inp != InputController.inputState.none))
				{
					player.SendMessage ("Death");
					StartCoroutine (Restart());
				} 
			} 
			else 
			{
				if ((inputControl.inp != InputController.inputState.none) && (mtime > 0.2f)) 
				{
					mtime = 0;
					Debug.Log ("timer > 0.2f");
					if ((inputControl.inp == InputController.inputState.keyRight) && (markAction == MarkType.markType.jump)) {
						player.SendMessage ("ChangeToAction", markAction);
						action = true;

						//accion de saltar
					} else if ((inputControl.inp == InputController.inputState.keyLeft) && (markAction == MarkType.markType.dodge)) {
						player.SendMessage ("ChangeToAction", markAction);
						action = true;
						//accion de dodge
					} else if ((inputControl.inp == InputController.inputState.keyBoth) && (markAction == MarkType.markType.attack)) {
						player.SendMessage ("ChangeToAction", markAction);
						action = true;
						//accion de atacar
					} else {
						player.SendMessage ("Death");
						StartCoroutine (Restart ());
					}
				} 
				else 
				{ 
					mtime += Time.deltaTime;
				}
			}
		}
	}

	IEnumerator Restart()
	{
		fade.CrossFade(false);
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene (nivel);
	}

	public void RestartLevel()
	{
		StartCoroutine (Restart ());
	}

	public void levelFail()
	{ 
		player.SendMessage ("Death");
		StartCoroutine (Restart());
	}

}

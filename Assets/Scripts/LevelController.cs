using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	public GameObject target,resp;
	PlayerMovement player;
	CameraMovement cam;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		cam = Camera.main.GetComponent<CameraMovement> ();

		StartCoroutine (Init());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	IEnumerator Init()
	{
		yield return new WaitForSeconds(2.0f);
		cam.ChangeToFollow (target);
		player.ChangeToMove ();
	}

	IEnumerator RespawnPlayer()
	{
		cam.ChangeToGoTo (resp.transform.position);
		player.ChangeToStop ();
		yield return new WaitForSeconds(0.2f);

		target.transform.position = resp.transform.position;
		yield return new WaitForSeconds(1.5f);
		cam.ChangeToFollow (target);
		player.ChangeToMove ();
	}

	public void PlayerDeath()
	{
		StartCoroutine (RespawnPlayer());
	}

	public void ChangeCheckPoint(Vector3 newCheck)
	{
		resp.transform.position = newCheck;
	}
}

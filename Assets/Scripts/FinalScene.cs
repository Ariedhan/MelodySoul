using UnityEngine;
using System.Collections;

public class FinalScene : MonoBehaviour 
{
	fader fade;
	CameraMovement cam;
	// Use this for initialization
	void Start () 
	{
		fade = GameObject.Find("Fader").GetComponent<fader> ();
		StartCoroutine (FinalCoroutina());
		cam = Camera.main.GetComponent<CameraMovement> ();
	}

	IEnumerator FinalCoroutina()
	{
		yield return 0;
		Animator anim = GameObject.Find ("With Katana").GetComponent<Animator> ();
		cam.ChangeToGoTo (GameObject.Find("Torso").transform.position);
		cam.ChangeToFollow (GameObject.Find("Torso"));


		yield return new WaitForSeconds(0.2f);
		anim.SetTrigger ("Triggerkatana");

		yield return new WaitForSeconds(3.0f);
		fade.CrossFade (false);
		Application.LoadLevel (0);
	}
}

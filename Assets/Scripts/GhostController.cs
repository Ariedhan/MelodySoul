using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour 
{
	public List<GameObject> travelingMarks;
	public float travelSpeed = 25;
	public AudioClip attackClip;
	public AudioClip jumpClip;
	public AudioClip dodgeClip;

	private AudioSource audioSource;
	private bool waiting;
	private Vector3 originPos;
	private Vector3 destinationPos;
	private GameObject target;
	private int markToTravel = 1;
	private float step = 0;
	private bool ghostMove;
	private GameObject levelController;
	private float currentWaitTime;


	public void changeWaitTime(float time)
	{
		currentWaitTime = time;
		Debug.Log (currentWaitTime);
	}

	void Awake ()
	{
		audioSource = this.GetComponent<AudioSource> ();
		if (travelingMarks != null && travelingMarks.Count >= 2) {
			Debug.Log ("travelling count "+travelingMarks.Count);
			transform.position = new Vector3 (travelingMarks [0].transform.position.x, travelingMarks [0].transform.position.y, 0);
			originPos = new Vector3 (transform.position.x, transform.position.y, 0);
			target = travelingMarks [1];
			destinationPos = new Vector3 (target.transform.position.x, target.transform.position.y, 0);
			markToTravel++;
		} else {
		}
	}

	// Use this for initialization
	void Start () {
		levelController = GameObject.Find ("LevelController");
	}

	public void StartMovement(float speed)
	{
		ghostMove = true;
		travelSpeed = speed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (ghostMove) {
			TravelTroughMarks ();
		}

	}

	private void TravelTroughMarks ()
	{
		transform.position = Vector3.Lerp (originPos, destinationPos, step);
		step += (travelSpeed * Time.deltaTime) / (originPos - destinationPos).magnitude;

		if (step > 1) {

			if(!waiting)
			{
				waiting=true;
				PlaySound();
				Debug.Log (currentWaitTime);
				StartCoroutine("Wait",currentWaitTime);
			}
		}
	}

	void PlaySound()
	{
		Debug.Log ("PlaySound");
		if (target.tag == "Attack") {
			audioSource.clip = attackClip;
			audioSource.Play ();
		} else if (target.tag == "Jump") {
			audioSource.clip = jumpClip;
			audioSource.Play ();
		} else if (target.tag == "Dodge") {
			audioSource.clip = dodgeClip;
			audioSource.Play ();
		} else if (target.tag == "EndMark") {
			levelController.SendMessage("GhostEnd");

		}
	}
	IEnumerator Wait(float waitTime)
	{

		yield return new WaitForSeconds(waitTime);

		if (markToTravel < travelingMarks.Count) {
			originPos = new Vector3 (transform.position.x, transform.position.y, 0);
			target = travelingMarks [markToTravel];
			destinationPos = new Vector3 (target.transform.position.x, target.transform.position.y, 0);
			markToTravel++;
			step = 0;
			waiting=false;
		}
	}

	public void SkipGhost()
	{

		travelSpeed = 100;
		currentWaitTime = 0.0f;
		audioSource.volume = 0.0f;
	}
}

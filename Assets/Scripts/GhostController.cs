using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostController : MonoBehaviour {


	public List<GameObject> travelingMarks;
	public float travelSpeed = 25;
	public AudioClip attackClip;
	public AudioClip jumpClip;
	public AudioClip dodgeClip;

	private AudioSource audio;

	private Vector3 originPos;
	private Vector3 destinationPos;
	private GameObject target;
	private int markToTravel = 1;
	private float step = 0;


	void Awake ()
	{
		audio = this.GetComponent<AudioSource> ();
		if (travelingMarks != null && travelingMarks.Count >= 2) {

			transform.position = new Vector3 (travelingMarks [0].transform.position.x, travelingMarks [0].transform.position.y, 0);
			originPos = new Vector3 (transform.position.x, transform.position.y, 0);
			target = travelingMarks [1];
			destinationPos = new Vector3 (target.transform.position.x, target.transform.position.y, 0);
			markToTravel++;
		} else {
			//shotPosition = this.transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		TravelTroughMarks ();
	
	}
	
	// Update is called once per frame
	void Update () {
		TravelTroughMarks ();
	
	}
	
	private void TravelTroughMarks ()
	{
		transform.position = Vector3.Lerp (originPos, destinationPos, step);
		step += (travelSpeed * Time.deltaTime) / (originPos - destinationPos).magnitude;
		
		if (step > 1) {

			StartCoroutine("PlaySoundWait");
		
		}
	}

	IEnumerator PlaySoundWait()
	{
		if (target.tag == "Attack") {
			audio.clip=attackClip;
		} else if (target.tag == "Jump") {
			audio.clip = jumpClip;
		} else if (target.tag == "Dodge") {
			audio.clip= dodgeClip;
		}

		yield return new WaitForSeconds(1.0f);

		if (markToTravel < travelingMarks.Count) {
			originPos = new Vector3 (transform.position.x, transform.position.y, 0);
			target = travelingMarks [markToTravel];
			destinationPos = new Vector3 (target.transform.position.x, target.transform.position.y, 0);
			markToTravel++;
			step = 0;
		}
	}
}

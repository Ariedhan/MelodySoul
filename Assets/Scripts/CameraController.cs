using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public GameObject follow;
	public GameObject ResetPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.L))
			StartCoroutine(ResetCo());
		else
			Camera.main.transform.position = new Vector3 (follow.transform.position.x, transform.position.y, transform.position.z);
	}

	IEnumerator ResetCo()
	{
		yield return new WaitForSeconds(1.0f);
	}
}

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	float t; 
	public enum CameraState
	{
		follow,
		goTo
	}
	public CameraState state;

	// Use this for initialization
	void Start () 
	{
		state = CameraState.follow;
	}

	IEnumerator GoTo(Vector3 v)
	{		
		t = 0f;

		while (t < 1)
		{ 
			if (state != CameraState.goTo)
				break;
			Vector2 aux = new Vector2 (0, 0);
			aux = Vector2.Lerp ((Vector2)transform.position,(Vector2)v, t); 
			transform.position = new Vector3(aux.x,aux.y,transform.position.z); 

			t += Time.deltaTime;
			yield return 0;
		} 	 
	}

	IEnumerator FollowObject(GameObject obj)
	{
		while (state == CameraState.follow) 
		{
			transform.position = new Vector3 (obj.transform.position.x, transform.position.y, transform.position.z);
			yield return 0;
		}
	}

	public void ChangeToFollow(GameObject obj)
	{
		state = CameraState.follow;
		StartCoroutine (FollowObject(obj));
	}

	public void ChangeToGoTo(Vector3 target)
	{
		state = CameraState.goTo;
		StartCoroutine (GoTo(target));
	}

	void Update()
	{
		
	}
}

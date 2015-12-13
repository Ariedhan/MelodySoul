using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	LevelController lvl;
	void Start()
	{
		lvl = GameObject.Find ("Level").GetComponent<LevelController> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			lvl.PlayerDeath ();
		}
	}
}

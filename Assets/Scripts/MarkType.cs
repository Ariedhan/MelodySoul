using UnityEngine;
using System.Collections;

public class MarkType : MonoBehaviour 
{
	public enum markType
	{
		attack,
		jump, 
		dodge, 
		end,
		start
	}
	public markType mark;
	LevelManager2 lvl2;
	Collider2D colli;
	// Use this for initialization
	void Start () 
	{
		colli = GetComponent<Collider2D> ();
		lvl2 = GameObject.Find ("LevelController").GetComponent<LevelManager2> ();
		switch (mark) 
		{
			case markType.jump:
				this.gameObject.tag = "Jump";
				break;
			case markType.dodge:
				this.gameObject.tag = "Dodge";
				break;
			case markType.attack:
				this.gameObject.tag = "Attack";
				break;
			case markType.end:
				this.gameObject.tag = "EndMark";
				break;
			case markType.start:
				this.gameObject.tag = "StartMark";
				break;		
			default:
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			switch (this.gameObject.tag) {
			case "Jump":
				lvl2.CurrentMark (markType.jump,this.gameObject);
				lvl2.f = true;
				break;

			case "Dodge":
				lvl2.CurrentMark (markType.dodge,this.gameObject);
				lvl2.f = true;
				break;
			case "Attack":
				lvl2.CurrentMark (markType.attack,this.gameObject);
				lvl2.f = true;
				break;

			case "EndMark":
				lvl2.CurrentMark (markType.end, this.gameObject);
				lvl2.f = true;
				lvl2.LevelSuccess ();
				break;
			case "StartMark":
				lvl2.CurrentMark (markType.start,this.gameObject);

				break;

			default:
				break;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			switch (this.gameObject.tag) {
			case "Jump":				 
				if (!lvl2.action) 
				{
					lvl2.f = false;
					lvl2.levelFail ();
				}
				else
					lvl2.f = false;
				break;
			
			case "Dodge": 
				if (!lvl2.action) 
				{
					lvl2.f = false;
					lvl2.levelFail ();
				}
				else
					lvl2.f = false;
				break;
			case "Attack": 
				if (!lvl2.action) 
				{
					lvl2.f = false;
					lvl2.levelFail ();
				}
				else
					lvl2.f = false;
				break; 
			case "EndMark": 

				break;
			case "StartMark": 
				break;

			default:
				break;
			}
		}
	}

	public void DisableCollider()
	{
		if (colli != null) 
		{
//			lvl2.TimeForAction = false;
			colli.enabled = false;
		}
	}
}

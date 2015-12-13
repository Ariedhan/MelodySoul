using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public enum EnemyState
	{
		idle,
		attack,
		death
	}
	public EnemyState state;
	Animator anim;
	int idleHash,attackHash,deathHash;
	int attackTriggerHash,deathTriggerHash,idleTriggerHash;

	void Start()
	{
		state = EnemyState.idle;
		anim = GetComponent<Animator> ();
		idleHash = Animator.StringToHash("Base Layer.Idle");
		attackHash = Animator.StringToHash("Base Layer.Attack");
		deathHash = Animator.StringToHash("Base Layer.Death");
		attackTriggerHash = Animator.StringToHash("attackTrigger");
		deathTriggerHash = Animator.StringToHash("deathTrigger");
		idleTriggerHash = Animator.StringToHash("idleTrigger");
	}

	public void EnemyIdle()
	{
		anim.SetTrigger (idleTriggerHash);
	}

	public void EnemyAttack()
	{
		anim.SetTrigger (attackTriggerHash);
	}

	public void EnemyDeath()
	{
		anim.SetTrigger (deathTriggerHash);
	}

	void Update()
	{
		AnimatorStateInfo stateinfo =  anim.GetCurrentAnimatorStateInfo(0);
		if (Input.GetKeyDown (KeyCode.A)) 
		{
			EnemyAttack ();
		}	

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			EnemyDeath ();
		}	

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			EnemyIdle ();
		}	
	}

}

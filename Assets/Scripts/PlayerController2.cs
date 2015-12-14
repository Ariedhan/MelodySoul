using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour 
{
	public enum PlayerStates
	{
		waiting,
		moving,
		action,
		synchronizing,
		dead
	}

	public PlayerStates state;
	private Rigidbody2D rb;
	private Animator anim;
	private float currentVelocity;

	public AudioClip attackClip;
	public AudioClip jumpClip;
	public AudioClip dodgeClip;

	public float waitAfterJump;
	public float waitForAttack;
	public float waitForSwipe;
	public AnimationCurve jumpCurve;
	Vector2 initPlayerPos;

	AudioSource audio;
	AnimatorStateInfo stateinfo;
	int idleTriggerHash,jumpTriggerHash,attackTriggerHash,dodgeTriggerHash,runningTriggerHash,deathTriggerHash;
	// Use this for initialization
	void Start () 
	{
		state = PlayerStates.waiting;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		initPlayerPos = (Vector2)transform.position;
		audio = GetComponent<AudioSource> ();
		idleTriggerHash = Animator.StringToHash ("Player_Idle");
		jumpTriggerHash = Animator.StringToHash ("Player_Jump");
		attackTriggerHash = Animator.StringToHash("Player_Attack");
		dodgeTriggerHash= Animator.StringToHash ("Player_Dodge");
		runningTriggerHash= Animator.StringToHash ("Player_Running");
		deathTriggerHash= Animator.StringToHash ("Player_Death");
	}

	void Update()
	{
		stateinfo =  anim.GetCurrentAnimatorStateInfo(0);
	}

	public void ChangeToMoving(float speed)
	{
		currentVelocity = speed;
		state = PlayerStates.moving;
		StartCoroutine (MovingCoroutine(speed));
	}

	public void ChangeToAction(MarkType.markType mType)
	{
		state = PlayerStates.action;
		StartCoroutine (ActionCoroutine(mType));
	}

	public void ChangeToSynchronizing(float timef)
	{
		state = PlayerStates.synchronizing;
		StartCoroutine (SynchronizingCoroutine(timef));

	}

	public void ChangeToWaiting()
	{
		state = PlayerStates.waiting;
		StartCoroutine (WaitingCoroutine());
		
	}

	IEnumerator MovingCoroutine(float speed)
	{
		rb.velocity = Vector2.right * speed;
		anim.SetTrigger (runningTriggerHash);
		yield return 0;
	}

	IEnumerator ActionCoroutine(MarkType.markType mType)	
	{
		switch (mType) 
		{
			case MarkType.markType.jump:
				StartCoroutine (JumpCoroutine(1.25f));
				break;
			case MarkType.markType.dodge:
				StartCoroutine (DodgeCoroutine());
				break;
			case MarkType.markType.attack:
				StartCoroutine (AttackCoroutine());
			break;

			default:
				break;
		}
		yield return 0;
	}

	IEnumerator SynchronizingCoroutine(float timeF)
	{
		rb.velocity = Vector2.zero;
		anim.SetTrigger (idleTriggerHash);
		yield return new WaitForSeconds(timeF);
		ChangeToMoving (currentVelocity);
	}

	IEnumerator WaitingCoroutine()
	{
		rb.velocity = Vector2.zero;
		anim.SetTrigger (idleTriggerHash);
		yield return new WaitForSeconds (1.0f);
		ChangeToMoving (currentVelocity);

	}

	IEnumerator JumpCoroutine(float animationTime)
	{
		anim.SetTrigger (jumpTriggerHash);
		audio.clip = jumpClip;
		audio.Play ();
		float elapsedTime = 0;
		float initY = transform.position.y;
		while (elapsedTime < animationTime) 
		{
			transform.position = new Vector3(
				transform.position.x,
				Mathf.Lerp(initY, initY + 2, jumpCurve.Evaluate(elapsedTime/animationTime)),
				transform.position.z);
			elapsedTime += Time.deltaTime; 
			yield return 0;
		} 
		rb.velocity = Vector2.zero;
		transform.position = new Vector3 (transform.position.x, initPlayerPos.y, transform.position.z);
		ChangeToWaiting ();

	}

	IEnumerator DodgeCoroutine()
	{
		audio.clip = dodgeClip;
		audio.Play ();
		anim.SetTrigger (dodgeTriggerHash);
		//
		yield return new WaitForSeconds(1);
		state = PlayerStates.waiting;
		rb.velocity = Vector2.zero;
		anim.SetTrigger (idleTriggerHash);
		yield return new WaitForSeconds (waitForSwipe);
		ChangeToMoving (currentVelocity);
	}

	IEnumerator AttackCoroutine()
	{
		audio.clip = attackClip;
		audio.Play ();
		rb.velocity = Vector2.zero;
		anim.SetTrigger (attackTriggerHash);
		ChangeToSynchronizing (waitForAttack);
		yield return 0;
	}

	IEnumerator DeathCoroutine()
	{
		rb.velocity = Vector2.zero;
		anim.SetTrigger (deathTriggerHash);
	
		yield return 0;
	}

	public void Death()
	{
		StartCoroutine (DeathCoroutine());
	}
}

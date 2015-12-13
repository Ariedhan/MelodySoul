using UnityEngine;
using System.Collections;

public class MarkType : MonoBehaviour {


	public enum markType{
		attack, jump, dodge, end, start

	}
	public markType mark;
	// Use this for initialization
	void Start () {
		if (mark == markType.attack) {
			this.gameObject.tag = "Attack";
		} else if (mark == markType.dodge) {
			this.gameObject.tag = "Dodge";
		} else if (mark == markType.jump) {
			this.gameObject.tag = "Jump";
		
		} else if (mark == markType.end) {
			this.gameObject.tag="EndMark";
		}else if (mark == markType.start) {
			this.gameObject.tag="StartMark";
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

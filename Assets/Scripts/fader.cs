using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fader : MonoBehaviour {

	private Image fade;

	// Use this for initialization
	void Start () {

		fade = this.GetComponent<Image> ();
		CrossFade (true);
	}

	public void CrossFade(bool fadeIn)
	{
		if (fadeIn) {
			fade.CrossFadeAlpha (0.0f, 1.0f, false);
		} else {
			fade.CrossFadeAlpha (1.0f, 1.0f, false);
		}
	}
}

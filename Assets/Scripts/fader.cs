using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fader : MonoBehaviour {

	private Image fade;
	private AudioSource audio;
	// Use this for initialization
	void Start () {

		fade = this.GetComponent<Image> ();
		audio = GetComponent<AudioSource> ();
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

	public void InstantFade(bool fadeIn)
	{
		fade = this.GetComponent<Image> ();
		Color c = new Color(fade.color.r,fade.color.g,fade.color.b,fade.color.a);

		if (fadeIn) 
		{
			c.a = 1;
			fade.color = c;
		} 
		else 
		{
			c.a = 0;
			fade.color = c;
		}
	}

}

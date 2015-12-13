using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{
	public GameObject panelMenu, panelCredits,panelFade;
	public Sprite play, playOn, credits, creditsOn;
	fader fade;
	public enum MenuState
	{
		main,
		credits

	}
	public MenuState menu;
	Button[] b; 
	// Use this for initialization
	void Start () 
	{
		panelFade.SetActive (true);
		menu = MenuState.main;
		panelMenu.SetActive(true);
		panelCredits.SetActive(false);
		fade = panelFade.GetComponent<fader> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) 
		{
			if (panelMenu.activeInHierarchy) 
			{
				Sprite spr = panelMenu.GetComponent<Sprite> ();
				//Sprite aux = 
				PlayButton ();
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) 
		{
			if (panelMenu.activeInHierarchy) 
			{
				ChangeMenuToCredits ();
			}			
		}		
	
	}

	public void ChangeMenuToCredits()
	{
		StartCoroutine (Credits());
	}

	public void ChangeMenuToMain()
	{
		panelMenu.SetActive(true);
		panelCredits.SetActive(false);
	}

	public void PlayButton()
	{
		StartCoroutine (PlayScene());
	}

	IEnumerator Credits()
	{ 
		Image[] img = GetComponentsInChildren<Image> ();
		img[3].sprite = creditsOn;
		yield return new WaitForSeconds (0.2f);
		img[3].sprite = credits;
		yield return new WaitForSeconds (0.2f);
		panelMenu.SetActive(false);
		panelCredits.SetActive(true);
		yield return new WaitForSeconds (5.0f);
		ChangeMenuToMain ();
	}

	IEnumerator PlayScene()
	{
		Image[] img = GetComponentsInChildren<Image> ();
		img[2].sprite = playOn;
		yield return new WaitForSeconds (0.2f);
		img[2].sprite = play;
		yield return new WaitForSeconds (0.2f);

		fade.CrossFade (false);
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("prototipoGhost");
	}
}

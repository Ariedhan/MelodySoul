using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{	
	public enum inputState
	{
		none,
		keyLeft,
		keyRight,
		keyBoth
	}
	public inputState inp,aux;


	void Start()
	{
		inp = inputState.none;

	}
	// Update is called once per frame
	void Update () 
	{
		if ((Input.GetKeyUp (KeyCode.LeftArrow)) || (Input.GetKeyUp (KeyCode.RightArrow))) 
		{
			inp = inputState.none;
		}
		else 
		{
			if (Input.GetKey(KeyCode.LeftArrow) && ((inp != inputState.keyLeft)&&(inp != inputState.keyBoth)))
			{
				if (Input.GetKey (KeyCode.RightArrow)) 
				{
					inp = inputState.keyBoth;
				} 
				else 
				{
					inp = inputState.keyLeft;
				}
			}
			if (Input.GetKey(KeyCode.RightArrow) && ((inp != inputState.keyRight)&&(inp != inputState.keyBoth)))
			{
				if (Input.GetKey (KeyCode.LeftArrow)) 
				{
					inp = inputState.keyBoth;
				} 
				else 
				{
					inp = inputState.keyRight;
				}
			}
		}	
	}
}

using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	public void StartEasy(){
		Application.LoadLevel ("easy");
	}

	public void StartMedium(){
		Application.LoadLevel ("medium");
	}

	public void StartExpert(){
		Application.LoadLevel ("expert");
	}

	public void QuitGame(){
		Application.Quit();
	}

	 public void OpenMenu(){
		Application.LoadLevel ("menu");
	}
}
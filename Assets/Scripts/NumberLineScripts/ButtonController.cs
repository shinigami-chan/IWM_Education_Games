using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	public void StartEasy(){
		SceneManager.LoadScene("Numberline_Game_Easy");
	}

	public void StartMedium(){
		SceneManager.LoadScene ("Numberline_Game_Medium");
	}

	public void StartExpert(){
		SceneManager.LoadScene ("Numberline_Game_Expert");
	}

	public void QuitGame(){
        SceneManager.LoadScene("Game_Selection");
	}

	 public void OpenMenu(){
		SceneManager.LoadScene ("Numberline_Menu");
	}
}
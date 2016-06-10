using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	public void StartEasy(){
		SceneManager.LoadScene("numberline_game_easy_scene");
	}

	public void StartMedium(){
		SceneManager.LoadScene ("numberline_game_medium_scene");
	}

	public void StartExpert(){
		SceneManager.LoadScene ("numberline_game_expert_scene");
	}

	public void QuitGame(){
        SceneManager.LoadScene("game_selection_scene");
	}

	 public void OpenMenu(){
		SceneManager.LoadScene ("numberline_menu_scene");
	}
}
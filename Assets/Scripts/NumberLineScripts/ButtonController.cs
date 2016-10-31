using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	// Use this for initialization
	public void StartEasy(){
        Logger.Instance.GameLog(Action.START_NUMBER_LINE_GAME,1); //1 for easy mode
        SceneManager.LoadScene("Numberline_Game_Easy");
	}

	public void StartMedium(){
        Logger.Instance.GameLog(Action.START_NUMBER_LINE_GAME,2); //2 for medium mode
        SceneManager.LoadScene ("Numberline_Game_Medium");
	}

	public void StartExpert(){
        Logger.Instance.GameLog(Action.START_NUMBER_LINE_GAME,3); //3 for expert mode
        SceneManager.LoadScene ("Numberline_Game_Expert");
	}

	public void QuitGame(){
        SceneManager.LoadScene("Game_Selection_Android");
	}

	 public void OpenMenu(){
		SceneManager.LoadScene ("Numberline_Menu");
	}
}
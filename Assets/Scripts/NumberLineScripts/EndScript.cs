using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {


	public Text highScoreText;
	public Text endScoreText;
	private int endScore;
	public int highScore;
	public static int levelEnd;



	void OnGUI () {

		levelEnd = GameController.level;

		if( levelEnd == 1 ){
			highScore = PlayerPrefs.GetInt ("High Score easy");
			endScore = PlayerPrefs.GetInt("Player Score easy");

			if (endScore > highScore) {
				highScore = endScore;
				PlayerPrefs.SetInt("High Score easy", highScore);
			} else {
				highScore = PlayerPrefs.GetInt("High Score easy");
				
			}

		}

		else if( levelEnd == 2){
			highScore = PlayerPrefs.GetInt ("High Score medium");
			endScore = PlayerPrefs.GetInt("Player Score medium");
			
			if (endScore > highScore) {
				highScore = endScore;
				PlayerPrefs.SetInt("High Score medium", highScore);
			} else {
				highScore = PlayerPrefs.GetInt("High Score medium");
				
			}
			
		}

		else if( levelEnd == 3){
			highScore = PlayerPrefs.GetInt ("High Score expert");
			endScore = PlayerPrefs.GetInt("Player Score expert");
			
			if (endScore > highScore) {
				highScore = endScore;
				PlayerPrefs.SetInt("High Score expert", highScore);
			} else {
				highScore = PlayerPrefs.GetInt("High Score expert");
				
			}
			
		}



		highScoreText.text = ("Highscore: " + highScore);
		endScoreText.text = ("erreichte Punktzahl: " + endScore);
	

	}

}

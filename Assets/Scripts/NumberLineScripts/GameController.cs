using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public int score;
	public int trials;
	public static int level;
	public static double result;

	
	


	void OnGUI(){


	

		if( Application.loadedLevelName.Equals("easy")){
			score = PlayerPrefs.GetInt("Player Score easy");
			level = 1;
			result = CollisionEasy.outputContact;
		}
		else if( Application.loadedLevelName.Equals("medium")){
			score = PlayerPrefs.GetInt("Player Score medium");
			level = 2;
			result = CollisionMedium.outputContact;
		}
		else if( Application.loadedLevelName.Equals("expert")){
			score = PlayerPrefs.GetInt("Player Score expert");
			level = 3;
			result = CollisionExpert.outputContact;
		}


		trials = PlayerPrefs.GetInt ("Trials");

		scoreText.text = "Score: " + score.ToString ();
	

		if (trials == 15) {

			StartCoroutine(endGame (2f));
		}
	}

	IEnumerator endGame(float waitTime){
		yield return new WaitForSeconds (waitTime);
		Application.LoadLevel("end");
	}




}

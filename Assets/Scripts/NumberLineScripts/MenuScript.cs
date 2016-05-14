using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {



	void Start(){
	
		PlayerPrefs.SetInt("Player Score easy", 0);
		PlayerPrefs.SetInt("Player Score medium", 0);
		PlayerPrefs.SetInt("Player Score expert", 0);
		PlayerPrefs.SetInt ("Trials", 0);

	}


}

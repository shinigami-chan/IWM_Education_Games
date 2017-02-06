using UnityEngine;
using System.Collections;

public class OnMainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		AchievementManager instance = AchievementManager.Instance;

		BalloonEventCounter.Instance.CheckForAchievements ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

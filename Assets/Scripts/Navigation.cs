using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class Navigation : MonoBehaviour {
	public GameObject login;
	public GameObject register1;
	public GameObject register2;
	public GameObject mainMenu;
	public GameObject gameSelection;
	public GameObject navigation;
	public GameObject profile;
	public GameObject statistics;
	public GameObject help;
	public GameObject achievements;

	public GameObject backButton;

	public Image statisticsImg;
	public Image profileImg;
	public Image helpImg;
	public Image achievementsImg;

	private static Sprite statisticsActive;
	private static Sprite statisticsInactive;
	private static Sprite profileActive;
	private static Sprite profileInactive;
	private static Sprite helpActive;
	private static Sprite helpInactive;
	private static Sprite achievementsActive;
	private static Sprite achievementsInactive;

	private readonly string PATH = "Buttons/";

	public void GOsLoaded () {
		Debug.Log ("GameSelection loaded");
		Debug.Log (gameSelection == null);
	}

	void Update() {

	}

	void Awake () {
		statisticsActive = Resources.Load<Sprite> (PATH + "stats_active");
		statisticsInactive = Resources.Load<Sprite> (PATH + "stats");
		profileActive = Resources.Load<Sprite> (PATH + "profile_active");
		profileInactive = Resources.Load<Sprite> (PATH + "profile");
		helpActive = Resources.Load<Sprite> (PATH + "questionmark_active");
		helpInactive = Resources.Load<Sprite> (PATH + "questionmark");
		achievementsActive = Resources.Load<Sprite> (PATH + "trophy_active");
		achievementsInactive = Resources.Load<Sprite> (PATH + "trophy_inactive");
	}

	void Start () {
		//StartLogin ();
		//StartProfile ();
	}

	private void StartLogin () {
		HideAll ();
		ShowLogin ();
	}

	private void StartProfile () {
		HideAll ();
		ShowProfile ();
	}


	public void HideAll () {
		login.SetActive (false);
		register1.SetActive (false);
		register2.SetActive (false);
		gameSelection.SetActive (false);
		navigation.SetActive (false);
		profile.SetActive (false);
		statistics.SetActive (false);
		help.SetActive (false);
		achievements.SetActive (false);
		backButton.SetActive (false);
		mainMenu.SetActive (false);
	}

	/// <summary>
	/// Activates the Login GameObject and deactivates other content
	/// </summary>
	public void ShowLogin () {
		HideAll ();
		login.SetActive (true);
	}

	public void ShowMainMenu () {
		HideAll ();
		mainMenu.SetActive (true);
	}

	/// <summary>
	/// Activates the Register GameObject and deactives other content
	/// </summary>
	public void ShowRegister1 () {
		HideAll ();
		register1.SetActive (true);
	}

	/// <summary>
	/// Activates the Register2 GameObject and deactivates other content
	/// </summary>
	public void ShowRegister2() {
		HideAll ();
		register2.SetActive (true);
	}

	public void ShowProfile() {
		HideAll ();
		profile.SetActive (true);
		//navigation.SetActive (true);
		backButton.SetActive (true);
		SetProfileActive ();
	}

	public void ShowStatistics() {
		HideAll ();
		statistics.SetActive (true);
		//navigation.SetActive (true);
		backButton.SetActive (true);
		SetStatisticsActive ();
	}

	public void ShowHelp () {
		HideAll ();
		help.SetActive (true);
		//navigation.SetActive (true);
		backButton.SetActive (true);
		SetHelpActive ();
	}

	public void ShowGameSelection () {
		Debug.Log ("ShowGameSelection()");
		HideAll ();
		gameSelection.SetActive (true);
		backButton.SetActive (true);
		//navigation.SetActive (true);
		SetAllActive (false);


		login.SetActive (false);
	}

		
	public void ShowAchievments() {
		HideAll ();
		achievements.SetActive (true);
		navigation.SetActive (false);
		backButton.SetActive (true);
		SetAchievementsActive ();
	}

	public void AreSpritesNull() {
		Debug.Log (statisticsActive == null ? "statistics active is null" : "statistics is not null");
		Debug.Log (statisticsInactive == null ? "statistics inactive is null" : "statistics is not null");
		Debug.Log (helpActive == null ? "help active is null" : "help is not null");
		Debug.Log (helpInactive == null ? "help inactive is null" : "help is not null");
		Debug.Log (profileActive == null ? "profile active is null" : "profile is not null");
		Debug.Log (profileInactive == null ? "profile inactive is null" : "profile is not null");
		Debug.Log (achievementsActive == null ? "achievements active is null" : "achievements is not null");
		Debug.Log (achievementsInactive == null ? "achievements inactive is null" : "achievements is not null");
	}

	public void SetAllActive (bool active) {
		AreSpritesNull ();
		if (active) {
			statisticsImg.sprite = statisticsActive;
			profileImg.sprite = profileActive;
			helpImg.sprite = helpActive;
			achievementsImg.sprite = achievementsActive;
		} else {
			statisticsImg.sprite = statisticsInactive;
			profileImg.sprite = profileInactive;
			helpImg.sprite = helpInactive;
			achievementsImg.sprite = achievementsInactive;
		}
	}

	private void SetStatisticsActive () {
		SetAllActive (false);
		statisticsImg.sprite = statisticsActive;
	}

	private void SetProfileActive () {
		SetAllActive (false);
		profileImg.sprite = profileActive;
	}

	private void SetHelpActive () {
		SetAllActive (false);
		helpImg.sprite = helpActive;
	}

	private void SetAchievementsActive () {
		SetAllActive (false);
		achievementsImg.sprite = achievementsActive;
	}


	public void LoadBalloonGame() {
		SceneManager.LoadScene("Balloon_Menu");
	}
		
	public void LoadNumberlineGame() {
		SceneManager.LoadScene("Numberline_Menu");
	}

	public void Logout() {
		Logger.Instance.EndSession ();
		Debug.Log ("Destroy in logout()");
		ShowLogin ();
	}
}

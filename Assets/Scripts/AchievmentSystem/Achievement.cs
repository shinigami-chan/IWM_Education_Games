using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Achievement {

	private string name;
	public string Name {
		get {
			return name;
		}
	}

	private string description;

	public string Description
	{
		get { return description; }
		set { description = value; }
 	}

	private bool unlocked;
	private int spriteIndex;

	public int SpriteIndex
	{
		get { return spriteIndex; }
		set { spriteIndex = value; }
	}

	private string parent;


	private GameObject achievementRef;


	public Achievement (string parent, string name, string description, int spriteIndex) {
		this.parent = parent;
		Debug.Log ("Achievement name: " + name);
		this.name = name;
		this.description = description;
		this.unlocked = false;
		this.spriteIndex = spriteIndex;

		LoadAchievement ();
	}	

	public void LoadIntoScene (GameObject achievementPrefab) {
		Debug.Log ("LoadIntoScene");

		achievementRef = achievementPrefab;

		achievementPrefab.transform.SetParent (AchievementManager.Instance.AchievementPanels[parent].transform);

		if (unlocked) {
			achievementPrefab.GetComponent<Image> ().sprite = AchievementManager.Instance.unlockedSprite;
		}
	}

	public void print() {
		Debug.Log("Achievement Name: " + name + "\nAchievement description: " + description + "\nAchievement parent: " + parent);

	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}

	public bool EarnAchievment() {
		if (!unlocked) {
			if (achievementRef != null) 
				achievementRef.GetComponent<Image> ().sprite = AchievementManager.Instance.unlockedSprite;
			SaveAchievement (true);
			return true;
		}
		return false;
	}

	public void SaveAchievement (bool value) {
		unlocked = value;

		PlayerPrefs.SetInt (name, value ? 1 : 0);
		PlayerPrefs.Save ();
	}

	public void LoadAchievement () {

		Debug.Log ("Lolo" + (PlayerPrefs.GetInt (name) == 1 ? true : false));
		unlocked = PlayerPrefs.GetInt (name) == 1 ? true : false;

		if (unlocked && achievementRef != null) {
			achievementRef.GetComponent<Image> ().sprite = AchievementManager.Instance.unlockedSprite;
		}
	}

}

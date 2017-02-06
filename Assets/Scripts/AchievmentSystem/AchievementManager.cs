using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {
	/// <summary>
	/// GameObject that contains the Achievement Panels
	/// </summary>
	public GameObject achievementParentPanel;

	/// <summary>
	/// Prefab for an Achievement
	/// </summary>
	public GameObject achievementPrefab;


	/// <summary>
	/// Sprites for the Achievement Icons
	/// </summary>
	public Sprite[] sprites;

	/// <summary>
	/// The scroll rect.
	/// </summary>
	public ScrollRect scrollRect;

	/// <summary>
	/// The Achievement Menu
	/// </summary>
	public GameObject achievementMenu;

	/// <summary>
	/// The Achievement Category that is currently active
	/// </summary>
	public AchievementButton activeCategoryButton;

	public GameObject visualAchievement;

	public Sprite unlockedSprite;

	public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
	public Dictionary<string, GameObject> achievementPanels = new Dictionary<string, GameObject> ();

	public Dictionary<string, GameObject> AchievementPanels {
		get {return achievementPanels; }
	}


	private float fadeInTime = 0.5f;
	private float fadeOutTime = 2f;

	/// <summary>
	/// Singleton instance of the Achievement Manager such that it can be accessed from everywhere
	/// </summary>
	private static AchievementManager instance;

	public static AchievementManager Instance {
		get
		{
			return AchievementManager.instance;
		}
	}
		
	/// <summary>
	/// This is the pane where the achievments are shown when earned
	/// (Shall not be destroyed on Scene change)
	/// </summary>
	public GameObject earnCanvas;

	// Add all new categorys here and drag and drop the panels in here
	public GameObject generalCategory;
	public GameObject ballonsCategory;
	public GameObject numberlineCategory;

	// Add all new categorys here and drag and drop the panels in here
	public Button generalBtn;
	public Button balloonsBtn;
	public Button numberlineBtn;


	void Awake() {
		if (instance == null) {
			instance = this;
			Debug.Log ("awake");
			Debug.Log (gameObject == null ? "null" : "not null");
			DontDestroyOnLoad (transform.gameObject);
		} else
			Destroy (transform.gameObject);
	}


	// Use this for initialization
	void Start () {
		InitializeAchievments ();
		//PlayerPrefs.DeleteAll ();
	}

	public GameObject GetCategoryPanel(string category) {
		return achievementPanels [category];
	}

	private void InitializeAchievments() {
		CreateAchievement (AchievementRefs.GENERAL_CATEGORY_PANEL, "Drücke W", "Drücke W um diesen Erfolg freizuschalten!", 0);
		CreateAchievement (AchievementRefs.BALLOONS_CATEGORY_PANEL, "Knallfrosch", "BauchiBauchi", 0);
		CreateAchievement (AchievementRefs.GENERAL_CATEGORY_PANEL, "Drücke X", "Drücke W um die Erfolg freizuschalten!", 0);
		CreateAchievement (AchievementRefs.BALLOONS_CATEGORY_PANEL, "Test", "BauchiBauchi", 0);

		foreach (GameObject achievementList in achievementPanels.Values) {
			achievementList.SetActive (false);
		}
	}

	public void Load () {
		LoadIntoScene ();
		activeCategoryButton.Click ();
		achievementParentPanel.SetActive (false);
	}


	/// <summary>
	/// The achievementManager is not destroyed on scene change since it maintains the
	/// achievment information throughout the whole game session.
	/// Therefore you cannot pass the GameObjects via public variables with the unity editer
	/// but can only be set via GameObject Find. Since this is a very hard to maintain approach
	/// there is the static class AchievementRefs containing static string variables for the 
	/// objects belonging to the achievement system.
	/// This method loads the gameobject into the variables.
	/// </summary>
	public void LoadGameObjects() {
		achievementParentPanel = GameObject.Find (AchievementRefs.PARENT_PANEL);
		achievementMenu = GameObject.Find (AchievementRefs.MENU);

		scrollRect = GameObject.Find (AchievementRefs.MASK).GetComponent<ScrollRect>();

		achievementPanels.Add (AchievementRefs.GENERAL_CATEGORY_PANEL, GameObject.Find (AchievementRefs.GENERAL_CATEGORY_PANEL));
		achievementPanels.Add (AchievementRefs.BALLOONS_CATEGORY_PANEL, GameObject.Find (AchievementRefs.BALLOONS_CATEGORY_PANEL));
		achievementPanels.Add (AchievementRefs.NUMBERLINE_CATEGORY_PANEL, GameObject.Find (AchievementRefs.NUMBERLINE_CATEGORY_PANEL));

		generalBtn = GameObject.Find (AchievementRefs.GENERAL_CATEGORY_BUTTON).GetComponent<Button> ();
		balloonsBtn = GameObject.Find (AchievementRefs.BALLOONS_CATEGORY_BUTTON).GetComponent<Button> ();
		numberlineBtn = GameObject.Find (AchievementRefs.NUMBERLINE_CATEGORY_BUTTON).GetComponent<Button> ();

		balloonsBtn.onClick.AddListener(() => ChangeCategory (balloonsBtn.gameObject));
		numberlineBtn.onClick.AddListener(() => ChangeCategory (numberlineBtn.gameObject));
		generalBtn.onClick.AddListener(() => ChangeCategory (generalBtn.gameObject));

		// CREATE SCRIPT THAT AUTOMATICALLY SAVES THE NAMES FROM THE INPUT GAMEOBJECTS AND RELOADS THEM

		earnCanvas = GameObject.Find (AchievementRefs.EARN_CANVAS);

		Debug.Log (GameObject.Find(AchievementRefs.GENERAL_CATEGORY_BUTTON) == null ? "active is null" : "not nullaby");

		activeCategoryButton = activeCategoryButton == null ? balloonsBtn.GetComponent<AchievementButton> () : activeCategoryButton;
		Debug.Log (GameObject.Find(AchievementRefs.GENERAL_CATEGORY_BUTTON) == null ? "active is null" : "not nullaby");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)) {
			EarnAchievement ("Drücke W");
		}
	}

	/// <summary>
	/// search for the 
	/// Check if an achievement is earnable or already earned. If earnable set it
	/// to earned and then create a visual achievement, set its information and 
	/// let it fade in and out.
	/// </summary>
	/// <param name="title">Title is the achievements title</param>
	public void EarnAchievement(string title) {
		if (achievements [title].EarnAchievment ()) {
			Debug.Log ("EarnAchievement");

			// Create new visual achievement for the earn panel
			GameObject achievement = Instantiate ((GameObject)visualAchievement);

			// Make sure the visual achievement is visible (z-axis)
			Vector3 pos = achievement.transform.localPosition;
			achievement.transform.localPosition = new Vector3(pos.x,pos.y, 1);

			//Set the parent to earnCanvas
			achievement.transform.SetParent (earnCanvas.transform);

			//Set the achievement information
			SetAchievementInfo (achievement, title);

			//Make the achievement fade in and fade out
			StartCoroutine (FadeAchievement(achievement));
			//StartCoroutine (HideAchievment (achievment));
		}
	}

	/// <summary>
	/// Load the achievements into the scene when the main scene is loaded.
	/// Must be performed after load GameObjects.
	/// </summary>
	public void LoadIntoScene() {
		LoadGameObjects ();
		foreach (Achievement a in achievements.Values) {
			a.print ();
			GameObject achievement = (GameObject)Instantiate (achievementPrefab);
			Debug.Log ("Size: " + GameObject.FindGameObjectsWithTag ("AchievementCategories").Length);
			a.LoadIntoScene (achievement);
			SetAchievementInfo (achievement, a.Name);
		}
	}

	public IEnumerator HideAchievement(GameObject achievment) {
		yield return new WaitForSeconds (3);
		Destroy (achievment);
	}

	public void CreateAchievement(string parent, string title, string description, int sprite) {
		//GameObject achievement = (GameObject)Instantiate (achievementPrefab);

		Achievement newAchievment = new Achievement (parent, title, description, sprite);
		achievements.Add (title, newAchievment);

		//SetAchievementInfo (achievement, title);
	}

	private void SetAchievementInfo(GameObject achievement, string title) {
		Achievement newAchievement = achievements [title];


		//GameObject parentObject = GameObject.Find(parent);
		//achievement.transform.SetParent (parentObject);

		achievement.transform.localScale = new Vector3 (1, 1, 1);
		Vector3 pos = achievement.transform.localPosition;
		achievement.transform.localPosition = new Vector3(pos.x,pos.y, 1);

		achievement.transform.GetChild (0).GetComponent<Text> ().text = title;
		achievement.transform.GetChild (1).GetComponent<Text> ().text = newAchievement.Description;
		//achievment.transform.GetChild (2).GetComponent<Image> ().sprite = sprites [newAchievment.spriteindex];
	}

	public void ChangeCategory(GameObject button) {
		AchievementButton achievementButton = button.GetComponent<AchievementButton> ();
		scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform> ();

		achievementButton.Click ();
		activeCategoryButton.Click ();

		activeCategoryButton = achievementButton;
	}


	private IEnumerator FadeAchievement(GameObject achievement) {
		CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup> ();

		float fadeInRate = 1.0f / fadeInTime;
		float fadeOutRate = 1.0f / fadeOutTime;

		int startAlpha = 0;
		int endAlpha = 1;

		float progress = 0.0f;

		float rate = fadeInRate;

		for (int i = 0; i < 2; i++) {
			progress = 0.0f;

			while (progress < 1.0) {
				canvasGroup.alpha = Mathf.Lerp (startAlpha, endAlpha, progress);
				progress += rate * Time.deltaTime;

				yield return null;
			}
			yield return new WaitForSeconds (2);
			startAlpha = 1;
			endAlpha = 0;
			rate = fadeOutRate;
		}

		Destroy (achievement);
	}
}


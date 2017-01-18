using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievmentManager : MonoBehaviour {

	public GameObject achievmentPrefab;

	public Sprite[] sprites;

	public ScrollRect scrollRect;

	public GameObject achievmentMenu;

	private AchievmentButton activeButton;

	public GameObject visualAchievment;

	public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>();


	// Use this for initialization
	void Start () {
		//Example:
		//achievments.Add("Run", new Achievment("Run", "He ran", 0, this.gameObject));



		activeButton = GameObject.Find ("GeneralBtn").GetComponent<AchievmentButton> ();

		if (activeButton == null)
			Debug.Log ("active Button is null");

		//CreateAchievment ("General", "Mathemarathon", "Wow, du hast schon 5 Stunden gespielt", 0);
		CreateAchievment ("General", "Drücke W", "Drücke W um diesen Erfolg freizuschalten!", 0);


		foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList")) {
			achievmentList.SetActive (false);
			Debug.Log ("Disable");
		}

		activeButton.Click ();

		achievmentMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			achievmentMenu.SetActive (!achievmentMenu.activeSelf);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			EarnAchievment ("Drücke W");
		}
	}

	public void EarnAchievment(string title) {
		if (achievments [title].EarnAchievment ()) {
			GameObject achievment = Instantiate ((GameObject)visualAchievment);
			Vector3 pos = achievment.transform.localPosition;
			achievment.transform.localPosition = new Vector3(pos.x,pos.y, 1);

			SetAchievmentInfo ("EarnCanvas", achievment, title);

			StartCoroutine (HideAchievment (achievment));
		}
	}

	public IEnumerator HideAchievment(GameObject achievment) {
		yield return new WaitForSeconds (3);
		Destroy (achievment);
	}

	public void CreateAchievment(string parent, string title, string description, int sprite) {
		GameObject achievment = (GameObject)Instantiate (achievmentPrefab);

		Achievment newAchievment = new Achievment (name, description, sprite, achievment);
		achievments.Add (title, newAchievment);

		SetAchievmentInfo (parent, achievment, title);
	}

	private void SetAchievmentInfo(string parent, GameObject achievment, string title) {
		Achievment newAchievment = achievments [title];

		achievment.transform.SetParent (GameObject.Find (parent).transform);
		achievment.transform.localScale = new Vector3 (1, 1, 1);
		Vector3 pos = achievment.transform.localPosition;
		achievment.transform.localPosition = new Vector3(pos.x,pos.y, 1);

		achievment.transform.GetChild (0).GetComponent<Text> ().text = title;
		achievment.transform.GetChild (1).GetComponent<Text> ().text = newAchievment.Description;
		//achievment.transform.GetChild (2).GetComponent<Image> ().sprite = sprites [newAchievment.spriteindex];
	}

	public void ChangeCategory(GameObject button) {
		AchievmentButton achievmentButton = button.GetComponent<AchievmentButton> ();
		scrollRect.content = achievmentButton.achievmentList.GetComponent<RectTransform> ();

		achievmentButton.Click ();
		activeButton.Click ();

		activeButton = achievmentButton;
	}

}

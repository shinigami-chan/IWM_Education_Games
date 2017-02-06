using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	public GameObject achievementList;
	public Sprite neutral, highlight;
	private bool active;

	private Image sprite;

	void Awake() {
		sprite = GetComponent<Image> ();
		sprite.sprite = neutral;
		active = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Click () {
		Debug.Log ("Clicked " + name + "!");

		if (!active) {
			sprite.sprite = highlight;
			achievementList.SetActive (!active);
		} else {
			sprite.sprite = neutral;
			achievementList.SetActive (!active);
		}
		active = !active;
	}
}

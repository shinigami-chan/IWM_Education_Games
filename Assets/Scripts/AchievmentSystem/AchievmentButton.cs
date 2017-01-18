using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievmentButton : MonoBehaviour {

	public GameObject achievmentList;
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
		if (!active) {
			sprite.sprite = highlight;
			achievmentList.SetActive (!active);
		} else {
			sprite.sprite = neutral;
			achievmentList.SetActive (!active);
		}
		active = !active;
	}
}

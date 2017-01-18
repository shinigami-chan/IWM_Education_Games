using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Badge : MonoBehaviour {
	public Button badgeButton;
	public Image badgeImage;
	public Text headline;
	public Text description;
	public Text requirements;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowBadgeInformation(Sprite badge, string headline, string description, string requirements) {
		this.badgeImage.sprite = badge;
		this.headline.text = headline;
		this.description.text = description;
		this.requirements.text = requirements;
	}
}

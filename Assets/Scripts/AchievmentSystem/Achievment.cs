using UnityEngine;
using System.Collections;

public class Achievment {

	private string name;
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


	private GameObject achievmentRef;


	public Achievment (string name, string description, int spriteIndex, GameObject achievmentRef) {
		this.name = name;
		this.description = description;
		this.unlocked = false;
		this.spriteIndex = spriteIndex;
		this.achievmentRef = achievmentRef;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool EarnAchievment() {
		if (!unlocked) {
			unlocked = true;
			return true;
		}
		return false;
	}
}

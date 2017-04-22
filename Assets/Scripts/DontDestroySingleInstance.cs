using UnityEngine;
using System.Collections;

public class DontDestroySingleInstance : MonoBehaviour {
	public string tag;

	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Length: " + GameObject.FindGameObjectsWithTag (tag).Length);
		if (GameObject.FindGameObjectsWithTag (tag).Length > 1) {
			Debug.Log ("more than one instance");
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

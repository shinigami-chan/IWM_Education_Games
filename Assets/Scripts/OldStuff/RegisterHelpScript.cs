using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterHelpScript : MonoBehaviour {
	// Public variables

	public GameObject errorSignPanel;
	public Text errorSignText;

	public void ShowHelp (string text) {
		errorSignText.text = text;
		errorSignPanel.SetActive (true);
	}

	// Close Help Sign
	public void HideHelp () {
		errorSignPanel.SetActive (false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

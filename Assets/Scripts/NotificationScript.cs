using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationScript : MonoBehaviour {

	public GameObject signPanel;
	public Text signText;
	public Button exitButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowMessage(string message)
	{
		signText.text = message;
		signPanel.SetActive(true);
	}

	public void HideMessage()
	{
		signPanel.SetActive(false);
	}
}

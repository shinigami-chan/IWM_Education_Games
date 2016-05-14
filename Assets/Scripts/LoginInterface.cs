using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginInterface : MonoBehaviour {

    Button registerButton;
    Text registerButtonText;
    GameObject registerB;

	// Use this for initialization
	void Start () {

	}

    public void loadBalloonGame()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void loadNumberLineGame()
    {
        SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void Update () {
	
	}
}

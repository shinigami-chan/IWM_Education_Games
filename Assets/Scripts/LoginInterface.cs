using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginInterface : MonoBehaviour {


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

    public void OnValueChanged(int selection)
    {
        if (selection == 1)
        {
            Debug.Log("log out");
            Application.Quit();
        }
    }


    // Update is called once per frame
    void Update () {
    }

}

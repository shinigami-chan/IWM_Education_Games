using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectionScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameObject.Find("PlayerMenu").GetComponentInChildren<Text>().text = PlayerPrefs.GetString("username");
    }


    public void loadBalloonGame()
    {
        SceneManager.LoadScene("balloon_menu_scene");
    }

    public void loadNumberLineGame()
    {
        SceneManager.LoadScene("numberline_menu_scene");
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

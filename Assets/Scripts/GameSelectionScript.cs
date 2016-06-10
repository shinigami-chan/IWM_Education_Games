using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectionScript : MonoBehaviour {

    Dropdown playerMenu;

    // Use this for initialization
    void Start () {
        playerMenu = GameObject.Find("PlayerMenu").GetComponent<Dropdown>();
        playerMenu.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("username");
    }


    public void loadBalloonGame()
    {
        SceneManager.LoadScene("balloon_menu_scene");
    }

    public void loadNumberLineGame()
    {
        SceneManager.LoadScene("numberline_menu_scene");
    }

    public void OnValueChanged()
    {
        if (playerMenu.options[playerMenu.value].text.Equals("Ausloggen"))
        {
            Debug.Log("log out");
            Application.Quit();
        }
    }


    // Update is called once per frame
    void Update () {
    }

}

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
        SceneManager.LoadScene("Balloon_Menu");
    }

    public void loadNumberLineGame()
    {
        SceneManager.LoadScene("Numberline_Menu");
    }

    public void OnValueChanged()
    {
        if (playerMenu.options[playerMenu.value].text.Equals("Ausloggen"))
        {
            Debug.Log("log out");
            SceneManager.LoadScene("Login");
        }
    }


    // Update is called once per frame
    void Update () {
    }

}

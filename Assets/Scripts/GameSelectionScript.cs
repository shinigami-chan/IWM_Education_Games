using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameSelectionScript : MonoBehaviour {

    Dropdown playerMenu;

    // Use this for initialization
    void Start () {
        Debug.Log("User ID test: new Instance??");
        Debug.Log(Logger.Instance.GetUserID());

        playerMenu = GameObject.Find("PlayerMenu").GetComponent<Dropdown>();
        playerMenu.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("username");
    }


    public void loadBalloonGame()
    {
        Logger.Instance.Log(Action.CHOOSE_GAME);
        SceneManager.LoadScene("Balloon_Menu");
    }

    public void loadNumberLineGame()
    {
        Logger.Instance.Log(Action.SHOW_STATISTICS);
        SceneManager.LoadScene("Numberline_Menu");
    }

    public void OnValueChanged()
    {
        if (playerMenu.options[playerMenu.value].text.Equals("Ausloggen"))
        {
            Debug.Log("log out");

            //LOGGER
            Logger.Instance.EndSession();

            Debug.Log(Logger.Instance.GetUserID());

            //AssetDatabase.ImportAsset("Assets\\Scripts\\Logger.cs", UnityEditor.ImportAssetOptions.Default);

            //Debug.Log(Logger.Instance.GetUserID());

            Destroy(GameObject.Find("Logger"));

            SceneManager.LoadScene("Login");
        }
    }


    // Update is called once per frame
    void Update () {
    }

}

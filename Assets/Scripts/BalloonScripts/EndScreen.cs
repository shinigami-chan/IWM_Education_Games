using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    public Text endScreenText;

	void Start () {

        endScreenText = GameObject.Find("Text").GetComponent<Text>();

        endScreenText.text = "Super! Du hast " + PlayerPrefs.GetInt("PlayerPoints") + " Punkte erreicht. Willst du nochmal spielen?";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("main_menu");
    }


}

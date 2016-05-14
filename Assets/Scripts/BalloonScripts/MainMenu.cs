using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerName;

    void Start()
    {
        playerName = GameObject.Find("NameText").GetComponent<Text>();
    }


    public void OnPressEnter()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject.Find("NameField").GetComponent<InputField>().ActivateInputField();
            StartGame();
        }
    }

    public void setPlayerNameText(string inputFieldString)
    {
        playerName.text = inputFieldString;
    }

    public void StartGame()
    {
        if (playerName.text.Length >= 1)
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
            SceneManager.LoadScene("balloon_scene");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", "Spieler");
            SceneManager.LoadScene("balloon_scene");
        }
    }

    public void changeSpeed(Slider slider)
    {
        PlayerPrefs.SetInt("SpeedMode",(int)slider.value);
        Debug.Log(slider.value);
    }
}
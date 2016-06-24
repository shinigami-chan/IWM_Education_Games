using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
    }

    //public void OnPressEnter()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
    //    {
    //        StartGame();
    //    }
    //}

    public void StartGame()
    {
            SceneManager.LoadScene("Balloon_Game");
    }

    public void changeSpeed(Slider slider)
    {
        PlayerPrefs.SetInt("SpeedMode",(int)slider.value);
        Debug.Log(slider.value);
    }
}
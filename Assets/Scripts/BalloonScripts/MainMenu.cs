using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        changeSpeed(GameObject.Find("Slider").GetComponent<Slider>());
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
        Logger.Instance.GameLog(Action.START_BALLOON_GAME,PlayerPrefs.GetInt("SpeedMode"));
        SceneManager.LoadScene("Balloon_Game");
    }

    public void changeSpeed(Slider slider)
    {
        PlayerPrefs.SetInt("SpeedMode",(int)slider.value);
    }
}
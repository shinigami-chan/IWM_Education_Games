using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationSystem : MonoBehaviour {

    public Button muteButton;
    public Button unmuteButton;
    public Button returnButton;
    public Button quitButton;

    public GameObject sound;
    public GameObject music;

    public AudioSource balloonPoppingSoundSource;
    public AudioSource musicSource;

    public NavigationSystem() { }

    void Start () {
        sound = GameObject.Find("Sound");
        music = GameObject.Find("Music");

        balloonPoppingSoundSource = sound.GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();

        muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
        returnButton = GameObject.Find("ReturnButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        unmuteButton = GameObject.Find("UnmuteButton").GetComponent<Button>();
        unmuteButton.gameObject.SetActive(false);

        DontDestroyOnLoad(music);
        DontDestroyOnLoad(sound);
    }

    public void goToMainMenu()
    {
        Destroy(music);
        Destroy(sound);
        SceneManager.LoadScene("main_menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void muteSound()
    {
        musicSource.mute = true;
        balloonPoppingSoundSource.mute = true;
        muteButton.interactable = false;
        muteButton.gameObject.SetActive(false);
        unmuteButton.interactable = true;
        unmuteButton.gameObject.SetActive(true);
    }

    public void unMuteSound()
    {
        musicSource.mute = false;
        balloonPoppingSoundSource.mute = false;
        muteButton.interactable = true;
        muteButton.gameObject.SetActive(true);
        unmuteButton.interactable = false;
        unmuteButton.gameObject.SetActive(false);
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {


	void Start () {
    }

    public void OnLoginButtonClick()
    {
        string username = GameObject.Find("UsernameFieldText").GetComponent<Text>().text;
        string password = GameObject.Find("PasswordFieldText").GetComponent<Text>().text;
        StartCoroutine(CheckLogin(username, password));
    }

    public IEnumerator CheckLogin(string username, string password)
    {
        WWW db = new WWW("http://localhost/unity_games/login_user.php?username=" + username + "&password=" + password);
        yield return db;

        string[] phpOutput = db.text.Split(":"[0]);
        string phpReturn = phpOutput[1];
        

        if (phpReturn != "1")
        {
            Debug.Log("login failed");
            GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen: Falsches Passwort oder falscher Nutzername!";
        }
        else
        {
            Debug.Log("login succeeded");
            SceneManager.LoadScene("game_selection");
        }
    }


    public void OnRegisterButtonClick()
    {
        SceneManager.LoadScene("Registration_Scene");
    }
	

}

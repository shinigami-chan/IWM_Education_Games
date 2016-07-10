using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public class LoginScript : MonoBehaviour {


	void Start () {
    }

    public void OnLoginButtonClick()
    {
        string username = GameObject.Find("UsernameFieldText").GetComponent<Text>().text;
        PlayerPrefs.SetString("username",username);
        string password = GameObject.Find("PasswordField").GetComponent<InputField>().text;
        //PlayerPrefs.SetString("password",password);
        StartCoroutine(CheckLogin(username, password));
    }

    public IEnumerator CheckLogin(string username, string password)
    {
        string hashedPassword = Utilities.GetSHA256(password);

        string url = "http://localhost/unity_games/login_user.php?username=" + username + "&password=" + hashedPassword;

        WWW db = new WWW(url);

        yield return db;

        string[] phpOutput = Utilities.GetPhpOutput(db);
        
        if (phpOutput.Length == 0)
        {
            Debug.Log("database is not running");
            GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen. Es konnte nicht auf die Datenbank zugegriffen werden. Bitte überprüfe deine Internetverbindung.";
        }
        else
        {
            Debug.Log("Indexof: " + Array.IndexOf(phpOutput, "LOGIN:1"));

            if (Array.IndexOf(phpOutput,"LOGIN:1") > 0)
            {
                Debug.Log("login succeeded");
                PlayerPrefs.Save();
                SystemLog.LogAction(SystemLog.REGISTRATION);
                SceneManager.LoadScene("Game_Selection");
            }
            else
            {
                Debug.Log("login failed");
                GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen: Falsches Passwort oder falscher Nutzername!";
            }
        }
    }


    public void OnRegisterButtonClick()
    {
        SceneManager.LoadScene("Registration");
    }
	

}

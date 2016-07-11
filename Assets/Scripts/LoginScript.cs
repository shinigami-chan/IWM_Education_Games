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

        //string url = "http://localhost/unity_games/login_user.php?username=" + username + "&password=" + hashedPassword;
        string url = RegisterScript.SERVER + "login_user.php?username=" + username + "&password=" + hashedPassword;

        Debug.Log(url);

        WWW db = new WWW(url);

        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db, true);

        Debug.Log("hallo");

        if (!handler.Connection())
        {
            Debug.Log("database is not running");
            GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen. Es konnte nicht auf die Datenbank zugegriffen werden. Bitte überprüfe deine Internetverbindung.";
        }
        else if (handler.Success())
        {
            if (handler.GetOutput().ContainsKey("SUCCESS") && handler.GetOutput()["SUCCESS"] == "1")
            {
                Debug.Log("login succeeded");
                PlayerPrefs.Save();
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

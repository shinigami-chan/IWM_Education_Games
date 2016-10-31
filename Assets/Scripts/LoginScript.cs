using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;

public class LoginScript : MonoBehaviour {
    public GameObject errorSignPanel;
    public Text errorSignText;

	void Start () {
    }

    public void ShowErrorMessage(String errorMessage)
    {
        errorSignText.text = errorMessage;
        errorSignPanel.SetActive(true);
    }

    public void HideErrorMessage()
    {
        errorSignPanel.SetActive(false);
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

        if (!handler.Connection())
        {
            Debug.Log("database is not running");
            ShowErrorMessage("Es gibt keine Verbindung zum Internet. Bitte stelle sicher dass dein Gerät mit dem Internet verbunden ist.");
            //GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen. Es konnte nicht auf die Datenbank zugegriffen werden. Bitte überprüfe deine Internetverbindung.";
        }
        else if (handler.Success())
        {
            Debug.Log("Connection is stable");
            if (handler.GetOutput().ContainsKey("SUCCESS") && handler.GetOutput()["SUCCESS"] == "1")
            {
                Debug.Log("login succeeded");
                PlayerPrefs.Save();
                SceneManager.LoadScene("Game_Selection_Android");
                //SceneManager.LoadScene("Game_Selection");

                if (handler.GetOutput().ContainsKey("USER_ID"))
                    Logger.Instance.SetUserID(Int32.Parse(handler.GetOutput()["USER_ID"]));
                Logger.Instance.StartSession();
            }
        }
        else
        {
            Debug.Log("login failed");
            ShowErrorMessage("Anmeldung ist fehlgeschlagen: Entweder ist dein Nutzername oder dein Passwort falsch!");
            //GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen: Falsches Passwort oder falscher Nutzername!";
        }
    }


    public void OnRegisterButtonClick()
    {
        SceneManager.LoadScene("Registration_Android");
    }
	

}

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
        PlayerPrefs.SetString("username",username);
        string password = GameObject.Find("PasswordField").GetComponent<InputField>().text;
        //PlayerPrefs.SetString("password",password);
        StartCoroutine(CheckLogin(username, password));
    }

    public IEnumerator CheckLogin(string username, string password)
    {
        WWW db = new WWW("http://localhost/unity_games/login_user.php?username=" + username + "&password=" + password);
        yield return db;
        
        string[] phpOutput = db.text.Split(":"[0]);
        if (phpOutput[0].Equals(""))
        {
            Debug.Log("database is not running");
            GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen. Es konnte nicht auf die Datenbank zugegriffen werden. Bitte überprüfe deine Internetverbindung.";
        }
        else
        {
            string phpReturn = phpOutput[1];
            if (phpReturn != "1")
            {
                Debug.Log("login failed");
                GameObject.Find("LoginFailText").GetComponent<Text>().text = "Anmeldung ist fehlgeschlagen: Falsches Passwort oder falscher Nutzername!";
            }
            else
            {
                Debug.Log("login succeeded");
                PlayerPrefs.Save();
                SceneManager.LoadScene("game_selection_scene");
            }
        }
    }


    public void OnRegisterButtonClick()
    {
        SceneManager.LoadScene("Registration_Scene");
    }
	

}

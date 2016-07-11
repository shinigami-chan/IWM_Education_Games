using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RegisterScript : MonoBehaviour {
    private bool usernameValid;
    private RegisterSceneController controller;
    public static readonly string SERVER = "http://lernplattform.iwm-kmrc.de/php_scripts/";


    // Use this for initialization
    void Start () {
        controller = GameObject.Find("RegistrationPanel").GetComponent<RegisterSceneController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator RegisterWorker()
    {
        // Extract hashed passwort from password field
        string username = Utilities.PercentEncode(GameObject.Find("UsernameField").GetComponent<InputField>().text);
        string hashedPw = Utilities.GetSHA256(GameObject.Find("PasswordField").GetComponent<InputField>().text);
        string password = Utilities.PercentEncode(hashedPw);

        // Prepare url with ref to the register script and the given parameters
        string url = SERVER + "register_user.php?";

        string usernameString = "username=" + username;
        string passwordString = "&password=" + password;

        url += usernameString;
        url += passwordString;

        if (controller.HasEmail())
        {
            string email = Utilities.PercentEncode(GameObject.Find("EmailField").GetComponent<InputField>().text);
            url += "&email=" + email;
        }
        if (controller.HasSex())
        {
            string sex = ExtractGenderFromToggle(
                    GameObject.Find("MaleToggle").GetComponent<Toggle>() as Toggle,
                    GameObject.Find("FemaleToggle").GetComponent<Toggle>() as Toggle);
            url += "&sex=" + sex;
        }
        if (controller.HasAge())
        {
            string age = controller.GetAge();
            url += "&age=" + age;
        }
        if (controller.HasSchoolType())
        {
            string school = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().options[GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().value].text));
            url += "&school=" + school;
        }
        if (controller.HasState())
        {
            string state = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("StateDropdown").GetComponent<Dropdown>().options[GameObject.Find("StateDropdown").GetComponent<Dropdown>().value].text));
            url += "&state=" + state;
        }
        if (controller.HasNativeLanguage())
        {
            string nativeLanguage = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().options[GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().value].text));
            url += "&native_language=" + nativeLanguage;
        }

        Debug.Log(url);

        WWW db = new WWW(url);

        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db, true);

        if (handler.Success())
        {
            // Registration was successful
            SceneManager.LoadScene("Login");
        }
        else
        {
            // Registration was unsuccessful
            // Do nothing (eventually show notification)
        }
    }
    
    public IEnumerator RegisterCoroutine()
    {
        string username = GameObject.Find("UsernameField").GetComponent<InputField>().text;
        string url = SERVER + "is_name_taken.php?username=" + username;

        //Debug.Log(url);

        WWW itemsData = new WWW(url);

        yield return itemsData;

        PhpOutputHandler handler = new PhpOutputHandler(itemsData, true);

        Debug.Log("loggable");
        Debug.Log(controller.IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField")));

        if (handler.Success() && controller.IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField")))
        {
            if (handler.GetOutput().ContainsKey("USERNAME_TAKEN") && handler.GetOutput()["USERNAME_TAKEN"].Equals("0"))
            {
                // Username is free
                Debug.Log("Username is free");
                StartCoroutine(RegisterWorker());
            }
            else
            {
                // Username is taken
                Debug.Log("Username is taken");
                controller.UpdateValidity(GameObject.Find("UsernameField"), false);
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterCoroutine());
    }

    private static string ExtractGenderFromToggle(Toggle maleToggle, Toggle femaleToggle)
    {
        if (maleToggle.isOn && !femaleToggle.isOn)
        {
            return "M";
        }
        else if (!maleToggle.isOn && femaleToggle.isOn)
        {
            return "W";
        }
        else
        {
            return "";
        }
    }

    public static string GetSHA256(string text)
    {
        byte[] byteRepresentation = Encoding.UTF8.GetBytes(text);

        SHA256Managed hashstring = new SHA256Managed();
        byte[] hash = hashstring.ComputeHash(byteRepresentation);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += string.Format("{0:x2}", x);
        }
        return hashString;
    }
}

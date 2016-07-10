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
    

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("RegistrationPanel").GetComponent<RegisterSceneController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator OnRegisterButtonClick()
    {
        // Extract hashed passwort from password field
        string username = Utilities.PercentEncode(GameObject.Find("UsernameField").GetComponent<InputField>().text);
        string hashedPw = Utilities.GetSHA256(GameObject.Find("PasswordField").GetComponent<InputField>().text);
        string password = Utilities.PercentEncode(hashedPw);

        // Prepare url with ref to the register script and the given parameters
        string url = "http://localhost/unity_games/register_user.php?";

        string usernameString = "username=" + username;
        string passwordString = "&password=" + password;

        url += usernameString;
        url += passwordString;

        if (controller.hasEmail())
        {
            string email = Utilities.PercentEncode(GameObject.Find("EmailField").GetComponent<InputField>().text);
            url += "&email=" + email;
        }
        if (controller.hasSex())
        {
            string sex = ExtractGenderFromToggle(
                    GameObject.Find("MaleToggle").GetComponent<Toggle>() as Toggle,
                    GameObject.Find("FemaleToggle").GetComponent<Toggle>() as Toggle);
            url += "&sex=" + sex;
        }
        if (controller.hasAge())
        {
            string age = RegisterSceneController.getSelectedItemFromDropdown(GameObject.Find("AgeDropdown").GetComponent<Dropdown>());
            url += "&age=" + age;
        }
        if (controller.hasSchoolType())
        {
            string school = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().options[GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().value].text));
            url += "&school=" + school;
        }
        if (controller.hasState())
        {
            string state = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("StateDropdown").GetComponent<Dropdown>().options[GameObject.Find("StateDropdown").GetComponent<Dropdown>().value].text));
            url += "&state=" + state;
        }
        if (controller.hasNativeLanguage())
        {
            string nativeLanguage = Utilities.PercentEncode(Utilities.ReplaceUmlautForPhp(GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().options[GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().value].text));
            url += "&native_language=" + nativeLanguage;
        }
        
        WWW db = new WWW(url);

        yield return db;

        if (db.text != "1")
        {

        }
        else
        {
            SceneManager.LoadScene("Login");
            //print("success");
        }
    }

    public IEnumerator RegisterIfUsernameIsFree(string username)
    {
        Debug.Log("load Data");
        WWW itemsData = new WWW("http://localhost/unity_games/is_name_taken.php?username=" + username);

        yield return itemsData;

        if (controller.IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField")))
        {
            Debug.Log("Text: " + itemsData.text);
            if (itemsData.text.Equals("0"))
            {
                Debug.Log("IS FREE");
                StartCoroutine(OnRegisterButtonClick());
            }
            else
                controller.UpdateValidity(GameObject.Find("UsernameField"), false);
        }
    }

    public void RegisterCoroutine()
    {
        StartCoroutine(OnRegisterButtonClick());
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

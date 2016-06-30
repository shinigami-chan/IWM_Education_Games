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
        string username = PercentEncode(GameObject.Find("UsernameField").GetComponent<InputField>().text);
        string password = PercentEncode(GameObject.Find("PasswordField").GetComponent<InputField>().text);
        string email = PercentEncode(GameObject.Find("EmailField").GetComponent<InputField>().text);
        string sex = ExtractGenderFromToggle(
            GameObject.Find("MaleToggle").GetComponent<Toggle>() as Toggle,
            GameObject.Find("FemaleToggle").GetComponent<Toggle>() as Toggle);
        string age = RegisterSceneController.getSelectedItemFromDropdown(GameObject.Find("AgeDropdown").GetComponent<Dropdown>());
        string school = PercentEncode(ReplaceUmlautForPhp(GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().options[GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().value].text));
        string state = PercentEncode(ReplaceUmlautForPhp(GameObject.Find("StateDropdown").GetComponent<Dropdown>().options[GameObject.Find("StateDropdown").GetComponent<Dropdown>().value].text));
        string nativeLanguage = PercentEncode(ReplaceUmlautForPhp(GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().options[GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>().value].text));

        string url = "http://localhost/unity_games/register_user.php?";

        string usernameString = "username=" + username;
        string passwordString = "&password=" + GetSHA256(password);

        url += usernameString;
        url += passwordString;

        if (controller.hasEmail())
            url += "&email=" + email;
        if (controller.hasSex())
            url += "&sex=" + sex;
        if (controller.hasAge())
            url += "&age=" + age;
        if (controller.hasSchoolType())
            url += "&school=" + school;
        if (controller.hasState())
            url += "&state=" + state;
        if (controller.hasNativeLanguage())
            url += "&native_language=" + nativeLanguage;

        Debug.Log("URL: " + url);
        WWW db = new WWW(url);

        yield return db;

        Debug.Log("Text: " + db.text);

        if (db.text != "1")
        {
            print("failed");
        }
        else
        {
            SceneManager.LoadScene("login_scene");
            print("success");
        }
    }

    public static string ReplaceUmlautForPhp(string s)
    {
        return s.Replace("ü", "' || U&\'\\00FC\' || \'").Replace("ö", "' || U&26\'\\00F6\' || \'").Replace("ä", "' || U&26\'\\00F4\' || \'");

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

    public static string PercentEncode(string s)
    {
        Debug.Log("string before percent encoding:\n " + s);
        Dictionary<string, string> percentEncodings = new Dictionary<string,string>();
        percentEncodings.Add(" ", "%20");
        percentEncodings.Add("!", "%21");
        percentEncodings.Add("\"", "%22");
        percentEncodings.Add("#", "%23");
        percentEncodings.Add("$", "%24");
        //percentEncodings.Add("%", "%25");
        percentEncodings.Add("&", "%26");
        percentEncodings.Add("'", "%27");
        percentEncodings.Add("(", "%28");
        percentEncodings.Add(")", "%29");
        percentEncodings.Add("*", "%2A");
        percentEncodings.Add("+", "%2B");
        percentEncodings.Add(",", "%2C");
        percentEncodings.Add("/", "%2F");
        percentEncodings.Add(":", "%3A");
        percentEncodings.Add(";", "%3B");
        percentEncodings.Add("=", "%3D");
        percentEncodings.Add("?", "%3F");
        percentEncodings.Add("@", "%40");
        percentEncodings.Add("[", "%5B");
        percentEncodings.Add("\\", "%5C");
        percentEncodings.Add("]", "%5D");
        percentEncodings.Add("{", "%7B");
        percentEncodings.Add("|", "%7C");
        percentEncodings.Add("}", "%7D");

        s = s.Replace("%", "%25");

        foreach (KeyValuePair<string, string> keyValue in percentEncodings)
            s = s.Replace(keyValue.Key, keyValue.Value);
        Debug.Log("string after percent encoding:\n " + s);
        return s;
    }
}

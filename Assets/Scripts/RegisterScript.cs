using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour {
    private bool usernameValid;
    

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator OnRegisterButtonClick()
    {
        string username = GameObject.Find("UsernameFieldText").GetComponent<Text>().text;
        string password = GameObject.Find("PasswordField").GetComponent<InputField>().text;
        string sex = ExtractGenderFromToggle(
            GameObject.Find("MaleToggle").GetComponent<Toggle>() as Toggle,
            GameObject.Find("FemaleToggle").GetComponent<Toggle>() as Toggle);
        string age = GameObject.Find("AgeFieldText").GetComponent<Text>().text;
        string school = GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().options[GameObject.Find("SchoolDropdown").GetComponent<Dropdown>().value].text;
        string state = GameObject.Find("StateDropdown").GetComponent<Dropdown>().options[GameObject.Find("StateDropdown").GetComponent<Dropdown>().value].text;
        string mothertongue = GameObject.Find("MothertongueFieldText").GetComponent<Text>().text;


        WWW db = new WWW("http://localhost/unity_games/register_user.php?username=" + username + "&password=" + GetSHA256(password) + "&gender=" + sex + "&age=" + age + "&school=" + school + "&state=" + state + "&mothertongue=" + mothertongue);
        yield return db;

        if (db.text != "success")
        {
            print("failed");
        }
        else
        {
            print("success");
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

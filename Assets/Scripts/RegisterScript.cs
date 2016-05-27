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

    public void OnRegisterButtonClick()
    {
        string username = GameObject.Find("UsernameFieldText").GetComponent<Text>().text;
        string password = GameObject.Find("PasswordFieldText").GetComponent<Text>().text;
        string sex = ExtractGenderFromToggle(
            GameObject.Find("MaleToggle").GetComponent<Toggle>() as Toggle,
            GameObject.Find("FemaleToggle").GetComponent<Toggle>() as Toggle);
        string age = GameObject.Find("AgeFieldText").GetComponent<Text>().text;
        string school = GameObject.Find("SchoolFieldText").GetComponent<Text>().text;
        string state = GameObject.Find("StateFieldText").GetComponent<Text>().text;
        string mothertongue = GameObject.Find("MothertongueFieldText").GetComponent<Text>().text;
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

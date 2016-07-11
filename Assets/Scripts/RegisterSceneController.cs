using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class RegisterSceneController : MonoBehaviour {
    // Set background color of inputfields with valid input
    private static readonly Color Valid = new Color(0.733f, 0.867f, 0.734f, 1f);
    // Set background color of inputfields with invalid input
    private static readonly Color Invalid = new Color(0.867f, 0.733f, 0.733f, 1f);

    // The default value for the dropdown menues
    private static readonly string DefaultDropdownValue = "Bitte wählen";

    // Variables that define min and max lengths as well as regular expressions 
    // for validating input of the username and password input field
    private static readonly int UsernameMaxLength = 12;
    private static readonly int UsernameMinLength = 6;
    private static readonly int PasswordMaxLength = 16;
    private static readonly int PasswordMinLength = 6;
    private static readonly string UsernameRegex = @"[0-9a-zA-Z]*";
    private static readonly string PasswordRegex = @".*";
    private static readonly string EmailRegex = @".+@.+\..+";

    // Load reference to register script to access
    // the register logic (database etc)
    private RegisterScript regScript;

    // create grade lists for different types of schools 
    private List<Dropdown.OptionData>
        defaultList,
        grundschule,
        hauptschule,
        realschule,
        gymnasium,
        gesamtschule,
        foerderschule;


    // Use this for initialization
    void Start () {
        regScript = GameObject.Find("RegistrationPanel").GetComponent<RegisterScript>();
        InitDropdown();
    }

    // add grades for the lists for different types of schools
    private void InitDropdown()
    {
        defaultList = generateGradeList(0, 0);
        grundschule = generateGradeList(1, 4);
        hauptschule = generateGradeList(5, 9);
        realschule = generateGradeList(5, 10);
        gymnasium = generateGradeList(5, 13);
        gesamtschule = generateGradeList(5, 13);
        foerderschule = generateGradeList(5, 12);
    }

    private List<Dropdown.OptionData> generateGradeList(int from, int to)
    {
        List<Dropdown.OptionData> gradeList = new List<Dropdown.OptionData>();
        gradeList.Add(new Dropdown.OptionData(DefaultDropdownValue));
        for (int i = from; i <= to; i++) {
            gradeList.Add(new Dropdown.OptionData(i.ToString()));
        }
        return gradeList;
    }

    /**
     * 
     */ 
    public bool isGradeSelected()
    {
        Dropdown gradeDropdown = GameObject.Find("GradeDropdown").GetComponent<Dropdown>();
        return !(gradeDropdown.options[gradeDropdown.value].text.Equals("Bitte wählen"));
    }

    public void SetGradeDropdownItems()
    {
        Dropdown schoolDropdown = GameObject.Find("SchoolDropdown").GetComponent<Dropdown>();
        Dropdown gradeDropdown = GameObject.Find("GradeDropdown").GetComponent<Dropdown>();
        string selectedSchool = schoolDropdown.options[schoolDropdown.value].text;

        gradeDropdown.ClearOptions();
        gradeDropdown.interactable = true;

        switch (selectedSchool)
        {
            case "Bitte wählen":
                gradeDropdown.AddOptions(defaultList);
                gradeDropdown.interactable = false;
                break;
            case "Grundschule":
                gradeDropdown.AddOptions(grundschule);
                break;
            case "Hauptschule":
                gradeDropdown.AddOptions(hauptschule);
                break;
            case "Realschule":
                gradeDropdown.AddOptions(realschule);
                break;
            case "Gymnasium":
                gradeDropdown.AddOptions(gymnasium);
                break;
            case "Förderschule":
                gradeDropdown.AddOptions(foerderschule);
                break;
            case "Gesamtschule":
                gradeDropdown.AddOptions(gesamtschule);
                break;
            default:
                break;
        }
    }

    public void CheckUsername(GameObject input)
    {
        UpdateValidity(input, ValidateInput(input, UsernameRegex, UsernameMinLength, UsernameMaxLength));
    }

    public void CheckPassword(GameObject input)
    {
        UpdateValidity(input, ValidateInput(input, PasswordRegex, PasswordMinLength, PasswordMaxLength));
    }

    public void CheckPasswordRepeat(GameObject passwordRepeat)
    {
        GameObject password = GameObject.Find("PasswordField");
        UpdateValidity(passwordRepeat, IsContentEqual(passwordRepeat, password));
    }

    public bool IsContentEqual(GameObject input1, GameObject input2)
    {
        string text1 = input1.GetComponent<InputField>().text;
        string text2 = input2.GetComponent<InputField>().text;
        if (text1 != null && text2 != null && text1.Equals(text2))
            return true;
        return false;
    }

    public bool ValidateInput(GameObject input, string regex, int minLength, int maxLength)
    {
        Debug.Log(input == null);
        if (input.GetComponent<InputField>() != null)
        {
            string username = input.GetComponent<InputField>().text;
            if (Regex.IsMatch(username, UsernameRegex) && username.Length >= minLength && username.Length <= maxLength)
                return true;
            return false;
        }
        return false;
    }

    public void UpdateValidity(GameObject input, bool isValid)
    {
        Image background = input.GetComponent("Image") as Image;
        if (background != null)
        {
            if (isValid) background.color = Valid;
            else background.color = Invalid;
        }

    }

    public bool HasEmail()
    {
        if (GameObject.Find("EmailField").GetComponent<InputField>().text.Equals(""))
            return false;
        return true;
    }

    public bool HasAge()
    {
        if (getSelectedItemFromDropdown(GameObject.Find("AgeDropdown").GetComponent<Dropdown>()).Equals(DefaultDropdownValue))
            return false;
        return true;
    }

    public string GetAge()
    {
        string value = getSelectedItemFromDropdown(GameObject.Find("AgeDropdown").GetComponent<Dropdown>());
        if (value.Contains(">"))
        {
            return System.Int32.MaxValue.ToString();
        }
        return value;
    }

    public bool HasSex()
    {
        if (GameObject.Find("MaleToggle").GetComponent<Toggle>().isOn ||
            GameObject.Find("FemaleToggle").GetComponent<Toggle>().isOn)
            return true;
        return false;
    }

    public bool HasSchoolType()
    {
        if (getSelectedItemFromDropdown(GameObject.Find("SchoolDropdown").GetComponent<Dropdown>()).Equals(DefaultDropdownValue))
            return false;
        return true;
    }

    public bool HasGrade()
    {
        if (getSelectedItemFromDropdown(GameObject.Find("GradeDropdown").GetComponent<Dropdown>()).Equals(DefaultDropdownValue))
            return false;
        return true;
    }

    public bool HasState()
    {
        if (getSelectedItemFromDropdown(GameObject.Find("StateDropdown").GetComponent<Dropdown>()).Equals(DefaultDropdownValue))
            return false;
        return true;
    }

    public bool HasNativeLanguage()
    {
        if (getSelectedItemFromDropdown(GameObject.Find("MothertongueDropdown").GetComponent<Dropdown>()).Equals(DefaultDropdownValue))
            return false;
        return true;
    }

    public static string getSelectedItemFromDropdown(Dropdown dropdown)
    {
        return dropdown.options[dropdown.value].text;
    }
    
    /**
     * 
     */
    public void Register()
    {
        regScript.Register();
    }
    

	// Update is called once per frame
	void Update () {
        //nothing happens here
	}
}

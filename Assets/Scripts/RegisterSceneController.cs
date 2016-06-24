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
        InitDropdown();
    }

    // add grades for the lists for different types of schools
    private void InitDropdown()
    {
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
        Debug.Log(selectedSchool.Equals("Förderschule"));

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
                Debug.Log("set förderschule for grade dropdown: Förderschule");
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


    public IEnumerator RegisterIfUsernameIsFree(string username)
    {
        Debug.Log("load Data");
        WWW itemsData = new WWW("http://localhost/unity_games/users_data.php?field=" + username);

        yield return itemsData.text;

        if (IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField")))
        {
            if (itemsData.text.Equals("1"))
            {
                StartCoroutine(regScript.OnRegisterButtonClick());
            }
            else
                UpdateValidity(GameObject.Find("UsernameField"), false);
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterIfUsernameIsFree(GameObject.Find("UsernameField").GetComponent<InputField>().text));
    }
    

	// Update is called once per frame
	void Update () {

	}

    /*    public void HelpTextOnFocus()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Invoke HelpTextOnFocus() with " + name);
        if (gameObjectMap.ContainsKey(name))
        {
            Debug.Log("Map contains key " + name + ". Show help text");
            Debug.Log(gameObjectMap[name]);
            if (gameObjectMap.Equals(null)) Debug.Log("nullinger");
            gameObjectMap[name].SetActive(true);
            if (gameObjectMap[name].activeSelf == true)
            {
                Debug.Log("Is active!");
                print("works maybe");
            }
        }
    }

    public void DisableHelpText()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Invole DisableHelpText() with " + name);
        if (gameObjectMap.ContainsKey(name))
        {
            Debug.Log("Map contains key" + name);
            Debug.Log("Deselect object");
            gameObjectMap[name].SetActive(false);
            if (gameObjectMap[name].activeSelf == true)
            {
                Debug.Log("Is active!");
                print("works maybe");
            }
        }
    }*/

    /*public void initFieldToBackgroundMap()
    {

        Debug.Log("Start Script:  FocusInputFieldScript");
        if (gameObjectMap.Keys.Count <= 0)
        {
            gameObjectMap.Add("UsernameField", GameObject.Find("UsernameHelpBackground"));
            gameObjectMap.Add("PasswordField", GameObject.Find("PasswordHelpBackground"));
            gameObjectMap.Add("PasswordRepeatField", GameObject.Find("PasswordRepeatHelpBackground"));
            gameObjectMap.Add("AgeField", GameObject.Find("AgeHelpBackground"));
            gameObjectMap.Add("MaleToggle", GameObject.Find("GenderHelpBackground"));
            gameObjectMap.Add("FemaleToggle", GameObject.Find("GenderHelpBackground"));
            gameObjectMap.Add("GradeDropdown", GameObject.Find("GradeHelpBackground"));
            gameObjectMap.Add("SchoolDropdown", GameObject.Find("SchoolHelpBackground"));
            gameObjectMap.Add("StateDropdown", GameObject.Find("StateHelpBackground"));
            gameObjectMap.Add("MotherTongueField", GameObject.Find("MotherTongueHelpBackground"));
        }

        foreach (KeyValuePair<string, GameObject> kvp in gameObjectMap)
        {
            if (kvp.Value != null)
            {
                kvp.Value.SetActive(false);
            }
        }

    }*/
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class FocusOnInputFieldScript : MonoBehaviour {
    public static Dictionary<string, GameObject> gameObjectMap =
            new Dictionary<string, GameObject>();
    public static Color valid = new Color(0.733f, 0.867f, 0.734f, 1f);
    public static Color invalid = new Color(0.867f, 0.733f, 0.733f, 1f);

    public static int usernameMaxLength = 12;
    public static int usernameMinLength = 6;
    public static int passwordMaxLength = 12;
    public static int passwordMinLength = 6;
    public static string usernameRegex = @"[0-9a-zA-Z]*";

    private List<Dropdown.OptionData>
        defaultList = new List<Dropdown.OptionData>(),
        elementary = new List<Dropdown.OptionData>(),
        hauptschule = new List<Dropdown.OptionData>(),
        realschule = new List<Dropdown.OptionData>();


    // Use this for initialization
    void Start () {

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

        InitDropdown();
    }

    private void InitDropdown()
    {
        elementary.Add(new Dropdown.OptionData("Bitte wählen"));
        elementary.Add(new Dropdown.OptionData("1"));
        elementary.Add(new Dropdown.OptionData("2"));
        elementary.Add(new Dropdown.OptionData("3"));
        elementary.Add(new Dropdown.OptionData("4"));

        hauptschule.Add(new Dropdown.OptionData("Bitte wählen"));
        hauptschule.Add(new Dropdown.OptionData("5"));
        hauptschule.Add(new Dropdown.OptionData("6"));
        hauptschule.Add(new Dropdown.OptionData("7"));
        hauptschule.Add(new Dropdown.OptionData("8"));
        hauptschule.Add(new Dropdown.OptionData("9"));

        realschule.Add(new Dropdown.OptionData("Bitte wählen"));
        realschule.Add(new Dropdown.OptionData("5"));
        realschule.Add(new Dropdown.OptionData("6"));
        realschule.Add(new Dropdown.OptionData("7"));
        realschule.Add(new Dropdown.OptionData("8"));
        realschule.Add(new Dropdown.OptionData("9"));
        realschule.Add(new Dropdown.OptionData("10"));
    }

    public void SetGradeDropdownItems()
    {
        Dropdown schoolDropdown = GameObject.Find("SchoolDropdown").GetComponent<Dropdown>();
        Dropdown gradeDropdown = GameObject.Find("GradeDropdown").GetComponent<Dropdown>();
        string selectedSchool = schoolDropdown.options[schoolDropdown.value].text;
        Debug.Log(selectedSchool);

        gradeDropdown.ClearOptions();
        gradeDropdown.interactable = true;

        switch (selectedSchool)
        {
            case "Bitte wählen":
                gradeDropdown.AddOptions(defaultList);
                gradeDropdown.interactable = false;
                break;
            case "Grundschule":
                gradeDropdown.AddOptions(elementary);
                break;
            case "Hauptschule":
                gradeDropdown.AddOptions(hauptschule);
                break;
            case "Realschule":
                gradeDropdown.AddOptions(realschule);
                break;
            default:
                break;
        }
    }

    public void HelpTextOnFocus()
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
    }

    public void CheckUsername(GameObject text)
    {
        Image background = GameObject.Find("UsernameField").GetComponent("Image") as Image;
        Debug.Log("Check Username Input");
        string username = GameObject.Find("UsernameFieldText").GetComponent<Text>().text;
        Debug.Log(username + " : " + username.Length);

        if (Regex.IsMatch(username, usernameRegex) && username.Length >= usernameMinLength && username.Length <= usernameMaxLength) {
            background.color = valid;
            Debug.Log("TrueTrudi");
        }
        else
        {
            background.color = invalid;
        }

    }

	// Update is called once per frame
	void Update () {

	}
}

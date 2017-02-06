using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System;

public class RegisterController : MonoBehaviour {
    // Public variables

	public InputField usernameLoginField;
	public InputField passwordLoginField;

    public InputField usernameField;
    public InputField emailField;
    public InputField passwordField;
    public InputField passwordRepeatField;

	public Dropdown schoolDropdown;
	public Dropdown gradeDropdown;

	public Dropdown dayDropdown;
	public Dropdown monthDropdown;
	public Dropdown yearDropdown;

	public Dropdown genderDropdown;
	public Dropdown stateDropdown;
	public Dropdown languageDropdown;

	public GameObject login;
	public GameObject register1;
	public GameObject register2;


	// Default values for dropdowns
	private const string SCHOOL_DEFAULT = "Auf welche Schule gehst du?";

	private const string DAY_DEFAULT = "Tag";
	private const string MONTH_DEFAULT = "Monat";
	private const string YEAR_DEFAULT = "Jahr";

	private const string GRADE_DEFAULT = "In welche Klasse gehst du?";
	private const string GENDER_DEFAULT = "Bist du ein Junge oder ein Mädchen?";
	private const string STATE_DEFAULT = "In welchem Bundesland wohnst du?";
	private const string LANGUAGE_DEFAULT = "Wähle eine Sprache aus!";

	// String constants for the different school types in the school dropdown
	private const string GRUNDSCHULE = "Grundschule";
	private const string HAUPTSCHULE = "Hauptschule";
	private const string REALSCHULE = "Realschule";
	private const string GYMNASIUM = "Gymnasium";
	private const string GESAMTSCHULE = "Gesamtschule";
	private const string FOERDERSCHULE = "Förderschule";


    // Set background color of inputfields with valid input
    private static readonly Color Valid = new Color(0.733f, 0.867f, 0.734f, 1f);
    // Set background color of inputfields with invalid input
    private static readonly Color Invalid = new Color(0.867f, 0.733f, 0.733f, 1f);

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
	private NotificationScript notificationScript;
	private Navigation navigation;

    // create grade lists for different types of schools 
    private List<Dropdown.OptionData>
        defaultList,
        grundschule,
        hauptschule,
        realschule,
        gymnasium,
        gesamtschule,
        foerderschule;




	// ********************************************************************************
	// Start & Update
	// ********************************************************************************


    // Use this for initialization
    void Start () {
		notificationScript = transform.GetComponent<NotificationScript> ();
		navigation = transform.GetComponent<Navigation> ();
		AchievementManager.Instance.Load ();

		InitAgeDropdown ();
		InitSchoolDropdown ();
		InitGenderDropdown ();
		InitGradeDropdown ();
		InitStateDropdown ();
		InitLanguageDropdown ();
		SetGradeDropdownItems ();

		if (Logger.Instance.IsSessionRunning ())
			navigation.ShowGameSelection ();
    }

	// Update is called once per frame
	void Update () {
		//nothing happens here
	}


	// ********************************************************************************
	// Initialize Dropdowns
	// ********************************************************************************

	/// <summary>
	/// Generates the option data list for a Dropdown menu.
	/// </summary>
	/// <returns>The option data list.</returns>
	/// <param name="args">Arguments.</param>
	private List<Dropdown.OptionData> GenerateOptionDataList(params string[] args) {
		List<Dropdown.OptionData> result = new List<Dropdown.OptionData> ();
		foreach (string arg in args) {
			result.Add(new Dropdown.OptionData(arg));
		}
		return result;
	}

	/// <summary>
	/// Initiates the school dropdown
	/// </summary>
	private void InitSchoolDropdown() {
		schoolDropdown.ClearOptions ();
		schoolDropdown.AddOptions (GenerateOptionDataList (
			SCHOOL_DEFAULT,
			GRUNDSCHULE,
			HAUPTSCHULE,
			REALSCHULE,
			GYMNASIUM,
			GESAMTSCHULE,
			FOERDERSCHULE));
	}

	/// <summary>
	/// Initiates the age dropdown
	/// - dayDropdown
	/// - monthDropdown
	/// - yearDropdown
	/// </summary>
	private void InitAgeDropdown() {
		dayDropdown.ClearOptions ();
		monthDropdown.ClearOptions ();
		yearDropdown.ClearOptions ();

		dayDropdown.AddOptions (generateNumberList (DAY_DEFAULT ,1, 31));
		monthDropdown.AddOptions (generateNumberList (MONTH_DEFAULT, 1, 12));
		yearDropdown.AddOptions (generateNumberList (YEAR_DEFAULT, 1990, 2015));
	}

	/// <summary>
	/// Initiates the state dropdown.
	/// </summary>
	private void InitStateDropdown() {
		stateDropdown.ClearOptions ();
		stateDropdown.AddOptions (GenerateOptionDataList (
			STATE_DEFAULT,
			"Baden-Württemberg",
			"Bayern",
			"Berlin",
			"Brandenburg",
			"Bremen",
			"Hamburg",
			"Hessen",
			"Mecklenburg-Vorpommern",
			"Niedersachsen",
			"Nordrhein-Westfalen",
			"Rheinland-Pfalz",
			"Saarland",
			"Sachsen",
			"Sachsen-Anhalt",
			"Schleswig-Holstein",
			"Thüringen"));
	}


	/// <summary>
	/// Initates the gender dropdown.
	/// </summary>
	private void InitGenderDropdown() {
		genderDropdown.ClearOptions ();
		genderDropdown.AddOptions (GenerateOptionDataList (
			GENDER_DEFAULT,
			"Junge",
			"Mädchen"));
	}


	/// <summary>
	/// Initiates the grade dropdown for the respective school types
	/// </summary>
	private void InitGradeDropdown()
	{
		defaultList = generateNumberList(GRADE_DEFAULT, 0, 0);
		grundschule = generateNumberList(GRADE_DEFAULT, 1, 4);
		hauptschule = generateNumberList(GRADE_DEFAULT, 5, 9);
		realschule = generateNumberList(GRADE_DEFAULT, 5, 10);
		gymnasium = generateNumberList(GRADE_DEFAULT, 5, 13);
		gesamtschule = generateNumberList(GRADE_DEFAULT, 5, 13);
		foerderschule = generateNumberList(GRADE_DEFAULT , 5, 12);
	}


	/// <summary>
	/// Initiates the language dropdown.
	/// </summary>
	private void InitLanguageDropdown() {
		languageDropdown.ClearOptions ();
		languageDropdown.AddOptions (GenerateOptionDataList (
			LANGUAGE_DEFAULT,
			"Arabisch",
			"Bengalisch",
			"Chinesisch",
			"Deutsch",
			"Englisch",
			"Französisch",
			"Hindi",
			"Japanisch",
			"Javanisch",
			"Koreanisch",
			"Lahnda",
			"Marathi",
			"Portugiesisch",
			"Russisch",
			"Spanisch",
			"Tamil",
			"Telugu",
			"Türkisch",
			"Urdu",
			"Vietnamesisch"));
	}


	/// <summary>
	/// Generates the grade list.
	/// </summary>
	/// <returns>The grade list.</returns>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	private List<Dropdown.OptionData> generateNumberList(string defaultString, int from, int to)
	{
		List<Dropdown.OptionData> gradeList = new List<Dropdown.OptionData>();

		gradeList.Add(new Dropdown.OptionData(defaultString));
		for (int i = from; i <= to; i++) {
			gradeList.Add(new Dropdown.OptionData(i.ToString()));
		}
		return gradeList;
	}


	/// <summary>
	/// Sets the grade dropdown items according to the selected school.
	/// </summary>
	public void SetGradeDropdownItems()
	{
		string selectedSchool = schoolDropdown.options[schoolDropdown.value].text;

		gradeDropdown.ClearOptions();
		gradeDropdown.interactable = true;

		switch (selectedSchool)
		{
		case SCHOOL_DEFAULT:
			gradeDropdown.AddOptions(defaultList);
			gradeDropdown.interactable = false;
			break;
		case GRUNDSCHULE:
			gradeDropdown.AddOptions(grundschule);
			break;
		case HAUPTSCHULE:
			gradeDropdown.AddOptions(hauptschule);
			break;
		case REALSCHULE:
			gradeDropdown.AddOptions(realschule);
			break;
		case GYMNASIUM:
			gradeDropdown.AddOptions(gymnasium);
			break;
		case FOERDERSCHULE:
			gradeDropdown.AddOptions(foerderschule);
			break;
		case GESAMTSCHULE:
			gradeDropdown.AddOptions(gesamtschule);
			break;
		default:
			break;
		}
	}


	// ********************************************************************************
	// Methods important for registration
	// ********************************************************************************


	/// <summary>
	/// Check if Data in input fields of the registration form is valid 
	/// and start the registration if so.
	/// </summary>
	public void RegisterIfDataValid () {
		if (!HasEmail ()) {
			notificationScript.ShowMessage ("Du hast keine E-Mail angegeben. Gib doch bitte eine E-Mail an!");
			return;
		}
		if (!HasGender ()) {
			notificationScript.ShowMessage ("Du hast kein Geschlecht ausgewählt, bitte wähle ein Geschlecht aus!");
			return;
		}
		if (!HasAge()) {
			notificationScript.ShowMessage ("Du hast kein Alter ausgewählt, bitte sag uns wie Alt du bist!");
			return;
		}
		if (!HasSchoolType()) {
			notificationScript.ShowMessage ("Du hast keine Schule ausgewählt, bitte gebe eine Schule an!");
			return;
		}
		if (!HasGrade()) {
			notificationScript.ShowMessage ("Du hast keine Klasse ausgewählt, sag uns doch in welche Klasse du gehst!");
			return;
		}
		if (!HasState()) {
			notificationScript.ShowMessage ("Sag uns bitte noch woher du kommst!");
			return;
		}
		if (!HasNativeLanguage()) {
			notificationScript.ShowMessage ("Sag uns doch bitte welche Sprache bei dir Zuhause meistens gesprochen wird!");
			return;
		}
		StartCoroutine (DatabaseInterface.IsUsernameTaken(GetUsername (), RegistrationNameHandler));
	}

	public void RegistrationNameHandler(PhpOutputHandler handler) {
		if (handler.Success ()) {
			if (handler.GetOutput ().ContainsKey ("USERNAME_TAKEN") && handler.GetOutput () ["USERNAME_TAKEN"].Equals ("0")) {
				// Username is free
				Debug.Log ("Username is free");
				StartCoroutine (DatabaseInterface.Register(GetUsername (), GetPassword (), GetEmail (), GetGender (), /*GetAge ()*/ "12", GetSchool (), GetGrade (), GetState (), GetLanguage (), RegistrationHandler));
			} else {
				// Username is taken
				notificationScript.ShowMessage ("Der Name ist leider vergeben. Such dir doch einen anderen Benutzernamen aus!");
			}
		}
	}

	/// <summary>
	/// Handle the php output of the registration script
	/// Proceed to login if successful,
	/// Show error message if unsuccessful
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void RegistrationHandler (PhpOutputHandler handler) {
		if (handler.Success())
		{
			// Registration was successful
			navigation.ShowLogin ();
		}
		else
		{
			// Registration failed
			notificationScript.ShowMessage ("Die Registrierung ist fehlgeschlagen. Bitte überprüfe deine Internetverbindung!");
		}
	}

	// ********************************************************************************
	// Methods used for login
	// ********************************************************************************

	/// <summary>
	/// Handles the php output of the login script.
	/// 
	/// </summary>
	/// <param name="handler">Handler.</param>
	public void LoginHandler (PhpOutputHandler handler) {
		if (!handler.Connection()) {
			Debug.Log("database is not running");
			notificationScript.ShowMessage ("Es gibt keine Verbindung zum Internet. Bitte stelle sicher dass dein Gerät mit dem Internet verbunden ist.");
		} else if (handler.Success()) {
			Debug.Log("Connection is stable");
			if (handler.GetOutput().ContainsKey("SUCCESS") && handler.GetOutput()["SUCCESS"] == "1") {
				Debug.Log("login succeeded");
				if (handler.GetOutput().ContainsKey("USER_ID"))
					Logger.Instance.SetUserID(Int32.Parse(handler.GetOutput()["USER_ID"]));
				Logger.Instance.StartSession();

				navigation.ShowGameSelection ();
			}
		} else {
			Debug.Log("login failed");
			notificationScript.ShowMessage ("Anmeldung ist fehlgeschlagen: Entweder ist dein Nutzername oder dein Passwort falsch!");
		}
	}

	public void Login() {
		StartCoroutine (DatabaseInterface.Login (GetUsernameLogin (), GetPasswordLogin (), LoginHandler));
	}

	// ********************************************************************************
	// Methods to check if the user selected a value in a dropdown
	// ********************************************************************************

	/// <summary>
	/// Checks if the dropdown value is equal to a given string
	/// </summary>
	/// <returns><c>true</c>, if dropdown value equal to was ised, <c>false</c> otherwise.</returns>
	/// <param name="dropdown">Dropdown.</param>
	/// <param name="value">Value.</param>
	public bool isDropdownValueEqualTo (Dropdown dropdown, string value) {
		return dropdown.options[dropdown.value].text.Equals(value);
	}

	/// <summary>
	/// Checks if the user selected the grade.
	/// </summary>
	/// <returns><c>true</c>, if grade selected was ised, <c>false</c> otherwise.</returns>
	public bool isGradeSelected()
	{
		return !(isDropdownValueEqualTo(gradeDropdown, GRADE_DEFAULT));
	}

	public bool HasEmail()
	{
		if (GetEmail().Equals(""))
			return false;
		return true;
	}

	/// <summary>
	/// Determines whether this instance has age.
	/// </summary>
	/// <returns><c>true</c> if this instance has age; otherwise, <c>false</c>.</returns>
	public bool HasAge()
	{
		return !(isDropdownValueEqualTo (dayDropdown, DAY_DEFAULT) || isDropdownValueEqualTo (monthDropdown, MONTH_DEFAULT) || isDropdownValueEqualTo (yearDropdown, YEAR_DEFAULT));
	}

	/// <summary>
	/// Determines whether this instance has sex.
	/// </summary>
	/// <returns><c>true</c> if this instance has sex; otherwise, <c>false</c>.</returns>
	public bool HasGender()
	{
		Debug.Log (GENDER_DEFAULT);
		Debug.Log(GetSelectedItemFromDropdown(genderDropdown));
		return !(isDropdownValueEqualTo (genderDropdown, GENDER_DEFAULT));
	}

	/// <summary>
	/// Determines whether this instance has school type.
	/// </summary>
	/// <returns><c>true</c> if this instance has school type; otherwise, <c>false</c>.</returns>
	public bool HasSchoolType()
	{
		return !(isDropdownValueEqualTo (schoolDropdown, SCHOOL_DEFAULT));
	}

	/// <summary>
	/// Determines whether user selected a grade.
	/// </summary>
	/// <returns><c>true</c> if user selected a grade; otherwise, <c>false</c>.</returns>
	public bool HasGrade()
	{
		return !(isDropdownValueEqualTo (gradeDropdown, GRADE_DEFAULT));
	}

	/// <summary>
	/// Determines whether the user selected a state.
	/// </summary>
	/// <returns><c>true</c> if user selected a state; otherwise, <c>false</c>.</returns>
	public bool HasState()
	{
		return !(isDropdownValueEqualTo (stateDropdown, STATE_DEFAULT));
	}

	/// <summary>
	/// Determines whether the user selected a language.
	/// </summary>
	/// <returns><c>true</c> if user selected a language; otherwise, <c>false</c>.</returns>
	public bool HasNativeLanguage()
	{
		return !(isDropdownValueEqualTo (languageDropdown, LANGUAGE_DEFAULT));
	}

	/// <summary>
	/// Gets the selected item from dropdown.
	/// </summary>
	/// <returns>The selected item from dropdown.</returns>
	/// <param name="dropdown">Dropdown.</param>
	public static string GetSelectedItemFromDropdown(Dropdown dropdown)
	{
		return dropdown.options[dropdown.value].text;
	}
		
	private string intToString(int number, int padding, char c) {
		string number_string = number.ToString ();
		for (int i = number_string.Length; i < padding; i++) {
			number_string = c + number_string;
		}

		return number_string;
	}
		




	// ********************************************************************************
	// These methods are called within the unity editor of the respective fields
	// if there is a change
	// ********************************************************************************

	/// <summary>
	/// Checks the email.
	/// </summary>
	public void ValidateEmail() {
		bool valid = IsInputValid (emailField, EmailRegex, 0, 100);
		Debug.Log ("Validity: " + valid.ToString ());
		Debug.Log (Regex.IsMatch (emailField.text,EmailRegex));
		UpdateValidity(emailField, valid);
	}

	/// <summary>
	/// Checks the username.
	/// </summary>
	/// <param name="input">Input.</param>
    public void ValidateUsername()
    {
		UpdateValidity(usernameField, IsUsernameValid ());
    }

	/// <summary>
	/// Checks if the password meets the requirements of length and containing characters
	/// </summary>
	/// <param name="input">Input.</param>
    public void ValidatePassword()
    {
		UpdateValidity(passwordField, IsPasswordValid ());
    }

	/// <summary>
	/// Checks if the repeated password matches the password and updates the validity of the field accordingly
	/// </summary>
	/// <param name="passwordRepeat">Password repeat.</param>
	public void ValidatePasswordRepeat()
    {
		UpdateValidity(passwordRepeatField, IsContentEqual(passwordRepeatField, passwordField));
    }

	/// <summary>
	/// Determines whether this instance is content equal the specified input1 input2.
	/// </summary>
	/// <returns><c>true</c> if this instance is content equal the specified input1 input2; otherwise, <c>false</c>.</returns>
	/// <param name="input1">Input1.</param>
	/// <param name="input2">Input2.</param>
    public bool IsContentEqual(InputField input1, InputField input2)
    {
        string text1 = input1.text;
        string text2 = input2.text;
        if (text1 != null && text2 != null && text1.Equals(text2))
            return true;
        return false;
    }
		
	/// <summary>
	/// Validates the input.
	/// </summary>
	/// <returns><c>true</c>, if input was validated, <c>false</c> otherwise.</returns>
	/// <param name="input">Input.</param>
	/// <param name="regex">Regex.</param>
	/// <param name="minLength">Minimum length.</param>
	/// <param name="maxLength">Max length.</param>
    public bool IsInputValid(InputField input, string regex, int minLength, int maxLength)
    {
		if (input == null)
			return false;

        string inputText = input.text;
		if (Regex.IsMatch(inputText, regex) && inputText.Length >= minLength && inputText.Length <= maxLength)
        	return true;

		return false;
    }

	/// <summary>
	/// Updates the validity.
	/// </summary>
	/// <param name="input">Input.</param>
	/// <param name="isValid">If set to <c>true</c> is valid.</param>
    public void UpdateValidity(InputField input, bool isValid)
    {
        Image background = input.GetComponent("Image") as Image;
        if (background != null)
        {
            if (isValid) background.color = Valid;
            else background.color = Invalid;
        }
    }
		
	public void LoadRegister2() {
		Debug.Log ("Test");
		if (!IsUsernameValid ()) {
			notificationScript.ShowMessage ("Der Benutzername darf nur 6-12 Zeichen lang sein und muss aus Buchstaben und Zahlen bestehen.");
			return;
		}
		if (!IsEmailValid ()) {
			notificationScript.ShowMessage ("Die E-Mail Adresse ist ungültig!");
			return;
		}
		if (!IsPasswordValid ()) {
			notificationScript.ShowMessage ("Das Password muss mindestens " + PasswordMinLength.ToString () + " Zeichen, und maximal " + PasswordMaxLength.ToString () + " Zeichen lang sein!");
			return;
		}
		if (!IsContentEqual (passwordField, passwordRepeatField)) {
			notificationScript.ShowMessage ("Ups, die Passwörter sind nicht identisch! Du musst 2 mal das selbe Passwort eingeben!");
			return;
		}

		StartCoroutine (DatabaseInterface.IsUsernameTaken (GetUsername (), NameTakenHandler));
	}

	private void NameTakenHandler(PhpOutputHandler handler) {
		if (handler.Success ()) {
			if (handler.GetOutput ().ContainsKey ("USERNAME_TAKEN") && handler.GetOutput () ["USERNAME_TAKEN"].Equals ("0")) {
				// Username is free
				Debug.Log ("Username is free");
				navigation.ShowRegister2 ();
			} else {
				// Username is taken
				notificationScript.ShowMessage ("Der Name ist leider vergeben. Such dir doch einen anderen Benutzernamen aus!");
			}
		}
	}






	// ********************************************************************************
	// Validate Fields
	// ********************************************************************************

	bool IsUsernameValid() {
		return IsInputValid (usernameField, UsernameRegex, UsernameMinLength, UsernameMaxLength);
	}

	bool IsEmailValid() {
		return IsInputValid (emailField, EmailRegex, 0, 1000);
	}

	bool IsPasswordValid() {
		return IsInputValid (passwordField, PasswordRegex, PasswordMinLength, PasswordMaxLength);
	}




	// ********************************************************************************
	// Getter
	// ********************************************************************************

	public string GetUsernameLogin() {
		return usernameLoginField.text;
	}

	public string GetPasswordLogin () {
		return passwordLoginField.text;
	}

	public string GetUsername () {
		return usernameField.text;
	}

	public string GetEmail () {
		return emailField.text;
	}

	public string GetPassword () {
		return passwordField.text;
	}

	public string GetPasswordRepeat () {
		return passwordRepeatField.text;
	}

	/// <summary>
	/// Extracts the Age from the age Dropdown
	/// </summary>
	/// <returns>The age.</returns>
	public string GetAge()
	{
		string day = GetSelectedItemFromDropdown (dayDropdown);
		string month = GetSelectedItemFromDropdown (monthDropdown);
		string year = GetSelectedItemFromDropdown (yearDropdown);

		string date = intToString(Int32.Parse(year), 2, '0') + "-" + intToString(Int32.Parse(month), 2, '0') + "-" + intToString(Int32.Parse(day), 2, '0');
		Debug.Log (date);
		return date;
	}

	public string GetGender () {
		return GetSelectedItemFromDropdown (genderDropdown);
	}

	public string GetSchool () {
		return GetSelectedItemFromDropdown (schoolDropdown);
	}

	public string GetGrade () {
		return GetSelectedItemFromDropdown (gradeDropdown);
	}

	public string GetState () {
		return GetSelectedItemFromDropdown (stateDropdown);
	}

	public string GetLanguage () {
		return GetSelectedItemFromDropdown (languageDropdown);
	}

}
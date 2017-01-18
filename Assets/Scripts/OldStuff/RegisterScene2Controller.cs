using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterScene2Controller : MonoBehaviour {
	public Dropdown schoolDropdown;
	public Dropdown gradeDropdown;
	public Dropdown ageDropdown;
	public Dropdown genderDropdown;
	public Dropdown stateDropdown;
	public Dropdown languageDropdown;


	private const string SCHOOL_DEFAULT = "Auf welche Schule gehst du?";
	private const string AGE_DEFAULT = "Wie alt bist du?";
	private const string GRADE_DEFAULT = "In welche Klasse gehst du?";
	private const string GENDER_DEFAULT = "Bist du ein Junge oder ein Mädchen?";
	private const string STATE_DEFAULT = "In welchem Bundesland wohnst du?";
	private const string LANGUAGE_DEFAULT = "Wähle eine Sprache aus!";

	private const string GRUNDSCHULE = "Grundschule";
	private const string HAUPTSCHULE = "Hauptschule";
	private const string REALSCHULE = "Realschule";
	private const string GYMNASIUM = "Gymnasium";
	private const string GESAMTSCHULE = "Gesamtschule";
	private const string FOERDERSCHULE = "Förderschule";

	private GameObject gameController1;

	// create grade lists for different types of schools 
	private List<Dropdown.OptionData>
		defaultList,
		grundschule,
		hauptschule,
		realschule,
		gymnasium,
		gesamtschule,
		foerderschule;

	// Load reference to register script to access
	// the register logic (database etc)
	private RegisterScript regScript;
	private RegisterHelpScript regHelpScript;

	// Use this for initialization
	void Start () {
		gameController1 = GameObject.Find ("GameController1");
		regScript = transform.GetComponent<RegisterScript> ();
		regHelpScript = transform.GetComponent<RegisterHelpScript> ();

		InitAgeDropdown ();
		InitSchoolList ();
		InitGenderDropdown ();
		InitGradeDropdown ();
		InitStateDropdown ();
		InitLanguageDropdown ();
		SetGradeDropdownItems ();

		SetSecondRegisterSceneFieldValues ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSecondRegisterSceneFieldValues() {
		Debug.Log ("SetFirstRegisterSceneFieldValues Function Call");
		if (PlayerPrefs.HasKey ("AgeIndex"))
			ageDropdown.value = PlayerPrefs.GetInt ("AgeIndex");
		if (PlayerPrefs.HasKey ("GenderIndex"))
			genderDropdown.value = PlayerPrefs.GetInt ("GenderIndex");
		if (PlayerPrefs.HasKey ("SchoolIndex"))
			schoolDropdown.value = PlayerPrefs.GetInt ("SchoolIndex");
		if (PlayerPrefs.HasKey ("GradeIndex"))
			gradeDropdown.value = PlayerPrefs.GetInt ("GradeIndex");
		if (PlayerPrefs.HasKey ("StateIndex"))
			stateDropdown.value = PlayerPrefs.GetInt ("StateIndex");
		if (PlayerPrefs.HasKey ("LanguageIndex"))
			languageDropdown.value = PlayerPrefs.GetInt ("LanguageIndex");
	}

	/// <summary>
	/// Register this instance.
	/// </summary>
	public void Register()
	{
		regScript.Register();
	}

	/// <summary>
	/// Loads the first register scene. Also sets the PlayerPrefs for the
	/// Dropdown items (selected Item index)
	/// </summary>
	public void LoadFirstRegisterScene() {
		PlayerPrefs.SetInt ("AgeIndex", ageDropdown.value);
		PlayerPrefs.SetInt ("GenderIndex", genderDropdown.value);
		PlayerPrefs.SetInt ("SchoolIndex", schoolDropdown.value);
		PlayerPrefs.SetInt ("StateIndex", stateDropdown.value);
		PlayerPrefs.SetInt ("GradeIndex", gradeDropdown.value);
		PlayerPrefs.SetInt ("LanguageIndex", languageDropdown.value);

		Destroy (gameController1);
		SceneManager.LoadScene ("Registration_Android");
	}

	/// <summary>
	/// Deletes the register player prefs.
	/// </summary>
	public void DeleteRegisterPrefs() {
		PlayerPrefs.DeleteKey ("Username");
		PlayerPrefs.DeleteKey ("Email");
		PlayerPrefs.DeleteKey ("Password");
		PlayerPrefs.DeleteKey ("PasswordRepeat");
		PlayerPrefs.DeleteKey ("AgeIndex");
		PlayerPrefs.DeleteKey ("SchoolIndex");
		PlayerPrefs.DeleteKey ("GenderIndex");
		PlayerPrefs.DeleteKey ("StateIndex");
		PlayerPrefs.DeleteKey ("GradeIndex");
		PlayerPrefs.DeleteKey ("LanguageIndex");
	}


	// ********************************************************************************
	// Initialize Dropdowns
	// ********************************************************************************

	/// <summary>
	/// Generates the option data list.
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
	/// Initiates the school list.
	/// </summary>
	private void InitSchoolList() {
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
	/// Initiates the age dropdown.
	/// </summary>
	private void InitAgeDropdown() {
		ageDropdown.ClearOptions ();
		ageDropdown.AddOptions (GenerateOptionDataList (
			AGE_DEFAULT,
			"6",
			"7",
			"8",
			"9",
			"10",
			"11",
			"12",
			"13",
			"14",
			"15",
			"16",
			"17",
			"18",
			"19",
			"20",
			"> 20"));
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
	/// Initiates the grade dropdown.
	/// </summary>
	private void InitGradeDropdown()
	{
		defaultList = generateGradeList(0, 0);
		grundschule = generateGradeList(1, 4);
		hauptschule = generateGradeList(5, 9);
		realschule = generateGradeList(5, 10);
		gymnasium = generateGradeList(5, 13);
		gesamtschule = generateGradeList(5, 13);
		foerderschule = generateGradeList(5, 12);
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
	private List<Dropdown.OptionData> generateGradeList(int from, int to)
	{
		List<Dropdown.OptionData> gradeList = new List<Dropdown.OptionData>();

		gradeList.Add(new Dropdown.OptionData(GRADE_DEFAULT));
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
	// Methods to check if the user set a value in a dropdown
	// ********************************************************************************


	/// <summary>
	/// Checks if the user selected the grade.
	/// </summary>
	/// <returns><c>true</c>, if grade selected was ised, <c>false</c> otherwise.</returns>
	public bool isGradeSelected()
	{
		return !(gradeDropdown.options[gradeDropdown.value].text.Equals(GRADE_DEFAULT));
	}

	/// <summary>
	/// Determines whether this instance has age.
	/// </summary>
	/// <returns><c>true</c> if this instance has age; otherwise, <c>false</c>.</returns>
	public bool HasAge()
	{
		return !(ageDropdown.options [ageDropdown.value].text.Equals (AGE_DEFAULT));
	}

	/// <summary>
	/// Determines whether this instance has sex.
	/// </summary>
	/// <returns><c>true</c> if this instance has sex; otherwise, <c>false</c>.</returns>
	public bool HasGender()
	{
		return !(genderDropdown.options [genderDropdown.value].text.Equals (GENDER_DEFAULT));
	}

	/// <summary>
	/// Determines whether this instance has school type.
	/// </summary>
	/// <returns><c>true</c> if this instance has school type; otherwise, <c>false</c>.</returns>
	public bool HasSchoolType()
	{
		return !(schoolDropdown.options [schoolDropdown.value].text.Equals (SCHOOL_DEFAULT));
	}

	/// <summary>
	/// Determines whether user selected a grade.
	/// </summary>
	/// <returns><c>true</c> if user selected a grade; otherwise, <c>false</c>.</returns>
	public bool HasGrade()
	{
		return !(gradeDropdown.options [gradeDropdown.value].text.Equals (GRADE_DEFAULT));
	}

	/// <summary>
	/// Determines whether the user selected a state.
	/// </summary>
	/// <returns><c>true</c> if user selected a state; otherwise, <c>false</c>.</returns>
	public bool HasState()
	{
		return !(stateDropdown.options [stateDropdown.value].text.Equals (STATE_DEFAULT));
	}

	/// <summary>
	/// Determines whether the user selected a language.
	/// </summary>
	/// <returns><c>true</c> if user selected a language; otherwise, <c>false</c>.</returns>
	public bool HasNativeLanguage()
	{
		return !(languageDropdown.options [languageDropdown.value].text.Equals (LANGUAGE_DEFAULT));
	}

	/// <summary>
	/// Gets the selected item from dropdown.
	/// </summary>
	/// <returns>The selected item from dropdown.</returns>
	/// <param name="dropdown">Dropdown.</param>
	public static string getSelectedItemFromDropdown(Dropdown dropdown)
	{
		return dropdown.options[dropdown.value].text;
	}

	/// <summary>
	/// Extracts the Age from the age Dropdown
	/// </summary>
	/// <returns>The age.</returns>
	public string GetAge()
	{
		string value = ageDropdown.options [ageDropdown.value].text;
		if (value.Contains(">"))
		{
			return System.Int32.MaxValue.ToString();
		}
		return value;
	}

	public string GetGender () {
		return genderDropdown.options [genderDropdown.value].text;
	}

	public string GetSchool () {
		return schoolDropdown.options [schoolDropdown.value].text;
	}

	public string GetGrade () {
		return gradeDropdown.options [gradeDropdown.value].text;
	}

	public string GetState () {
		return stateDropdown.options [stateDropdown.value].text;
	}

	public string GetLanguage () {
		return languageDropdown.options [languageDropdown.value].text;
	}

	public RegisterHelpScript GetRegisterHelpScript() {
		return regHelpScript;
	}
}

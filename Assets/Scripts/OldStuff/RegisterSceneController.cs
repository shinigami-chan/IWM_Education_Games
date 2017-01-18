using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class RegisterSceneController : MonoBehaviour {
    // Public variables

    public InputField usernameField;
    public InputField emailField;
    public InputField passwordField;
    public InputField passwordRepeatField;


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
    private RegisterScript regScript;
	private RegisterHelpScript regHelpScript;

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
		Debug.Log ("RegisterSceneController Start call");
		DontDestroyOnLoad (GameObject.Find("GameController1"));
		regHelpScript = transform.GetComponent<RegisterHelpScript> ();
		SetFirstRegisterSceneFieldValues ();
    }
		

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
	/// Loads the second register scene.
	/// </summary>
	public void LoadSecondRegisterSceneWorker () {
		PlayerPrefs.SetString ("Username", usernameField.GetComponent<InputField> ().text);
		PlayerPrefs.SetString ("Email", emailField.GetComponent<InputField> ().text);
		PlayerPrefs.SetString ("Password", passwordField.GetComponent<InputField> ().text);
		PlayerPrefs.SetString ("PasswordRepeat", passwordRepeatField.GetComponent<InputField> ().text);
	
		SceneManager.LoadScene ("Registration_Next_Android");
		Debug.Log ("Load Register Scene");
	}


	/// <summary>
	/// Sets the first register scene field values.
	/// </summary>
	public void SetFirstRegisterSceneFieldValues() {
		Debug.Log ("SetFirstRegisterSceneFieldValues Function Call");
		if (PlayerPrefs.HasKey ("Username"))
			usernameField.GetComponent<InputField> ().text = PlayerPrefs.GetString ("Username");
		if (PlayerPrefs.HasKey ("Email"))
			emailField.GetComponent<InputField> ().text = PlayerPrefs.GetString ("Email");
		if (PlayerPrefs.HasKey ("Password"))
			passwordField.GetComponent<InputField> ().text = PlayerPrefs.GetString ("Password");
		if (PlayerPrefs.HasKey ("PasswordRepeat"))
			passwordRepeatField.GetComponent<InputField> ().text = PlayerPrefs.GetString ("PasswordRepeat");
	}

	/// <summary>
	/// Deletes the register player prefs.
	/// </summary>
	public static void DeleteRegisterPrefs() {
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

	public void CheckEmail() {
		bool valid = ValidateInput (emailField, EmailRegex, 0, 100);
		Debug.Log ("Validity: " + valid.ToString ());
		Debug.Log (Regex.IsMatch (emailField.text,EmailRegex));
		UpdateValidity(emailField, valid);
	}

	/// <summary>
	/// Checks the username.
	/// </summary>
	/// <param name="input">Input.</param>
    public void CheckUsername()
    {
		bool valid = ValidateInput (usernameField, UsernameRegex, UsernameMinLength, UsernameMaxLength);
		UpdateValidity(usernameField, valid);
    }

	/// <summary>
	/// Checks if the password meets the requirements of length and containing characters
	/// </summary>
	/// <param name="input">Input.</param>
    public void CheckPassword()
    {
		bool valid = ValidateInput (passwordField, PasswordRegex, PasswordMinLength, PasswordMaxLength);
		UpdateValidity(passwordField, valid);
    }

	/// <summary>
	/// Checks if the repeated password matches the password and updates the validity of the field accordingly
	/// </summary>
	/// <param name="passwordRepeat">Password repeat.</param>
	public void CheckPasswordRepeat()
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
    public bool ValidateInput(InputField input, string regex, int minLength, int maxLength)
    {
        if (input != null)
        {
            string inputText = input.text;
			if (Regex.IsMatch(inputText, regex) && inputText.Length >= minLength && inputText.Length <= maxLength)
                return true;
            return false;
        }
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


	public bool HasEmail()
	{
		if (GetEmail().Equals(""))
			return false;
		return true;
	}
    
    
	public void LoadLoginScene() {
		DeleteRegisterPrefs ();
		Destroy (GameObject.Find ("GameController1"));
		SceneManager.LoadScene ("Login");
	}

	// Update is called once per frame
	void Update () {
        //nothing happens here
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

	bool ArePasswordsIdentical() {
		if (GetPassword ().Equals (GetPasswordRepeat ()))
			return true;
		else {
			regHelpScript.ShowHelp ("Ups, die Passwörter sind nicht identisch! Du musst 2 mal das selbe Passwort eingeben!");
			UpdateValidity (passwordRepeatField, false);
			return false;
		}
	}

	bool ValidateUsername() {
		return ValidateInput (usernameField, UsernameRegex, UsernameMinLength, UsernameMaxLength);
	}

	bool ValidateEmail() {
		return ValidateInput (emailField, EmailRegex, 0, 1000);
	}

	bool ValidatePassword() {
		return ValidateInput (passwordField, PasswordRegex, PasswordMinLength, PasswordMaxLength);
	}

	public void Next() {
		if (!ValidateUsername ()) {
			regHelpScript.ShowHelp ("Der Benutzername darf nur 6-12 Zeichen lang sein und muss aus Buchstaben und Zahlen bestehen.");
			return;
		}
		if (!ValidateEmail ()) {
			regHelpScript.ShowHelp ("Die E-Mail Adresse ist ungültig!");
			return;
		}
		if (!ValidatePassword ()) {
			regHelpScript.ShowHelp ("Das Password muss mindestens " + PasswordMinLength.ToString () + " Zeichen, und maximal " + PasswordMaxLength.ToString () + " Zeichen lang sein!");
			return;
		}
		if (!GetPassword ().Equals (GetPasswordRepeat ())) {
			regHelpScript.ShowHelp ("Ups, die Passwörter sind nicht identisch! Du musst 2 mal das selbe Passwort eingeben!");
			return;
		}
		StartCoroutine (CheckForUsernameAndPassword ());
	}

	public void LoadSecondRegisterScene () {
		if (!HasEmail ()) {
			regHelpScript.ShowHelp ("Bitte gebe noch eine E-Mail Adresse an!");
			return;
		}
		StartCoroutine (CheckForUsernameAndPassword ());
	}

	IEnumerator CheckForUsernameAndPassword() {
		string url = RegisterScript.SERVER + "is_name_taken.php?username=" + GetUsername ();

		WWW itemsData = new WWW(url);

		yield return itemsData;

		PhpOutputHandler handler = new PhpOutputHandler(itemsData, true);

		if (handler.Success ()/* && controller1.IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField"))*/) {
			if (handler.GetOutput ().ContainsKey ("USERNAME_TAKEN") && handler.GetOutput () ["USERNAME_TAKEN"].Equals ("0")) {
				// Username is free
				Debug.Log ("Username is free");
				if (ArePasswordsIdentical ())
					LoadSecondRegisterSceneWorker ();
			} else {
				// Username is taken
				regHelpScript.ShowHelp ("Der Benutzername ist leider schon vergeben, such dir doch einen neuen aus!");
				UpdateValidity (usernameField, false);
			}
		}
	}
}
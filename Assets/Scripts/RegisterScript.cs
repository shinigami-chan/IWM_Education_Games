using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RegisterScript : MonoBehaviour {
    private bool usernameValid;
	private RegisterSceneController controller1;
    private RegisterScene2Controller controller2;
    public static readonly string SERVER = "http://lernplattform.iwm-kmrc.de/php_scripts/";

	private string username;
	private string email;
	private string password;
	private string passwordRepeat;

	private string age;
	private string gender;
	private string school;
	private string grade;
	private string state;
	private string language;

    // Use this for initialization
    void Start () {
		controller1 = GameObject.Find ("GameController1").GetComponent<RegisterSceneController> ();
        controller2 = GameObject.Find("GameController2").GetComponent<RegisterScene2Controller> ();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitRegisterValues() {
		username = controller1.GetUsername ();
		email = controller1.GetEmail ();
		password = controller1.GetPassword ();
		passwordRepeat = controller1.GetPasswordRepeat ();

		age = controller2.GetAge ();
		gender = controller2.GetGender ();
		school = controller2.GetSchool ();
		grade = controller2.GetGrade ();
		state = controller2.GetState ();
		language = controller2.GetLanguage ();
	}

    public IEnumerator RegisterWorker()
    {

		InitRegisterValues ();
        // Prepare url with ref to the register script and the given parameters
        string url = SERVER + "register_user.php?";

		// Extract hashed passwort from password field
		string hashedPw = Utilities.GetSHA256(password);

		url += "username=" + Utilities.PercentEncode(username);
		url += "&password=" + Utilities.PercentEncode (hashedPw);

		if (controller1.HasEmail ())
			url += "&email=" + Utilities.PercentEncode (email);
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Du hast keine E-Mail angegeben, bitte klicke zurück gebe eine E-Mail Adresse an!");
			yield break;
		}
		if (controller2.HasGender ()) {
			url += "&sex=" + ExtractGenderForDatabase (gender);
		} else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Du hast kein Geschlecht ausgewählt, bitte wähle ein Geschlecht aus!");
			yield break;
		}
        if (controller2.HasAge())
            url += "&age=" + age;
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Du hast kein Alter ausgewählt, bitte sag uns wie Alt du bist!");
			yield break;
		}
        if (controller2.HasSchoolType())
			url += "&school=" + Utilities.PercentEncode(school);
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Du hast keine Schule ausgewählt, bitte gebe eine Schule an!");
			yield break;
		}
		if (controller2.HasGrade())
			url += "&grade=" + Utilities.PercentEncode(grade);
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Du hast keine Klasse ausgewählt, sag uns doch in welche Klasse du gehst!");
			yield break;
		}
        if (controller2.HasState())
			url += "&state=" + Utilities.PercentEncode(state);
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Sag uns bitte noch woher du kommst!");
			yield break;
		}
        if (controller2.HasNativeLanguage())
			url += "&native_language=" + Utilities.PercentEncode(language);
		else {
			controller2.GetRegisterHelpScript ().ShowHelp ("Sag uns doch bitte in welcher Sprache bei dir Zuhause meistens gesprochen wird!");
			yield break;
		}

        Debug.Log(url);

        WWW db = new WWW(url);

        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db, true);

        if (handler.Success())
        {
            // Registration was successful
			controller2.DeleteRegisterPrefs ();
            SceneManager.LoadScene("Login");
        }
        else
        {
            // Registration was unsuccessful
            // Do nothing (eventually show notification)
        }
    }
    
    public IEnumerator RegisterCoroutine()
    {
        //string username = GameObject.Find("UsernameField").GetComponent<InputField>().text;
        string url = SERVER + "is_name_taken.php?username=" + username;

        //Debug.Log(url);

        WWW itemsData = new WWW(url);

        yield return itemsData;

        PhpOutputHandler handler = new PhpOutputHandler(itemsData, true);


        if (handler.Success()/* && controller1.IsContentEqual(GameObject.Find("PasswordRepeatField"), GameObject.Find("PasswordField"))*/)
        {
            if (handler.GetOutput().ContainsKey("USERNAME_TAKEN") && handler.GetOutput()["USERNAME_TAKEN"].Equals("0"))
            {
                // Username is free
                Debug.Log("Username is free");
                StartCoroutine(RegisterWorker());
            }
            else
            {
                // Username is taken
                Debug.Log("Username is taken");
				controller2.GetRegisterHelpScript ().ShowHelp ("Der Benutzername wurde dir vor der Nase weggeschnappt! Gehe zurück und wähle einen neuen aus!");
                /*controller1.UpdateValidity(GameObject.Find("UsernameField"), false);*/
            }
        }
    }

    public void Register()
    {
        StartCoroutine(RegisterCoroutine());
    }

	private static string ExtractGenderForDatabase(string gender)
    {
		if (gender.Equals("Junge"))
        {
            return "M";
        }
		else if (gender.Equals("Mädchen"))
        {
            return "W";
        }
        else
        {
            return "";
        }
    }
}

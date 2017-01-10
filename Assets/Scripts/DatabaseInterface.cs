using UnityEngine;
using System.Collections;
using System;

public static class DatabaseInterface {
	private static readonly string SERVER = "http://lernplattform.iwm-kmrc.de/php_scripts/";
	private static readonly string IS_NAME_TAKEN = "is_name_taken.php?username=";
	private static readonly string REGISTER = "register_user.php?";
	private static readonly string LOGIN = "login_user.php?";

	public delegate void Action ();
	public delegate void PhpActionHandler (PhpOutputHandler handler);

	public static IEnumerator IsUsernameTaken(string username, PhpActionHandler handle) {
		string url = SERVER + IS_NAME_TAKEN + username;

		WWW itemsData = new WWW(url);

		yield return itemsData;

		PhpOutputHandler handler = new PhpOutputHandler(itemsData, true);

		handle (handler);
	}

	public static IEnumerator Register(string username, string password, string email, string gender, string birthday, string school, string grade, string state, string language, PhpActionHandler handle)
	{
		// Prepare url with ref to the register script and the given parameters
		string url = SERVER + REGISTER;

		// Extract hashed passwort from password field
		string hashedPw = Utilities.GetSHA256(password);

		url += "username=" + Utilities.PercentEncode(username);
		url += "&password=" + Utilities.PercentEncode (hashedPw);
		url += "&email=" + Utilities.PercentEncode(email);
		url += "&sex=" + ExtractGenderForDatabase(gender);
		url += "&age=" + birthday;
		url += "&school=" + Utilities.PercentEncode(school);
		url += "&grade=" + Utilities.PercentEncode(grade);
		url += "&state=" + Utilities.PercentEncode(state);
		url += "&native_language=" + Utilities.PercentEncode(language);

		Debug.Log(url);

		WWW db = new WWW(url);

		yield return db;

		PhpOutputHandler handler = new PhpOutputHandler(db, true);

		handle (handler);
	}

	public static IEnumerator Login(string username, string password, PhpActionHandler handle) {
		string hashedPassword = Utilities.GetSHA256 (password);
		string url = SERVER + LOGIN;
		url += "username=" + username;
		url += "&password=" + hashedPassword;

		Debug.Log (url);

		WWW db = new WWW (url);

		yield return db;

		PhpOutputHandler handler = new PhpOutputHandler (db, true);
	
		handle (handler);
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

using UnityEngine;
using System.Collections;
using System.IO;

public class LogToFile : MonoBehaviour {


	private StreamWriter sr;
	private static string filePath;
	
	// Use this for initialization
	void Start () {
		filePath = Application.persistentDataPath;
	}
	
	// Update is called once per frame
	public static void WriteToFile () {
	
		StreamWriter sr = new StreamWriter (filePath + @"\chillemille.csv", true);
		sr.WriteLine ("Punkt: " + GameController.result);
		sr.Close();
	}
}

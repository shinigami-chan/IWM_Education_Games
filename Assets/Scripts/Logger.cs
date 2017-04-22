using System;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Linq;

public sealed class Logger : MonoBehaviour
{
    //Singleton approach for ensuring only one instance of the Logger class
    private static volatile Logger instance;
    private static object syncRoot = new System.Object();

    private int session_id = -1;
    private int user_id = -1;
    //    private const string FILEPATH = "./";
	private static string FILEPATH;
    private const string FILENAME = "loggingQueue.txt";

    private Logger() { }

	void Start () {
		FILEPATH = Application.persistentDataPath;
		Debug.Log (FILEPATH);
	}

    public static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        GameObject.Find("Logger").AddComponent<Logger>();
                        instance = GameObject.Find("Logger").GetComponent<Logger>();
                    }
                }
            }
            return instance;
        }
		set 
		{
			instance = value;
		}
    }

	public bool IsSessionRunning() {
		return session_id != -1 && user_id != -1;
	}

	void OnApplicationQuit() {
		Debug.Log("Application ending after " + Time.time + " seconds");
		RegisterSceneController.DeleteRegisterPrefs ();
		EndSession ();
	}

    private void AddURLToQuery(string url)
    {
        String file = @FILEPATH + "/" + FILENAME;
        using (StreamWriter w = File.AppendText(file))
        {
            w.WriteLine(url);
        }
    }
    
    /// <summary>
    /// Write content of file to same file without the first line
    /// </summary>
    /// <param name="file">file that shall "loose" its first line</param>
    private void WriteToFileWithoutFirstLine(String file)
    {
        var lines = File.ReadAllLines(file);
        File.WriteAllLines(file, lines.Skip(1).ToArray());
    }

    /// <summary>
    /// Recursively execute URLs in the Logfile until there is no connection
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator ExecuteQuery()
    {
        string line;
        string file = FILEPATH + "/" + FILENAME;
        if ((line = ReadFirstLine(file)) != null)
        {
            Debug.Log("Start Log: " + line);
            WWW db = new WWW(line);
            yield return db;
            Debug.Log("finished waiting for url");
			Debug.Log ("Yololo");

            PhpOutputHandler handler = new PhpOutputHandler(db, true);

			if (handler.Success ()) {
				Debug.Log ("1logger was successful");
				WriteToFileWithoutFirstLine (file);
				StartCoroutine(ExecuteQuery ());
			} else {
				if (handler.Connection ()) {
					WriteFailedIntoFile (line);
					WriteToFileWithoutFirstLine (file);
					Debug.Log ("Failure not because missing connection!");
				}
				Debug.Log ("Logger was unsuccessful");
			}
        }
    }

    private string ReadFirstLine(string file)
    {
        string firstLine = null;
        using (StreamReader reader = new StreamReader(file))
        {
            firstLine = reader.ReadLine();
        }
        return firstLine;
    }

    private void OfflineSaveLog(string latestUrl)
    {

        //string firstLine;

        //using (StreamReader reader = new StreamReader("MyFile.txt"))
        //{
        //    firstLine = reader.ReadLine() ?? "";
        //}
        //File.Delete(@FILEPATH + "/" + FILENAME);
        //File.Create(@FILEPATH + "/" + FILENAME);

        Debug.Log("PATH: \\" + @FILEPATH + "/" + FILENAME);
        //writeIntoFile(FILENAME,latestUrl);
        string[] urlQueue = File.ReadAllLines(@FILEPATH+"/"+FILENAME);
        //for(int i=0; i<urlQueue.Length; i++)
        //{
            //Debug.Log(urlQueue[i]);
            //StartCoroutine(TryUrl(urlQueue,i));
            //if (urlQueue[i] != null)
            //{
                //rewriteUrlQueue(urlQueue);
            //    break;
            //}
        //}

    }

    private void rewriteUrlQueue(string[] urlQueue)
    {
        File.Delete(@FILEPATH + "\\" + FILENAME);
        foreach (String url in urlQueue)
        {
            if (url != null)
            {
                WriteIntoFile(url);
            }
        }
    }

    

    private IEnumerator TryUrl(string[] urlQueue, int indexInQueue)
    {
        Debug.Log("before url");
        WWW db = new WWW(urlQueue[indexInQueue]);
        yield return db;
        Debug.Log("finished waiting for url");

        PhpOutputHandler handler = new PhpOutputHandler(db,true);

        if (handler.Success())
        {
            Debug.Log("logger was successful");
            urlQueue[indexInQueue] = null;
        }
    }

    public void StartSession()
    {
        if (user_id != -1)
            StartCoroutine(StartSessionWorker());
        else
            Debug.Log("user_id is not set");
    }

    private IEnumerator StartSessionWorker()
    {
        // Prepare url with ref to the session start script and the given parameters
        string url = RegisterScript.SERVER + "start_session.php?" + "user_id=" + user_id;
        Debug.Log(url);
        WWW db = new WWW(url);

        yield return db;

        Debug.Log("START SESSION HANDLER");
        PhpOutputHandler handler = new PhpOutputHandler(db,true);

        if (handler.Success() && handler.GetOutput().ContainsKey("SESSION_ID"))
        {
            session_id = Int32.Parse(handler.GetOutput()["SESSION_ID"]);
        }
        else
        {
            Debug.Log("Session Start could not be Logged although the connection to the database has been established.");
        }
    }

    public void EndSession()
    {
        Debug.Log("EndSession");

		string url = RegisterScript.SERVER + "end_session.php?system_log_id=" + session_id;

		WriteIntoFile(url);
		StartCoroutine (ExecuteQuery ());
		StartCoroutine (DestroyWithDelay ());
    }

	/// <summary>
	/// Destroy GameObject that holds the Logger after a delay, to ensure logging can complete
	/// before logging object is destroyed
	/// </summary>
	/// <returns>The with delay.</returns>
	IEnumerator DestroyWithDelay () {
		Debug.Log ("2Function Call Destroy With Delay");
		yield return new WaitForSeconds (1);
		Debug.Log ("2Destroy call");
		//Destroy (transform.gameObject);
		//instance = null;
		Destroy(this);
		Debug.Log ("2Destroy complete");
	}

    private string getTimestamp()
    {
        //needs placeholder "SPACE" to avoid misinterpreation
        return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffffff");
    }

    private string getEncodedTimestamp()
    {
        return Utilities.PercentEncode(getTimestamp());
    }

	/// <summary>
	/// Writes the into logging file. If File does not exist it is created
	/// </summary>
	/// <param name="url">URL.</param>
	private void WriteIntoFile(string url) {
		String file = @FILEPATH + "/" + FILENAME;
		using (StreamWriter w = File.AppendText(file))
		{
			w.WriteLine(url);
		}
	}

	/// <summary>
	/// Logs the GameAction
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="difficulty">Difficulty.</param>
    public void GameLog(Action action, int difficulty)
    {
        string url;
        switch (action)
        {
			case Action.START_BALLOON_GAME:
				url = RegisterScript.SERVER + "log_action.php?" + "session_id=" + session_id + "&game_id=" + Action.START_BALLOON_GAME.GetAttribute<Id> ().id + "&time_stamp=" + getEncodedTimestamp () + "&difficulty=" + difficulty;
				Debug.Log ("Action: Start Balloon Game");
				Debug.Log ("BALLON: " + url);
                break;
            case Action.START_NUMBER_LINE_GAME:
                url = RegisterScript.SERVER + "log_action.php?" + "session_id=" + session_id + "&game_id=" + Action.START_NUMBER_LINE_GAME.GetAttribute<Id>().id + "&time_stamp=" + getEncodedTimestamp()+"&difficulty=" + difficulty;
                Debug.Log("Action: Start Number Line Game");
                break;
            default: url = "";
                break;
        }
        WriteIntoFile(url);
		StartCoroutine(ExecuteQuery ());
        //OfflineSaveLog(url);
    }

    public void SetUserID(int user_id)
    {
        this.user_id = user_id;
    }

    public int GetUserID()
    {
        return this.user_id;
    }


	/// <summary>
	/// For debugging purposes write queries that failed not due to connection issues
	/// into a separate logging file
	/// </summary>
	/// <param name="url">URL.</param>
	void WriteFailedIntoFile(string url) {
		String file = @FILEPATH + "/" + "failed_loggingQueue.txt";
		using (StreamWriter w = File.AppendText(file))
		{
			w.WriteLine(url);
		}
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}

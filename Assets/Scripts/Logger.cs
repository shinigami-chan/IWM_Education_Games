using System;
using System.Collections;
using UnityEngine;
using System.IO;

public sealed class Logger : MonoBehaviour
{
    //Singelton approach for ensuring only one instance of the Logger class
    private static volatile Logger instance;
    private static object syncRoot = new System.Object();

    private int session_id = -1;
    private const string FILEPATH = "./";
    private const string FILENAME = "loggingQueue.txt";

    private Logger() { }

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
                    instance = new Logger();
                    GameObject.Find("Main Camera").AddComponent<Logger>();
                    instance = GameObject.Find("Main Camera").GetComponent<Logger>();
                }
                }
            }
            return instance;
        }
    }

    private void OfflineSaveLog(string latestUrl)
    {
        writeIntoFile(FILENAME,latestUrl);
        string[] urlQueue = File.ReadAllLines(@FILEPATH+"\\"+FILENAME);;
        for(int i=0; i<urlQueue.Length; i++)
        {
            StartCoroutine(TryUrl(urlQueue,i));
            if (urlQueue[i] == null)
            {
                rewriteUrlQueue(urlQueue);
                break;
            }
        }

    }

    private void rewriteUrlQueue(string[] urlQueue)
    {
        File.Delete(@FILEPATH + "\\" + FILENAME);
        foreach (String url in urlQueue)
        {
            if (url != null)
            {
                writeIntoFile(FILENAME,url);
            }
        }
    }

    private IEnumerator TryUrl(string[] urlQueue, int indexInQueue)
    {
        WWW db = new WWW(urlQueue[indexInQueue]);
        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db);

        if (handler.Success())
        {
            urlQueue[indexInQueue] = null;
        }
    }

    
    private IEnumerator StartSessionWorker(string user)
    {
        // Prepare url with ref to the session start script and the given parameters
        string url = "http://localhost/unity_games/start_session.php?" + "username=" + user;
        
        WWW db = new WWW(url);

        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db);

        if (handler.Success() && handler.GetOutput().ContainsKey("SESSION_ID"))
        {
            session_id = Int32.Parse(handler.GetOutput()["SESSION_ID"]);
        }
    }

    public IEnumerator EndSession(int system_action_log_id)
    {
        string url = "http://localhost/unity_games/end_session.php?" + "system_action_log_id=" + system_action_log_id;

        WWW db = new WWW(url);

        yield return db;

        if (db.text != "1")
        {

        }
        else
        {
            //print("success");
        }
    }

    private string getTimestamp()
    {
        return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
    }

    private void writeIntoFile(string fileName, string content)
    {
        if (!File.Exists(FILEPATH + fileName))
        {
            File.Create(FILEPATH + fileName).Close();
        }
        File.AppendAllText(FILEPATH + fileName, content+"\n");
    }

    public void Log(Action action)
    {
        string url;
        switch (action)
        {
            case Action.CHOOSE_GAME:
                url = "miau";               
                break;
            case Action.SHOW_STATISTICS:
                url = "http://localhost/unity_games/end_session.php?" + "system_action_log_id=" + 2;
                break;
            default: url = "";
                break;
        }
        OfflineSaveLog(url);
    }
}

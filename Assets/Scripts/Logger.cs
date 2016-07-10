using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Logger
{
    private int session_id = -1;

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
}

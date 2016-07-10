using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Logger
{
    private static IEnumerator StartSessionWorker(string user, Identifier id)
    {
        int system_action_log_id = 0;

        // Prepare url with ref to the session start script and the given parameters
        string url = "http://localhost/unity_games/start_session.php?" + "username=" + user;

        WWW db = new WWW(url);

        yield return db;

        PhpOutputHandler handler = new PhpOutputHandler(db);

        if (handler.Success() && handler.GetOutput().ContainsKey("ID"))
        {
            id.SetId(Int32.Parse(handler.GetOutput()["ID"]));
        }
    }

    public static IEnumerator EndSession(int system_action_log_id)
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

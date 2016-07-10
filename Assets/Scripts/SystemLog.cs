using System.Collections;
using System;
using UnityEngine;
using System.IO;
using System.Text;

public class SystemLog {

    const string FILEPATH = "./";

    public const int REGISTRATION = 1;
    public const int LOGIN = 2;
    public const int LOGOUT = 3;


    public static void LogAction(int action)
    {
        string fileName = "SystemLog.txt";
        switch (action)
        {
            //pattern: userId_actionId_timestamp
            case REGISTRATION: writeIntoFile(fileName,"1_"+getTimestamp()+";");
                break;
            case LOGIN: writeIntoFile(fileName, "2_" + getTimestamp() + ";");
                break;
            case LOGOUT: writeIntoFile(fileName, "3_" + getTimestamp() + ";");
                break;
            default: Debug.Log("Invalid log action in LogAction()");
                break;
        }
    }

    private static string getTimestamp()
    {
        return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
    }

    private static void writeIntoFile(string fileName, string log)
    {
        if (!File.Exists(FILEPATH + fileName))
        {
            File.Create(FILEPATH + fileName).Close();
        }
        File.AppendAllText(FILEPATH + fileName, log);
    }

}

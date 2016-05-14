using System.Collections;
using System;
using UnityEngine;
using System.IO;
using System.Text;

/// <summary>
/// Provides a list of reaction data (e.g. time stamps) and writes it into a csv file.
/// </summary>

public class ReactionData{

    //format: dd-MM-yyyy hh:mm:ss.fff

    private ArrayList reactionData;
    readonly string FILEPATH = //"D:\\Dokumente\\UNITY\\";
        "./";
    readonly string[] CSV_HEADER = new string[] { "id", "time_stamp", "action" };

    public ReactionData()
    {
        reactionData = new ArrayList();
    }

    private void addTimeStamp(TimeStamp timeStamp)
    {
        reactionData.Add(timeStamp);
    }

    public void addTimeStampClickedRight(DateTime time)
    {
        addTimeStamp(new TimeStamp(time, "clicked_right"));
    }

    public void addTimeStampClickedWrong(DateTime time)
    {
        addTimeStamp(new TimeStamp(time, "clicked_wrong"));
    }

    public void addTimeStampLoadedRound(DateTime time)
    {
        addTimeStamp(new TimeStamp(time, "loaded_round"));
    }

    public void printAllData()
    {
        foreach (TimeStamp timestamp in reactionData)
        {
            Debug.Log(timestamp.getActionTime().ToString("dd-MM-yyyy hh:mm:ss.fff") + " " + timestamp.getActionMode());
        }
    }

    public void creatingCsvFile(/*string filePath, */string fileName)
    {
        if (!File.Exists(/*filePath*/FILEPATH + fileName))
        {
            File.Create(/*filePath*/FILEPATH + fileName).Close();
        }
        ArrayList data = new ArrayList();
        data.Add(CSV_HEADER);
        foreach (TimeStamp timeStamp in reactionData)
        {
            data.Add(new string[] { "id", timeStamp.getActionTime().ToString("dd-MM-yyyy hh:mm:ss.fff"), timeStamp.getActionMode()});
        }
        string delimiter = ",";
        int length = data.Count;
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++) {
            sb.AppendLine(string.Join(delimiter, (string[])data[index]));
        }
        File.AppendAllText(/*filePath*/FILEPATH + fileName, sb.ToString());
    }
}

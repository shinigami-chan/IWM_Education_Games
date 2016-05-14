using System;

public class TimeStamp {

    private DateTime actionTime;
    private string actionMode;

    public TimeStamp(DateTime actionTime, string actionMode)
    {
        this.actionTime = actionTime;
        this.actionMode = actionMode;
    }

    public DateTime getActionTime()
    {
        return actionTime;
    }

    public string getActionMode()
    {
        return actionMode;
    }
}

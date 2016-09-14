using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PhpOutputHandler 
{
    private readonly Dictionary<string, string> output = new Dictionary<string, string>();

    public PhpOutputHandler(WWW result, bool verbose = false)
    {
        string[] array = result.text.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (string kv in array)
        {
            if (verbose)
                Debug.Log(kv);

            string[] split = kv.Split('=');
            if (split.Length == 2)
            {
                output.Add(split[0], split[1]);
            }
        }
    }

    public bool Success()
    {
        if (output.ContainsKey("SUCCESS") && output["SUCCESS"] == "1")
        {
            return true;
        }
        return false;
    }

    public bool Connection()
    {
        if (output.ContainsKey("CONNECTION") && output["CONNECTION"] == "1")
        {
            return true;
        }
        return false;
    }

    public Dictionary<string, string> GetOutput()
    {
        return output;
    }  
}

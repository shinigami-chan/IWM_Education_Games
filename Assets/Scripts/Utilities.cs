using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Utilities {

    public static string GetSHA256(string text)
    {
        byte[] byteRepresentation = Encoding.UTF8.GetBytes(text);

        SHA256Managed hashstring = new SHA256Managed();
        byte[] hash = hashstring.ComputeHash(byteRepresentation);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += string.Format("{0:x2}", x);
        }
        return hashString;
    }

    public static string ReplaceUmlautForPhp(string s)
    {
        return s.Replace("ü", "' || U&\'\\00FC\' || \'").Replace("ö", "' || U&26\'\\00F6\' || \'").Replace("ä", "' || U&26\'\\00F4\' || \'");
    }

    public static string PercentEncode(string s)
    {
        Debug.Log("string before percent encoding:\n " + s);
        Dictionary<string, string> percentEncodings = new Dictionary<string, string>();
        percentEncodings.Add(" ", "%20");
        percentEncodings.Add("!", "%21");
        percentEncodings.Add("\"", "%22");
        percentEncodings.Add("#", "%23");
        percentEncodings.Add("$", "%24");
        //percentEncodings.Add("%", "%25");
        percentEncodings.Add("&", "%26");
        percentEncodings.Add("'", "%27");
        percentEncodings.Add("(", "%28");
        percentEncodings.Add(")", "%29");
        percentEncodings.Add("*", "%2A");
        percentEncodings.Add("+", "%2B");
        percentEncodings.Add(",", "%2C");
        percentEncodings.Add("/", "%2F");
        percentEncodings.Add(":", "%3A");
        percentEncodings.Add(";", "%3B");
        percentEncodings.Add("=", "%3D");
        percentEncodings.Add("?", "%3F");
        percentEncodings.Add("@", "%40");
        percentEncodings.Add("[", "%5B");
        percentEncodings.Add("\\", "%5C");
        percentEncodings.Add("]", "%5D");
        percentEncodings.Add("{", "%7B");
        percentEncodings.Add("|", "%7C");
        percentEncodings.Add("}", "%7D");

        s = s.Replace("%", "%25");

        foreach (KeyValuePair<string, string> keyValue in percentEncodings)
            s = s.Replace(keyValue.Key, keyValue.Value);
        Debug.Log("string after percent encoding:\n " + s);
        return s;
    }

    public static string[] GetPhpOutput(WWW url)
    {
        return url.text.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
    }
    
    public static Dictionary<string, string> GetPhpOutputDict(WWW url)
    {
        string[] array = GetPhpOutput(url);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        
        foreach (string kv in array)
        {
            string[] split = kv.Split('=');
            if (split.Length == 2)
            {
                dict.Add(split[0], split[1]);
            }
        }
        return dict;
    }
}

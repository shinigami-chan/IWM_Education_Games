using System;
using System.Collections;
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

    public static string[] GetPhpOutput(WWW url)
    {
        return url.text.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
    }
}

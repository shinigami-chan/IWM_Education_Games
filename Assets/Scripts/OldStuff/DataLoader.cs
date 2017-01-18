using UnityEngine;
using System.Collections;

public class DataLoader : MonoBehaviour {
	// Use this for initialization
    void Start()
    {
        string username = "";
        //yield return GetPassword(username);
        
    }

    public IEnumerator GetPassword(string username)
    {
        Debug.Log("load Data");
        WWW itemsData = new WWW("http://localhost/unity_games/users_data.php?field=" + username);

        yield return itemsData;

        if (itemsData.text != "")
        {
            string[] data = itemsData.text.Split('|');
            print(data[1]);
        } else
        {
            print("Name Can be Used");
        }
        
        
        Debug.Log(itemsData.text);
    }
}

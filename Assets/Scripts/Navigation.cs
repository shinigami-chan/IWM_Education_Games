using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour {

    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login");
    }
}

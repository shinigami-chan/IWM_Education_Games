using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSelectionScriptAndroid : MonoBehaviour {

    Button player;
    Button stats;
    Button help;  

    // Use this for initialization
    void Start () {
        Debug.Log("User ID test: new Instance??");
        Debug.Log(Logger.Instance.GetUserID());

        player = GameObject.Find("Player").GetComponent<Button>();
        stats = GameObject.Find("Statistics").GetComponent<Button>();
        help = GameObject.Find("Help").GetComponent<Button>();
        
    }


    public void loadBalloonGame()
    {
        SceneManager.LoadScene("Balloon_Menu");
    }

    public void loadNumberLineGame()
    {
        SceneManager.LoadScene("Numberline_Menu");
    }
   
	public void Logout() {
		Logger.Instance.EndSession ();
		Debug.Log ("Destroy in logout()");
		SceneManager.LoadScene ("Login");
	}

	public IEnumerator DestroyWithDelay(GameObject gameObject) {
		Debug.Log ("WaitForDestroy");
		yield return new WaitForSeconds (0.1f);
		Debug.Log ("DestroyWithDelay");
		Destroy (gameObject);
	}

    // Update is called once per frame
    void Update () {
    }

}

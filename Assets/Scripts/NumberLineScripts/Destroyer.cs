using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		SceneManager.LoadScene(Application.loadedLevel);
	}
}

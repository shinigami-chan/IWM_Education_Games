using UnityEngine;
using UnityEngine.UI;

public class BoxLayoutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("HelpBackground"))
        {
            Image background = gameObject.GetComponent<Image>();
            Text text = gameObject.GetComponentInChildren<Text>();
            Debug.Log(text.rectTransform.rect.height);
            background.rectTransform.sizeDelta = new Vector2(text.rectTransform.rect.width, text.rectTransform.rect.height);
            Debug.Log(background.rectTransform.rect.height);
        }
	}
}

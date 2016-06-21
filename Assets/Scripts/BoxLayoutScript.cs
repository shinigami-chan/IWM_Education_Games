using UnityEngine;
using UnityEngine.UI;

public class BoxLayoutScript : MonoBehaviour
{
    public readonly static int Border = 10;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("HelpBackground"))
        {


            Debug.Log("Start BoxLayoutScript");
            RectTransform background = gameObject.GetComponent<RectTransform>();
            //Text text = gameObject.GetComponentInChildren<Text>();
            //Debug.Log("Text height: " + text.preferredHeight);
            //Debug.Log("Text xPos: " + text.rectTransform.anchoredPosition.x + " yPos: " + text.rectTransform.anchoredPosition.y);
            background.anchoredPosition = new Vector2(-1, 0);

            //background.rectTransform.sizeDelta = new Vector2(text.rectTransform.rect.width+Border*2, text.preferredHeight+Border*2);
            //background.rectTransform.anchoredPosition = new Vector2(text.rectTransform.anchoredPosition.x - Border, text.rectTransform.anchoredPosition.y - Border);
            Debug.Log("background height: " + background.rect.height);
        }
    }

    public void setHelpTextTag(string helptext)
    {

    }
}

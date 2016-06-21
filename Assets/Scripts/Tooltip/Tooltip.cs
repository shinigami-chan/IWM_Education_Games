using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    static int padding = 5;
    public GameObject tooltipPrefab;
    public GameObject tooltipParent;
    public string tooltipText;
    GameObject tooltip;

	// Use this for initialization
	void Start () {
        tooltip = (Instantiate(tooltipPrefab) as GameObject);
        tooltip.transform.SetParent(tooltipParent.transform);
        SetText();
        BasicLayout();
        //Show();
        Hide();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetText()
    {
        Text text = tooltip.GetComponentInChildren<Text>();
        Debug.Log(text.text);
        Debug.Log(tooltipText);
        text.text = tooltipText;
    }

    public void Hide()
    {
        tooltip.SetActive(false);
    }

    public void Show()
    {
        tooltip.SetActive(true);
    }

    void BasicLayout()
    {
        RectTransform tooltipTransform = tooltip.GetComponent<RectTransform>();
        Text text = tooltip.GetComponentInChildren<Text>();
        RectTransform textTransform = text.GetComponent<RectTransform>();
        Debug.Log("Text width: " + textTransform.rect.width + " height: " + text.preferredHeight);

        Debug.Log(tooltipTransform.GetSiblingIndex());
        tooltipTransform.SetAsLastSibling();
        Debug.Log(tooltipTransform.GetSiblingIndex());

        tooltipTransform.localScale = new Vector2(1f, 1f);
        //textTransform.sizeDelta = new Vector2(121, 8);
        tooltipTransform.sizeDelta = new Vector2 (textTransform.rect.width+4, text.preferredHeight+4);
        tooltipTransform.anchoredPosition = new Vector2(-2, 0);
        tooltipTransform.localPosition = new Vector3(tooltipTransform.localPosition.x, tooltipTransform.localPosition.y , 0);
        tooltipTransform.anchorMin = new Vector2(0f, 1f);
        tooltipTransform.anchorMax = new Vector2(0f, 1f);
        tooltipTransform.pivot = new Vector2(1f, 1f);
        
        textTransform.anchoredPosition = new Vector2(2, -2);
        textTransform.anchorMin = new Vector2(0f, 1f);
        textTransform.anchorMax = new Vector2(0f, 1f);
        textTransform.pivot = new Vector2(0f, 1f);
    }
}

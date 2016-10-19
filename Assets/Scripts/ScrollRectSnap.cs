using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScrollRectSnap : MonoBehaviour {

    // Public Variables
    public RectTransform panel; // Hold the ScrollPanel
    public Button[] buttons;
    public RectTransform center; // Compare distance to each button

    // Private variables
    public float[] distance;
    private bool dragging = false;
    private int buttonDist;
    private int minButtonNum;

    // Use this for initialization
    void Start() {
        int buttonLen = buttons.Length;
        distance = new float[buttonLen];

        buttonDist = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x - buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - buttons[i].transform.position.x);
        }
        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < buttons.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        if (!dragging)
        {
            LerpToButton(minButtonNum * -buttonDist);
            //StartCoroutine(WaitThenLerp(0.5f));
        }

    }

    IEnumerator WaitThenLerp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LerpToButton(minButtonNum * -buttonDist);
    }

    void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 12f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}

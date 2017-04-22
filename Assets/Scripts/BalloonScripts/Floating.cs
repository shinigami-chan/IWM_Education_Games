using UnityEngine;
using System.Collections;
using Random = System.Random;
using System;
using UnityEngine.SceneManagement;

public class Floating : MonoBehaviour {
	public Canvas referenceCv = null;

	public float rate = 1;
	public float amplitudeFactor = 1;

    public float step;
    public float offsetY; //How far to offset the object upwards
    public float offsetX;

	public float amplitude;

	public float canvasscaley;

    void Start()
    {
		referenceCv = referenceCv == null ? GameObject.FindObjectOfType<Canvas> ().GetComponent<Canvas> () : referenceCv;
        if(SceneManager.GetActiveScene().name != "Balloon_Game")
        {
			amplitude = amplitudeFactor * 10 * referenceCv.transform.localScale.y;
        }
        else
        {
			
            amplitude = referenceCv.transform.localScale.y / 100;
        }

        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //Create an offset based on our height
        offsetY = transform.position.y + transform.localScale.y;

        step = rnd.Next(0,400)/100 + rnd.Next(0,300)/100;
    }

    void Update()
    {
        floating();
    }

    void floating()
    {

        step += 0.035f * rate;
        //Make sure Steps value never gets too out of hand 
        if (step > 999999) { step = 1; }

        //Float up and down along the y axis, 
        Vector3 point = transform.position;
        point.y = offsetY + Mathf.Sin(step) * amplitude; //0.5f / Screen.height*1000;
        transform.position = point;
    }
}

using UnityEngine;
using System.Collections;
using Random = System.Random;
using System;
using UnityEngine.SceneManagement;

public class Floating : MonoBehaviour {

    private float step;
    private float offsetY; //How far to offset the object upwards
    private float offsetX;

    private float amplitude;

        void Start()
    {
        if(SceneManager.GetActiveScene().name != "Balloon_Game")
        {
            amplitude = 10 * GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>().transform.localScale.y;
        }
        else
        {
            amplitude = GameObject.FindObjectOfType<Canvas>().GetComponent<Canvas>().transform.localScale.y / 100;
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

        step += 0.035f;
        //Make sure Steps value never gets too out of hand 
        if (step > 999999) { step = 1; }

        //Float up and down along the y axis, 
        Vector3 point = transform.position;
        point.y = offsetY + Mathf.Sin(step) * amplitude; //0.5f / Screen.height*1000;
        transform.position = point;
    }
}

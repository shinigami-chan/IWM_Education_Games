using UnityEngine;
using System.Collections;
using Random = System.Random;
using System;

public class Floating : MonoBehaviour {

    private float step;
    private float offsetY; //How far to offset the object upwards
    private float offsetX;
         
        void Start()
    {
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
        point.y = Mathf.Sin(step) * 1.8f + offsetY;
        transform.position = point;
    }
}

using UnityEngine;
using System.Collections;
using Random = System.Random;
using System;

public class FlyAway : MonoBehaviour
{

    private float step; //A variable we increment
    private float offsetX;//How far to offset the object to the side

    void Start()
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //Create an offset based on our height
        offsetX = transform.position.x + transform.localScale.x;

        step = rnd.Next(0, 400) / 100 + rnd.Next(0, 300) / 100;
    }

    void Update()
    {
        flyAway();
    }

    void flyAway()
    {
        step += 0.035f;

        Vector3 point = transform.position;
        point.y += step;
        point.x = Mathf.Sin(step) * 15 + offsetX;
        transform.position = point;
    }
}

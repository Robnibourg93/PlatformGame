﻿using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    private Transform target;
    private float trackSpeed = 12;

    public void SetTarget(Transform t) {
        target = t;
    }

    void LateUpdate() {
        if (target)
        {
            float x = incrementTowards(transform.position.x, target.position.x, trackSpeed);
            float y = incrementTowards(transform.position.y, target.position.y, trackSpeed);

            transform.position = new Vector3(x,y, transform.position.z);
        }
    }

    private float incrementTowards(float n, float target, float a)
    {

        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }
}

﻿using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            c.GetComponent<Entity>().TakeDamage(10);
        }
    }
}
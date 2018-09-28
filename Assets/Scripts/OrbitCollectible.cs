using System;
using UnityEngine;
using System.Collections;

public class OrbitCollectible : MonoBehaviour
{


    public Transform center;
    public Vector3 axis = Vector3.up;
    public Vector3 desiredPosition;

    public float radius = 0.6f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    public float currentSpeed;
    public float acceleration = 45f; 
    public float targetSpeed = 15f;

    private Vector3 playerPos;
    private Vector3 posIncrement = new Vector3(0f, 1.7f, 0f);

    public bool pickedUp = false;

    //Runs at start of the game
    void Start()
    {
        //find Center location Sphere
        center = GameObject.FindGameObjectWithTag("Collectible").transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    //is called every frame
    void Update()
    {
        // if picked up is false maintain orbit around Center Sphere
        if (!pickedUp) {
            //rotate around own axis
            transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            //calculate posistion to move to.
            desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            //changes the position of the game object
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }
        else {
            //declare a GameObject player
            GameObject player;
            //instantiate player (Creates some sort of link between the player in game and the gameObject here)
            player = GameObject.FindGameObjectWithTag("Player");

            //increase y position so small orbs hit the body of the player
            playerPos = player.transform.position + posIncrement;
            currentSpeed = incrementTowards(currentSpeed, targetSpeed, acceleration);
            Debug.Log(currentSpeed);
            //move small orbs
            transform.position = Vector3.MoveTowards(transform.position, playerPos, currentSpeed *Time.deltaTime);
            //if position of the orb is equal to player position destroy this GameObject.
            if (playerPos == transform.position) {
                Destroy(this.gameObject);
            }
        }
    }
    private float incrementTowards(float n, float target, float a)
    {

        if (n == target)
        {
            return n;
        }
        else {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public float speed = 1;     // speed of the platform
    bool isMoving = false;      // change depending on if the user pressed the right key

    public Transform[] targets; // array of transforms for the platform to move to

    private int nextDest;
	// Use this for initialization
	void Start () { 
        transform.position = targets[0].position;

        nextDest = 1;
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        Movement();
	}

    // checks for user input, if correct key is pressed handle movement by checking the Fire1 input
    private void HandleInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isMoving = !isMoving; // set isMoving to its inverse
        }
    }

    // user input with movement
    void Movement()
    {
        // if Fire1 input is false then return
        if(!isMoving)
        {
            return;
        }

        // calculate the distance from the current target
        float distance = Vector3.Distance(transform.position, targets[nextDest].position);

        // Has it arrived, if not caculate the step between the two points and move there
        if (distance > 0)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targets[nextDest].position, step);
        } else
        {
            nextDest++; // increase the index by 1 when you get to the destination

            // if you are outside of the array length go back to 0
            if(nextDest == targets.Length)
            {
                nextDest = 0;
            }
            isMoving = false;
        }
    }
}


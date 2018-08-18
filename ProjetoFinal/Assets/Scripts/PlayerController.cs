using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 movement;
    public float movespeed, turnspeed, runspeed;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        if (movespeed == 0)
            movespeed = 10f;
        if (turnspeed == 0)
            turnspeed = 10f;
        if (runspeed == 0)
            runspeed = 20f;
    }

    private void FixedUpdate() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
        Turn(horizontal, vertical);
    }

    private void Turn(float horizontal, float vertical) {
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection != Vector3.zero) {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnspeed));
        }
    }

    private void Move(float horizontal, float vertical) {
        float currentSpeed = Input.GetButton("Run") ? runspeed : movespeed;

        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    
}

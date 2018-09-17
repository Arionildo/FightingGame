using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThirdPerson1 : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float currentSpeed = 0;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private bool isRunning;
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string jump;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        UpdateStatus();
        GetCommand();
    }

    private void UpdateStatus()
    {
        isRunning = currentSpeed > 0;
    }
    
    private void GetCommand()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            float moveHorizontal = Input.GetAxis(horizontal);
            float moveVertical = Input.GetAxis(vertical);

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            if (movement.magnitude < 0.01f)
                return;

            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            if (Input.GetButton(jump))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        currentSpeed = controller.velocity.magnitude;

        animator.SetBool("RUNNING", isRunning);
    }
}

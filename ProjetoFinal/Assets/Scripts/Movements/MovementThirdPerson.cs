using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThirdPerson : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float currentSpeed = 0;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private bool isRunning;

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
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;
        //float verticalSpeed = controller.velocity.y;
        currentSpeed = controller.velocity.magnitude;

        animator.SetBool("RUNNING", isRunning);
    }
}

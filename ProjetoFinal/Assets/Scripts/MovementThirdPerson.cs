using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThirdPerson : MonoBehaviour
{
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string jump;
    [SerializeField] private string dash;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;
    private float currentDashTimer = 0f;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float currentSpeed = 0;
    public float gravity = 9.0F;
    public bool stunned = false;
    public float stuntimmer = 0f;
    public float initialDashTimer = .1f;
    public float dashCooldown = -1f;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!stunned)
        {
            GetCommand();
            Dash();
        }
        else if(stuntimmer > 0)
        {
            stuntimmer -= 1 * Time.deltaTime;
            if (stuntimmer <= 0)
            {
                stuntimmer = 0;
                stunned = false;
            }
        }
        
        
    }

    private void GetCommand()
    {
        if (controller.isGrounded)
        {
            float moveHorizontal = Input.GetAxis(horizontal);
            float moveVertical = Input.GetAxis(vertical);

            if (Input.GetButton(jump))
                moveDirection.y = jumpSpeed;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            if (movement.magnitude < 0.01f)
            {
                animator.SetBool("RUNNING", false);
                return;
            } else
            {
                animator.SetBool("RUNNING", true);
            }

            if (Input.GetButtonDown(dash) && currentDashTimer < dashCooldown)
                currentDashTimer = initialDashTimer;
            
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        currentSpeed = controller.velocity.magnitude;
    }

    public void Dash()
    {
        currentDashTimer -= .5f * Time.deltaTime;
        if (currentDashTimer > 0.0f)
        {
            float initialSpeed = speed;
            speed *= 10f;
            transform.Translate(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime, Space.World);
            speed = initialSpeed;
        }
    }
}

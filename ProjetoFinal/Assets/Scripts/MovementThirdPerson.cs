using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThirdPerson : MonoBehaviour
{
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string jump;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float currentSpeed = 0;
    public float gravity = 9.0F;
    private Animator animator;
    public bool stunned = false;
    public float stuntimmer = 0f;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!stunned)
        {
            GetCommand();
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
        CharacterController controller = GetComponent<CharacterController>();

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

            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        currentSpeed = controller.velocity.magnitude;
    }
}

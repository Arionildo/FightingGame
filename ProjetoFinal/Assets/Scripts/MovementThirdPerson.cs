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
    public float gravity = 20.0F;
    private Animator animator;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetCommand();
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
            {
                animator.SetBool("RUNNING", false);
                return;
            } else
            {
                animator.SetBool("RUNNING", true);
            }

            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            if (Input.GetButton(jump))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        currentSpeed = controller.velocity.magnitude;
    }
}

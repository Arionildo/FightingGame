using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovment2 : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float currentSpeed = 0;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;
    private bool isRunning;
    private int hp = 100;
    public Text vida;
    public bool podeAtacar = false;
    public int dano = 10;

    public int numeroColetores = 1;
    public int numeroFabricadores = 3;

    public float progresso;
    public float material = 20;



    // Use this for initialization
    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateStatus();
        GetCommand();
        GetAttack();
        vida.text = hp.ToString();

        material += numeroColetores * Time.deltaTime;
        progresso += numeroFabricadores * Time.deltaTime;

        if (progresso >= 10 && material >= 20)
        {
            speed += 3;
            material -= 20;
            progresso = 0;
        }
    }

    private void UpdateStatus()
    {
        isRunning = currentSpeed > 0;
    }

    private void GetAttack()
    {
        if (Input.GetKeyDown(KeyCode.M) && podeAtacar)
        {
            GameObject.Find("Player1").GetComponent<CharacterMovment>().TakeDamage(dano);
        }
    }

    private void GetCommand()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal2");
            float moveVertical = Input.GetAxis("Vertical2");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            if (movement.magnitude < 0.01f)
                return;
            transform.rotation = Quaternion.LookRotation(movement);


            transform.Translate(movement * speed * Time.deltaTime, Space.World);


            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //Vector3 horizontalVelocity = controller.velocity;
        //horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        //float horizontalSpeed = horizontalVelocity.magnitude;
        //float verticalSpeed = controller.velocity.y;
        currentSpeed = controller.velocity.magnitude;



        //animator.SetBool("RUNNING", isRunning);
    }

    public void TakeDamage(int dano)
    {
        hp -= dano;
        if (hp <= 0)
        {
            hp = 0;
            Destroy(gameObject);
        }
    }
}

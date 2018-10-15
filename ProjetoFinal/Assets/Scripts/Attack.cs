using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [SerializeField] private const float maxKeyComboTimer = 2f;
    [SerializeField] private KeyCode attackA;
    [SerializeField] private KeyCode attackB;
    [SerializeField] private KeyCode Defence;
    private Animator animator;
    private bool isAttacking;
    private float keyTimer;
    public string comboA;
    public string comboB;
    public Transform comboPlaceholder;
    public GameObject skillA;
    public GameObject skillB;
    public Character executor;
    public Queue currentCombo;
    public string helperCurrentCombo;
    public Text commandText;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentCombo = new Queue();
        executor = GetComponent<Character>();
        commandText.text = attackA.ToString() + " -> AttackA\n" +
                            attackB.ToString() + " -> AttackB\n" +
                            Defence.ToString() + " -> Defend";
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStatus();
        if ( !GetComponent<MovementThirdPerson>().stunned )
        {
            GetCommand();
        }
        UpdateKeyTimer();
    }

    private void UpdateKeyTimer()
    {
        keyTimer -= Time.deltaTime;
        if (currentCombo.Count > 0)
        {
            helperCurrentCombo = new String(currentCombo.OfType<char>().ToArray());

            if (helperCurrentCombo.Contains(comboA))
            {
                //EXECUTA A ANIMAÇÃO DO COMBO E LIMPA OS COMANDOS DIGITADOS PRA EVITAR LOOP
                currentCombo.Clear();
                Weapon special = Instantiate(skillA, comboPlaceholder).GetComponent<Weapon>();
                special.transform.parent = null;
                special.owner = executor;
            }

            if (helperCurrentCombo.Contains(comboB))
            {
                //EXECUTA A ANIMAÇÃO DO COMBO E LIMPA OS COMANDOS DIGITADOS PRA EVITAR LOOP
                currentCombo.Clear();
                Weapon special = Instantiate(skillB, comboPlaceholder).GetComponent<Weapon>();
                special.transform.parent = null;
                special.owner = executor;
            }

            if (keyTimer < 0.0f)
            {
                keyTimer = maxKeyComboTimer / 4;
            }
        } else
        {
            helperCurrentCombo = null;
        }
    }

    private void UpdateStatus()
    {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName(EAnimations.ATTACK01.ToString())
                    || animator.GetCurrentAnimatorStateInfo(0).IsName(EAnimations.ATTACK02.ToString());
        executor.isAttacking = isAttacking;
    }

    private void GetCommand()
    {
        if (!isAttacking && executor.IsAlive())
        {
            if (!executor.isDefending)
            {
                if (Input.GetKeyDown(attackA))
                {
                    animator.SetTrigger(EAnimations.ATTACK01.ToString());
                }

                if (Input.GetKeyDown(attackB))
                {
                    animator.SetTrigger(EAnimations.ATTACK02.ToString());
                }
            }

            if (Input.GetKey(Defence) && executor.cdShield == 0)
            {
                executor.isDefending = true;
            }
            else if (Input.GetKeyUp(Defence))
            {
                executor.isDefending = false;
            }
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.character.ToString() != "\0")
        {
            keyTimer = maxKeyComboTimer;
            try
            {
                if (Input.GetKeyDown(attackA) || Input.GetKeyDown(attackB))
                    currentCombo.Enqueue(Char.ToUpper(e.character));
            } catch (Exception ex)
            {
                Debug.Log(ex.StackTrace);
            }
        }
    }


}

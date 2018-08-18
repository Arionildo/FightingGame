using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;
    private float keyTimer;
    //public List<char> comboA = new List<char>{ 't', 'e', 't', 'a' };
    public string comboA;

    public const float maxKeyComboTimer = 2f;
    //public char[] currentCombo;
    public Queue currentCombo;
    public string helperCurrentCombo;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        //currentCombo = new char[10];
        currentCombo = new Queue();
        comboA = "TETA";
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStatus();
        GetCommand();
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
                Debug.Log("ComboA executado!");
                currentCombo.Clear();
            }

            if (keyTimer < 0.0f)
            {
                keyTimer = maxKeyComboTimer / 4;
                Debug.Log("Retirou o botão: " + currentCombo.Dequeue());
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
    }

    private void GetCommand()
    {
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                animator.SetTrigger(EAnimations.ATTACK01.ToString());

            if (Input.GetKeyDown(KeyCode.E))
                animator.SetTrigger(EAnimations.ATTACK02.ToString());
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.character.ToString() != "\0")
        {
            Debug.Log("Inseriu o botão: " + e.character);
            keyTimer = maxKeyComboTimer;
            try
            {
                //currentCombo[index++] = e.character;
                currentCombo.Enqueue(Char.ToUpper(e.character));
            } catch (Exception ex)
            {
                Debug.Log(ex.StackTrace);
            }
        }
    }
}

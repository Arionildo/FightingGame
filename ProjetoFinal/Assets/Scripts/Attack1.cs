﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking;
    private float keyTimer;
    public string comboA;
    public string comboB;
    public Transform comboPlaceholder;
    public GameObject explosion;
    public GameObject ray;

    public const float maxKeyComboTimer = 2f;
    public Queue currentCombo;
    public string helperCurrentCombo;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentCombo = new Queue();
        comboA = "+--";
        comboB = "--+";
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
                Instantiate(explosion, comboPlaceholder).transform.parent = null;
            }

            if (helperCurrentCombo.Contains(comboB))
            {
                //EXECUTA A ANIMAÇÃO DO COMBO E LIMPA OS COMANDOS DIGITADOS PRA EVITAR LOOP
                Debug.Log("ComboB executado!");
                currentCombo.Clear();
                Instantiate(ray, comboPlaceholder).transform.parent = null;
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
    }

    private void GetCommand()
    {
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                animator.SetTrigger(EAnimations.ATTACK01.ToString());
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                animator.SetTrigger(EAnimations.ATTACK02.ToString());
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
                currentCombo.Enqueue(Char.ToUpper(e.character));
            } catch (Exception ex)
            {
                Debug.Log(ex.StackTrace);
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [SerializeField] private const float maxKeyComboTimer = 2f;
    [SerializeField] private string attackA = "AttackA";
    [SerializeField] private string attackB = "AttackB";
    [SerializeField] private string defence = "Defence";
    [SerializeField] private float cooldownSkillA;
    [SerializeField] private float cooldownSkillB;
    private float keyTimer;
    private Animator animator;
    private bool isAttacking;
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
        SetTextAttackCommand();
        cooldownSkillB = skillB.GetComponent<Weapon>().cooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStatus();
        if (!GetComponent<MovementThirdPerson>().stunned)
        {
            GetCommand();
        }
        UpdateKeyTimer();
    }

    private void SetTextAttackCommand()
    {
        SerializedObject inputManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axisArray = inputManager.FindProperty("m_Axes");
        string commandTextAttackA = null;
        string commandTextAttackB = null;
        string commandTextDefence = null;
        for (int i = 0; i < axisArray.arraySize; ++i)
        {
            var axis = axisArray.GetArrayElementAtIndex(i);
            var axisName = axis.FindPropertyRelative("m_Name").stringValue;

            if (axisName.Equals(attackA))
                commandTextAttackA = axis.FindPropertyRelative("positiveButton").stringValue;
            else if (axisName.Equals(attackB))
                commandTextAttackB = axis.FindPropertyRelative("positiveButton").stringValue;
            else if (axisName.Equals(defence))
                commandTextDefence = axis.FindPropertyRelative("positiveButton").stringValue;
        }

        commandText.text = commandTextAttackA + " -> AttackA\n" +
                            commandTextAttackB + " -> AttackB\n" +
                            commandTextDefence + " -> Guard";
    }

    private void UpdateKeyTimer()
    {
        keyTimer -= Time.deltaTime;
        if (currentCombo.Count > 0)
        {
            helperCurrentCombo = new String(currentCombo.OfType<char>().ToArray());

            if (helperCurrentCombo.Contains(comboA) && cooldownSkillA <= 0f)
            {
                cooldownSkillA = skillA.GetComponent<Weapon>().cooldown;
                currentCombo.Clear();
                Weapon special = Instantiate(skillA, comboPlaceholder).GetComponent<Weapon>();
                special.transform.parent = null;
                special.owner = executor;
            }

            if (helperCurrentCombo.Contains(comboB) && cooldownSkillB <= 0f)
            {
                cooldownSkillB = skillB.GetComponent<Weapon>().cooldown;
                currentCombo.Clear();
                Weapon special = Instantiate(skillB, comboPlaceholder).GetComponent<Weapon>();
                special.transform.parent = null;
                special.owner = executor;
            }

            if (keyTimer < 0.0f)
            {
                keyTimer = maxKeyComboTimer / 4;
            }
        }
        else
        {
            helperCurrentCombo = null;
        }
    }

    private void UpdateStatus()
    {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName(EAnimations.ATTACK01.ToString())
                    || animator.GetCurrentAnimatorStateInfo(0).IsName(EAnimations.ATTACK02.ToString());
        executor.isAttacking = isAttacking;
        cooldownSkillA -= 1f * Time.deltaTime;
        cooldownSkillB -= 1f * Time.deltaTime;
    }

    private void GetCommand()
    {
        if (!isAttacking && executor.IsAlive())
        {
            if (!executor.isDefending)
            {
                if (Input.GetButtonDown(attackA) || Input.GetButtonDown(attackB))
                    keyTimer = maxKeyComboTimer;

                if (Input.GetButtonDown(attackA))
                {
                    animator.SetTrigger(EAnimations.ATTACK01.ToString());
                    currentCombo.Enqueue('Q');
                }

                if (Input.GetButtonDown(attackB))
                {
                    animator.SetTrigger(EAnimations.ATTACK02.ToString());
                    currentCombo.Enqueue('E');
                }
            }

            if (Input.GetButtonDown(defence) && executor.cdShield == 0)
            {
                executor.isDefending = true;
                executor.shieldobject.SetActive(true);
            }
            else if (Input.GetButtonUp(defence))
            {
                executor.isDefending = false;
                executor.shieldobject.SetActive(false);
            }
        }
    }
}
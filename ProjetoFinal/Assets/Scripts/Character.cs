using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int id;
    public float maxLife;
    public float currentLife;
    public float energy;
    public Text lifeText;
    public Slider barSlider;
    public bool isAttacking;
    public float mass;
    public Vector3 impact = Vector3.zero;
    private CharacterController cc;
    public Material characterColor;
    public Material deadColor;
    private Renderer modelRenderer;
    private Collider modelCollider;
    public bool isDefending;
    public float shieldEnergy = 100;
    public float cdShield = 0;
    

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        currentLife = maxLife;
        modelRenderer = transform.Find("Model").GetComponent<Renderer>();
        modelCollider = transform.Find("Model").transform.GetComponent<Collider>();
    }

    private void Update()
    {
        UpdateLife();
        CheckImpact();
        Defend();
    }

    private void UpdateLife()
    {
        if (!IsAlive())
        {
            if (modelRenderer != null)
                modelRenderer.material = deadColor;
            if (modelCollider != null)
                modelCollider.enabled = false;
            lifeText.text = "DEAD";
            cc.enabled = false;
            Invoke("Respawn", 3f);
        } else
        {
            lifeText.text = currentLife.ToString();
            barSlider.value = (currentLife / maxLife) * 100;
        }
    }

    public bool IsAlive()
    {
        return currentLife > 0;
    }

    private void Respawn()
    {
        if (modelRenderer != null)
            modelRenderer.material = characterColor;
        if (modelCollider != null)
            modelCollider.enabled = true;
        currentLife = maxLife;
        cc.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDefending)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (other.tag.Equals("Weapon"))
                if (!id.Equals(weapon.owner.id))
                    TakeDamage(weapon, 100f);

            if (other.tag.Equals("Special"))
                if (!id.Equals(weapon.owner.id))
                    TakeDamage(weapon, 100f);
        }
        else
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (other.tag.Equals("Weapon"))
                if (!id.Equals(weapon.owner.id))
                    shieldEnergy -= 15;

            if (other.tag.Equals("Special"))
                shieldEnergy -= 20;
        }
    }

    private void TakeDamage(Weapon weapon, float impact)
    {
        currentLife -= weapon.damage;

        switch (weapon.skillType)
        {
            case ESkillType.HOOK_STUN:
                AddImpact(weapon.owner.transform.InverseTransformDirection(Vector3.forward), impact*2);
                break;
            default:
                AddImpact(weapon.owner.transform.TransformDirection(Vector3.forward), impact);
                break;
        }
    }

    public void AddImpact(Vector3 direction, float force)
    {
        direction.Normalize();
        if (direction.y < 0) direction.y = -direction.y;
        impact += direction.normalized * force / mass;
    }

    private void CheckImpact()
    {
        if (impact.magnitude > 0.2)
        {
            cc.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    private void Defend()
    {
        if (shieldEnergy <= 0)
        {
            cdShield = 5;
            shieldEnergy = 1;
            isDefending = false;
        }
        if (shieldEnergy >= 0)
        {
            if (isDefending)
            {
                shieldEnergy -= 5 * Time.deltaTime;
            }
            else
            {
                if (shieldEnergy >= 100)
                {
                    shieldEnergy = 100;
                }
                else
                {
                    shieldEnergy += 15 * Time.deltaTime;
                }
            }
        }
        if(cdShield > 0)
        {
            cdShield -= 1 * Time.deltaTime;
            if (cdShield <= 0)
            {
                cdShield = 0;
            }
        }

        

    }
}

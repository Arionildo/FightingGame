using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int id;
    public float maxLife;
    public float currentLife;
    public float energy = 100;
    public float maxEnergy = 100;
    public Image fillBar;
    public Text lifeText;
    public Slider barSlider;
    public Slider barEnergySlider;
    public bool isAttacking;
    public float mass;
    public Vector3 impact = Vector3.zero;
    private CharacterController cc;
    public Material characterColor;
    public Material deadColor;
    public bool isDefending;
    public float cdShield = 0;
    public GameObject weaponPlaceholder;
    Color activeShield = new Color32(174, 240, 255, 255);
    Color upShieldColor = new Color32(12, 203, 255, 255);
    Color downShieldColor = new Color32(145, 145, 145, 255);
    public bool deactivatedShield = false;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        currentLife = maxLife;
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
        currentLife = maxLife;
        cc.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Pickup".Equals(other.tag))
            Pickup(other);
        else if ("Weapon".Equals(other.tag) || "Special".Equals(other.tag))
            Hit(other);
    }

    private void Hit(Collider other)
    {
        if (!isDefending)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (other.tag.Equals("Weapon"))
                if (!id.Equals(weapon.owner.id))
                    TakeDamage(weapon, 100f, 0.5f);

            if (other.tag.Equals("Special"))
                if (!id.Equals(weapon.owner.id))
                    TakeDamage(weapon, 100f, 0.5f);
        }
        else
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (other.tag.Equals("Weapon"))
                if (!id.Equals(weapon.owner.id))
                    energy -= 15;

            if (other.tag.Equals("Special"))
                energy -= 20;
        }
    }

    private void Pickup(Collider other)
    {
        foreach (Transform child in weaponPlaceholder.transform)
            Destroy(child.gameObject);

        Weapon weapon = other.GetComponent<Weapon>();
        weapon.transform.parent = weaponPlaceholder.transform;
        weapon.transform.position = weaponPlaceholder.transform.position;
        weapon.transform.rotation = weaponPlaceholder.transform.rotation;
        weapon.owner = this;
        weapon.enabled = true;
        other.tag = "Weapon";
    }

    private void TakeDamage(Weapon weapon, float impact, float stuntime)
    {
        currentLife -= weapon.damage;

        switch (weapon.skillType)
        {
            case ESkillType.HOOK_STUN:
                AddImpact(weapon.owner.transform.InverseTransformDirection(Vector3.forward), impact * 2);
                break;
            default:
                AddImpact(weapon.owner.transform.TransformDirection(Vector3.forward), impact);
                break;
        }
        if (stuntime > 0)
        {
            doStun(stuntime);
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
        if (impact.magnitude > 0.2 && cc.enabled)
        {
            cc.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    private void Defend()
    {
        if (cdShield <= 0 && energy >=100)
        {
            deactivatedShield = false;
        }
        if (energy <= 0)
        {
            deactivatedShield = true;
            cdShield = 5;
            energy = 1;
            isDefending = false;
        }
        if (energy >= 0)
        {
            if (isDefending)
            {
                fillBar.color = activeShield;
                energy -= 5 * Time.deltaTime;
            }
            else
            {
                if (deactivatedShield)
                {
                    fillBar.color = downShieldColor;
                }
                else
                {
                    fillBar.color = upShieldColor;
                }
                if (energy >= 100)
                {
                    energy = 100;
                }
                else
                {
                    energy += 15 * Time.deltaTime;
                }
            }
        }
        barEnergySlider.value = (energy / maxEnergy) * 100;
        if (cdShield > 0)
        {
            cdShield -= 1 * Time.deltaTime;
            if (cdShield <= 0)
            {
                cdShield = 0;
            }
        }
    }


    public void doStun(float stuntime)
    {
        GetComponent<MovementThirdPerson>().stuntimmer = stuntime;
        GetComponent<MovementThirdPerson>().stunned = true;
    }
}

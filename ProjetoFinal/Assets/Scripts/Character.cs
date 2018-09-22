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
    public bool isAttacking;
    public float mass;
    public Vector3 impact = Vector3.zero;
    private CharacterController cc;
    public Material characterColor;
    public Material deadColor;
    private Renderer modelRenderer;
    private Collider modelCollider;

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
    }

    private void UpdateLife()
    {
        if (!IsAlive())
        {
            lifeText.text = "DEAD";
            modelRenderer.material = deadColor;
            cc.enabled = false;
            modelCollider.enabled = false;
            Invoke("Respawn", 3f);
        } else
        {
            lifeText.text = currentLife.ToString();
        }
    }

    public bool IsAlive()
    {
        return currentLife > 0;
    }

    private void Respawn()
    {
        currentLife = maxLife;
        modelRenderer.material = characterColor;
        cc.enabled = true;
        modelCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (!id.Equals(weapon.owner.id))
            {
                currentLife -= weapon.damage;
                AddImpact(weapon.owner.transform.TransformDirection(Vector3.forward), 100f);
            }
        }

        if (other.tag.Equals("Special"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            currentLife -= weapon.damage;
            if (weapon.owner != null)
                AddImpact(weapon.owner.transform.TransformDirection(Vector3.forward), 100f);
        }
    }

    private void AddImpact(Vector3 direction, float force)
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
}

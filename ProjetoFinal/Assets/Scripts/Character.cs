using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int id;
    public float life;
    public float energy;
    public Text lifeText;
    public bool isAttacking;
    public float mass;
    public Vector3 impact = Vector3.zero;
    private CharacterController cc;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateLife();
        CheckImpact();
    }

    private void UpdateLife()
    {
        if (life <= 0)
        {
            lifeText.text = "DEAD";
            Destroy(gameObject);
        } else
        {
            lifeText.text = life.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (!id.Equals(weapon.owner.id))
            {
                life -= weapon.damage;
                AddImpact(weapon.owner.transform.TransformDirection(Vector3.forward), 100f);
                print("qwewqe");
            }
        }

        if (other.tag.Equals("Special"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            life -= weapon.damage;
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

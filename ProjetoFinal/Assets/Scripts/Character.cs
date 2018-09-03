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
    
    private void Start()
    {
        //life = 100;
        //energy = 100;
    }

    private void Update()
    {
        UpdateLife();
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
                life -= weapon.damage;
        }

        if (other.tag.Equals("Special"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            life -= weapon.damage;
        }
    }
}

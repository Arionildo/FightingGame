﻿using System.Collections;
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
        lifeText.text = life.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (!id.Equals(weapon.owner.id))
                life -= weapon.damage;

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (other.tag.Equals("Special"))
        {
            life -= 100;

            if (life <= 0)
            {
                lifeText.text = "DEAD";
                Destroy(gameObject);
            }
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int id;
    public float life;
    public float energy;
    
    private void Start()
    {
        life = 100;
        energy = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (!id.Equals(weapon.owner.id))
                life -= weapon.damage;

            if ((life - weapon.damage) <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

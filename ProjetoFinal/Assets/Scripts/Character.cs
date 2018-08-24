using System.Collections;
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
            life -= other.GetComponent<Weapon>().damage;

            if ((life - other.GetComponent<Weapon>().damage) <= 0)
                Destroy(gameObject);
        }
    }
}

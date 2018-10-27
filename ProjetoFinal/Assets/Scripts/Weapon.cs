using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage;
    public Character owner;
    public Collider weaponCollider;
    public ESkillType skillType;
    public float cooldown;

	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(GetComponent<Collider>(), transform.root.GetComponent<Collider>());

        weaponCollider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        SetCollider();
	}

    private void SetCollider()
    {
        weaponCollider.enabled = owner != null && owner.isAttacking ? true : false;
    }
}

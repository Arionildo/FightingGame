using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage;
    public Character owner;

	// Use this for initialization
	void Start () {
        if (transform.root != null)
        {
            owner = transform.root.GetComponent<Character>();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

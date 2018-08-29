using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player2")
        {
            GameObject.Find("Player1").GetComponent<CharacterMovment>().podeAtacar = true;
        }
      
    }

    void OnTriggerExit(Collider other)
    {
        
        GameObject.Find("Player1").GetComponent<CharacterMovment>().podeAtacar = false;
        
    }

}

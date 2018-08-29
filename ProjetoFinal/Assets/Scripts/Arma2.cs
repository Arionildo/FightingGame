using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma2 : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player1")
        {
            GameObject.Find("Player2").GetComponent<CharacterMovment2>().podeAtacar = true;
        }
      
    }

    void OnTriggerExit(Collider other)
    {
        
        GameObject.Find("Player2").GetComponent<CharacterMovment2>().podeAtacar = false;
        
    }

}

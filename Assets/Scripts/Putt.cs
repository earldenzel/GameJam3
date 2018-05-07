using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Putt : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<ConstantForce>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<ConstantForce>().enabled = true;
        }
    }

    void OnColissionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}

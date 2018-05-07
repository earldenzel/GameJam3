using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball") {
            Debug.Log("Dunked son");
            other.GetComponent<ballcontrol>().inHole = true;
        }
    }
}

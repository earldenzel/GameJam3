using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour {
    public float accelerate;
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Ball")
        {
            Debug.Log("fast");
            other.gameObject.GetComponent<Rigidbody>().velocity = transform.forward*accelerate;
            //GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, accelerate);
        }
        
    }
}

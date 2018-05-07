using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    public GameObject powerUpPrefab;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            if(other.GetComponent<ballcontrol>().ActiveGolfer.PowerUp == null){
                other.GetComponent<ballcontrol>().ActiveGolfer.PowerUp = powerUpPrefab;
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivot : MonoBehaviour {

    public Transform ballTransform;
    float angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = ballTransform.position; // transform.parent.position
        transform.eulerAngles = new Vector3(90, angle, 0);


        if (Input.GetKey("a"))
        {
            //transform.Rotate(0, 0, 1);
            angle -= 50 * Time.deltaTime;
        }

        if(Input.GetKey("d"))
        {
            //transform.Rotate(0, 0, -1);
            angle += 50 * Time.deltaTime;
        }

        if (Input.GetKeyDown("space"))
        {
            GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 0);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
    public LayerMask magneticlayer;
    public Vector3 position;
    public float radius;
    public float force;
    public turnManager tm;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame

    void FixedUpdate()
    {
        Collider[] colliders;
        Rigidbody rigidbody;
        colliders = Physics.OverlapSphere(transform.position + position, radius, magneticlayer);
        foreach (Collider collider in colliders)
        {
            rigidbody = (Rigidbody)collider.gameObject.GetComponent(typeof(Rigidbody));
            if (rigidbody == null)
            {
                continue;
            }
            rigidbody.AddExplosionForce(force * -1, transform.position + position, radius);
        }
        //if (tm.turn == TurnMode.Placement)
        //{
        //    
        //}
    }
}

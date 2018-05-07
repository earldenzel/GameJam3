using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    public Transform Follow;
    public turnManager TurnManager;
    public PlayerStats activePlayer;
    public CanvasController canvasController;
    GameObject powerUpReference;
    public LayerMask groundMask;
    

    // Use this for initialization
    void Start () {
		
	}

    public void PlacePowerUp() {
        //enable physics and powerup functionality
        powerUpReference = null;

    }

    // Update is called once per frame
    void Update () {

        //GetComponent<Rigidbody>().velocity = new Vector3 (0, 0,Follow.GetComponent<Rigidbody>().velocity.z);
        if (TurnManager.turn == TurnMode.Putting)
        {
            if (activePlayer != null) {
                activePlayer = null;
            }

            transform.position = Follow.position;

            //camera 
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }
        else if (TurnManager.turn == TurnMode.Placement)
        {
            //camera panning
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * 5, 0, Input.GetAxis("Vertical") * Time.deltaTime * 5);
            transform.Translate(movement);

            //camera 
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

            if (activePlayer != null)
            {
                //spawn power up prefab into scene - once
                if (powerUpReference == null)
                {
                    //ui text. can't change these anywhere because of null
                    canvasController.ChangePowerUp(activePlayer.PowerUp.name);

                    powerUpReference = Instantiate(activePlayer.PowerUp, transform.position, transform.rotation) as GameObject;
                    activePlayer.PowerUp = null;
                }

                //adjust prefab postion and rotation
                RaycastHit hit;
                Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out hit, 15, groundMask);
                if (hit.collider)
                {
                    powerUpReference.transform.position = hit.point;
                    //powerUpReference.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal);


                }
                else
                {
                    powerUpReference.transform.position = transform.position;
                }

                if (Input.GetKey("q"))
                {
                    powerUpReference.transform.Rotate(0, -50 * Time.deltaTime, 0);
                    //Vector3 eulerRot = powerUpReference.transform.rotation.eulerAngles;
                }
                else if (Input.GetKey("e"))
                {
                    powerUpReference.transform.Rotate(0, 50 * Time.deltaTime, 0);
                }

                
            }

        }
        

	}
}

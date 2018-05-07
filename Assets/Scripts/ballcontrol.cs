using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballcontrol : MonoBehaviour {

    //bool animate = true;
    private AudioSource fxputt;
    public Image power;
    public bool direction = true;
    public Transform clubObj;
    public float zforce = 400;
    public Transform pointer;
    public bool inPlay = false;
    public bool inHole = false;
    public PlayerStats ActiveGolfer;

    public Transform StartingPoint;

    public float brakingSpeed;


    float launchTime;
 
    // Use this for initialization
    void Start () {
        fxputt = GetComponent<AudioSource>();
    }

    public void NextLevel() {
        //transform.position = Vector3.zero;
        //Debug.Log("Reset Ball Position");
        transform.position = StartingPoint.position;
        inHole = false;
    }

    // Update is called once per frame
    void Update () {

        
            //Vector3 newVelocity;
         //GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity,Vector3.zero, Time.time - launchTime ); 
        


        //bar moves down
        if (zforce > 1500)
        {
            direction = false;
        }
        //bar moves up
        if (zforce < 20)
        {
            direction = true;
        }

        //Speed at which the bar fills up
        if (direction)
        {
            zforce += 1000 * Time.deltaTime;
        }
        else
        {
            zforce -= 1000 * Time.deltaTime;
        }

        if (!inPlay)
        {
            power.fillAmount = (zforce / 1480);
        }

        //Launch the ball
        if (Input.GetKeyDown("space") && !inPlay && ActiveGolfer !=null)
        {
            GetComponent<Rigidbody>().AddForce(pointer.up * zforce);
            inPlay = true;
            //animate = false;
            launchTime = Time.time;
            fxputt.Play();
            //AudioSource.Play();
            //Debug.Log("start ball putt now");
        }

        if (inPlay && ActiveGolfer != null)
        {
            //Debug.Log("ball has been in motion for " + (Time.time - launchTime) + " seconds");
            if ((Time.time - launchTime) > 8f)
            {
               // Debug.Log("start slowing down ball");
                GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity, Vector3.zero, brakingSpeed * Time.deltaTime);
            }
        }
        else if(!inPlay) {
           // Debug.Log("stop ball");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        //if (Input.GetKeyDown("space"))
        //{
        //    GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
        //    //Instantiate(clubObj, transform.position, clubObj.rotation);
        //    GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        //    pointer.GetComponent<Transform>().position = transform.position;
        //}
        ////Aim left
        //if (Input.GetKey("a"))
        //{
        //    transform.Rotate(0, -1, 0);
        //}
        ////Aim right
        //if (Input.GetKey("d"))
        //{
        //    transform.Rotate(0, 1, 0);
        //}

        //Power up
       // if (Input.GetKey("w"))
       // {
       //     zforce += 5;
       // }
       // //Power down
       // if (Input.GetKey("s"))
       // {
       //     zforce -= 5;
       // }
        //Power min
       // if (zforce < 20 )
       // {
       //     zforce = 20;
       // }
       // //Power max
       // if (zforce > 1000)
       // {
       //     zforce = 1000;
       // }


    }

    //void OnTriggerEnter(Collider other)
    //{
    //    //Ball goes into hole code
    //}
    //IEnumerator delayLoad()
    //{
    //    yield return new WaitForSeconds(5);
    //    SceneManager.LoadScene("insert next hole here");
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour {

    public TurnMode turn = TurnMode.Placement;
    public GameObject Ball;
    public GameObject cameraMover;
    public CanvasController canvasController;
    public float waitTime;
    public float messageTime = 5;
    public GameObject[] powerups;
    private AudioSource trap;

    // Use this for initialization
    void Start ()
    {
        trap = GetComponent<AudioSource>();
        RandomizeOrder();
        RandomizeItems();
        StartCoroutine("GameLoop");
    }

    void RandomizeOrder()
    {
        int additive = Random.Range(0, 3);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            int playerOrder = player.GetComponent<PlayerStats>().order + additive;
            if (playerOrder % 4 == 0)
            {
                playerOrder = 4;
            }
            else
            {
                playerOrder = playerOrder % 4;
            }
            player.GetComponent<PlayerStats>().order = playerOrder;
        }
    }

    void RandomizeItems()
    {
        //int numberOfPowerups = powerups.Length;
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //foreach (GameObject player in players)
        //{
        //    int chance = Random.Range(0, numberOfPowerups);
        //    player.GetComponent<PlayerStats>().PowerUp = powerups[chance];
        //}
    }

    IEnumerator GameLoop() {
        Debug.Log(playerPuttCount());
        while (playerPuttCount() > 1) {
            switch (turn) {
                case TurnMode.Placement:
                    canvasController.TogglePowerBar(false);
                    //for each player
                    List<PlayerStats> players = sortedPlayers();
                    foreach (PlayerStats player in players) {
                        //wait for controller confirmation
                        if (player.PowerUp != null)
                        {
                                while (true) {
                                    if (Input.anyKey) {
                                        break;
                                    }
                                    yield return null;
                                }

                            //start timer
                            float startTime = Time.time;

                            //if player canputt is false give a random power up for placement

                            cameraMover.GetComponent<camera>().activePlayer = player;
                            canvasController.ChangePlayerText(player.name, turn);
                        
                            //allow placement
                            

                            //REPLACE WITH TIME DURATION VARIABLE
                            //while timer
                            while ( (Time.time - startTime < waitTime)){
                                canvasController.ShowTime(waitTime - (Time.time - startTime));
                                //display timer ui
                               // Debug.Log("Turn timer: " + (Time.time - startTime));

                                //invoke function on character for power placement. pass in player

                                if (Input.GetKeyDown("space"))
                                {
                                    trap.Play();
                                    startTime -= 20;
                                }
                                yield return null;
                            }

                            canvasController.HideTime();

                            //end placement
                            cameraMover.GetComponent<camera>().PlacePowerUp();                        

                            //end turn UI 
                            //Debug.Log("Player turn is finished");
                        }
                    }

                    //change turnmode to putting
                    turn = TurnMode.Putting;
                    break;
                case TurnMode.Putting:
                    canvasController.RemovePowerUpText();
                    List<PlayerStats> players2 = sortedPlayers();
                    ballcontrol bc = Ball.GetComponent<ballcontrol>();
                    foreach (PlayerStats player in players2)
                    {
                        if (player.canPutt) {
                            canvasController.PuttMessage("Press any key to start putting!");
                            canvasController.TogglePowerBar(true);

                            //prompt for controller confirmation
                            //Debug.Log("Press a key to start your turn : " + player.name);
                            canvasController.ChangePlayerText(player.name, turn);

                            while (true)
                            {
                                if (Input.anyKey)
                                {
                                    break;
                                }
                                yield return null;
                            }

                            //////!!!!!!!!!!!!!!!!!!!!!!!!!
                            //bc.enabled = true;
                            
                            //wait for putt input
                            canvasController.PuttMessage("Press left mouse button to putt!");
                            //Debug.Log("waiting for putt input");
                            
                            bc.ActiveGolfer = player;
                            while (!bc.inPlay)
                            {
                                //Debug.Log("Checking if ball was hit");
                                if (bc.inPlay) {
                                    break;
                                }
                                yield return new WaitForSeconds(1);
                            }
                            canvasController.PuttMessage("Ball was hit!");
                            canvasController.TogglePowerBar(false);

                            //Debug.Log("waiting for ball to finish moving");
                            //wait for ball movement to finish
                            //while (Ball.GetComponent<Rigidbody>().velocity.magnitude == 0) {
                            //    yield return null;
                            //}

                            //double check
                            float timer = 0;
                            while (timer < 2) {
                                timer += Time.deltaTime;
                                if (Ball.GetComponent<Rigidbody>().velocity.magnitude > 0) {
                                    timer = 0;
                                }
                                yield return null;
                            }
                            //Debug.Log("Ball is now stopped we think");

                            //set the ball to not in play
                            bc.inPlay = false;
                            bc.ActiveGolfer = null;

                            if (bc.inHole)
                            {
                                //set player can putt bool to false
                                player.canPutt = false;
                                //show ui for player eliminated
                                //Debug.Log("Show ui that this player is eliminated");
                                canvasController.PuttMessage(player.name + " is eliminated");
                                canvasController.ShowDead(player.name);
                                canvasController.StartCoroutine("DunkedSon");

                                yield return new WaitForSeconds(messageTime);
                                
                                //if more than one player can putt
                                if (playerPuttCount() > 1)
                                {
                                    //Debug.Log("Reset scene for remaining players");
                                    canvasController.PuttMessage("Setting up next round!");
                                    yield return new WaitForSeconds(messageTime);
                                    //delete all power ups in game world
                                    GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
                                    for (var i = 0; i < powerups.Length; i++)
                                    {
                                        Destroy(powerups[i]);
                                    }

                                    //reset ball position
                                    bc.NextLevel();
                                    break;
                                }else
                                {
                                    canvasController.PuttMessage(player.name + ", you lose!");

                                }
                            }
                            ////////!!!!!!!!!!!!!!!!!!!!!!
                            //bc.enabled = false;
                        }
                    }

                    //get all powerups in the scene - send message to increase integer for turn count

                    if (playerPuttCount() > 1)
                    {
                        turn = TurnMode.Placement;
                    }
                    else {
                        turn = TurnMode.GameOver;
                    }
                    
                    break;
                default:
                    callWinner();
                    break;
            }
        }

        //else remaining player wins
        callWinner();

    }

    List<PlayerStats> sortedPlayers() {
        List<PlayerStats> sorted = new List<PlayerStats>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        while (sorted.Count != players.Length)
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<PlayerStats>().order == sorted.Count + 1)
                {
                    sorted.Add(player.GetComponent<PlayerStats>());
                }

            }
        }
        Debug.Log("Sorted player count : " + sorted.Count);
        return sorted;

    }

    int playerCount() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        return players.Length;
    }


    int playerPuttCount() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        

        int count = 0;

        foreach (GameObject gop in players) {
            PlayerStats ps = gop.GetComponent<PlayerStats>();
            if (ps.canPutt) {
                count++;
            }
        }
        return count;
    }

    void callWinner()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject gop in players)
        {
            PlayerStats ps = gop.GetComponent<PlayerStats>();
            if (ps.canPutt)
            {
                canvasController.PuttMessage(gop.name + ", you lose!");
                canvasController.ShowGameOver(gop.name);
            }
        }

    }
    
    // EPISODE GUIDE
    void Update()
    {

        //while more than one players can putt
        //switch on turnmode enum
        //case placement
        //for each player
        //wait for controller confirmation
        //start timer
        //while timer 
        //allow placement
        //end turn UI  
        //change turnmode to putting
        //case putting
        //foreach player with can putt bool
        //prompt for controller confirmation
        //wait for putt input
        //wait for ball movement to finish
        //if ball is in hole
        //set player can putt bool to false
        //show ui for player eliminated
        //if more than one player can put
        //delete all power ups in game world
        //reset ball position
        //else remaining player wins
        //change turnmode to placement
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

}

public enum TurnMode {
    Putting,
    Placement,
    GameOver
}

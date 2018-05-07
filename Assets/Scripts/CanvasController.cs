using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//ui setter
public class CanvasController : MonoBehaviour {

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public GameObject powerbar;
    public GameObject trajectoryIndicator;
    public GameObject turnsMenu;
    public GameObject gameOverMenu;
    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;
    public TextMeshProUGUI p3Text;
    public TextMeshProUGUI p4Text;
    public TextMeshProUGUI gameOverText;
    public GameObject dunked;

    public void ChangePlayerText(string playername, TurnMode mode)
    {
        playerText.text = playername + "\n" + mode.ToString() + " MODE";

        switch (playername.ToLower())
        {
            case "player1":
                p1Text.fontStyle = FontStyles.Bold;
                p2Text.fontStyle = FontStyles.Normal;
                p3Text.fontStyle = FontStyles.Normal;
                p4Text.fontStyle = FontStyles.Normal;
                p1Text.color = Color.black;
                p2Text.color = Color.white;
                p3Text.color = Color.white;
                p4Text.color = Color.white;
                break;
            case "player2":
                p1Text.fontStyle = FontStyles.Normal;
                p2Text.fontStyle = FontStyles.Bold;
                p3Text.fontStyle = FontStyles.Normal;
                p4Text.fontStyle = FontStyles.Normal;
                p1Text.color = Color.white;
                p2Text.color = Color.black;
                p3Text.color = Color.white;
                p4Text.color = Color.white;
                break;
            case "player3":
                p1Text.fontStyle = FontStyles.Normal;
                p2Text.fontStyle = FontStyles.Normal;
                p3Text.fontStyle = FontStyles.Bold;
                p4Text.fontStyle = FontStyles.Normal;
                p1Text.color = Color.white;
                p2Text.color = Color.white;
                p3Text.color = Color.black;
                p4Text.color = Color.white;
                break;
            case "player4":
                p1Text.fontStyle = FontStyles.Normal;
                p2Text.fontStyle = FontStyles.Normal;
                p3Text.fontStyle = FontStyles.Normal;
                p4Text.fontStyle = FontStyles.Bold;
                p1Text.color = Color.white;
                p2Text.color = Color.white;
                p3Text.color = Color.white;
                p4Text.color = Color.black;
                break;
            default:
                p1Text.fontStyle = FontStyles.Normal;
                p2Text.fontStyle = FontStyles.Normal;
                p3Text.fontStyle = FontStyles.Normal;
                p4Text.fontStyle = FontStyles.Normal;
                p1Text.color = Color.white;
                p2Text.color = Color.white;
                p3Text.color = Color.white;
                p4Text.color = Color.white;
                break;
        }
    }

    public void ShowDead(string playername)
    {
        switch (playername.ToLower())
        {
            case "player1":
                p1Text.GetComponentInParent<Image>().color = Color.gray;
                break;
            case "player2":
                p2Text.GetComponentInParent<Image>().color = Color.gray;
                break;
            case "player3":
                p3Text.GetComponentInParent<Image>().color = Color.gray;
                break;
            case "player4":
                p4Text.GetComponentInParent<Image>().color = Color.gray;
                break;
            default:
                break;
        }
    }

    public IEnumerator DunkedSon()
    {
        dunked.SetActive(true);
        yield return new WaitForSeconds(1f);
        dunked.SetActive(false);
    }

    public void ShowGameOver(string playername)
    {
        turnsMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameOverText.text = "CONGRATULATIONS, \n" + playername;
    }

    public void TogglePowerBar(bool powerbarStatus)
    {
        powerbar.SetActive(powerbarStatus);
        trajectoryIndicator.SetActive(powerbarStatus);
    }

    public void ChangePowerUp(string powerupname)
    {
        messageText.text = "Place power-up: " + powerupname;
    }

    public void RemovePowerUpText()
    {
        messageText.text = "";
    }

    public void ShowTime(float time)
    {
        timerText.text ="Time left:" + time.ToString();
    }

    public void HideTime()
    {
        timerText.text = "";
    }

    public void PuttMessage(string puttMessage)
    {
        messageText.text = puttMessage;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("WorkingScene");
    }
}

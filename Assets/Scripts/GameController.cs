using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColour
{
    public Color panelColour;
    public Color textColour;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public GameObject startInfo;

    public Player playerX; 
    public Player playerO; 
    public PlayerColour activePlayerColor; 
    public PlayerColour inactivePlayerColor;

    private string playerSide;
    private int moveCount;

    public void SetGameControllerForButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameController(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X") 
        { 
            SetPlayerColors(playerX, playerO); 
        }
        else 
        { 
            SetPlayerColors(playerO, playerX); 
        }

        StartGame();
    }

    public void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    public void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public void Awake() 
    {
        SetGameControllerForButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
    }

    public string GetPlayerSide() 
    {
        return playerSide; 
    }

    public void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";

        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    public void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColour;
        newPlayer.text.color = activePlayerColor.textColour;

        oldPlayer.panel.color = inactivePlayerColor.panelColour;
        oldPlayer.text.color = inactivePlayerColor.textColour;
    }

    public void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColour; 
        playerX.text.color = inactivePlayerColor.textColour; 
        playerO.panel.color = inactivePlayerColor.panelColour; 
        playerO.text.color = inactivePlayerColor.textColour;
    }

    public void EndTurn() 
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }            
    }

    public void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer == "draw") { 
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(playerSide + " Wins!");
        }

        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        moveCount = 0;

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }

        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
    }

    private void SetGameOverText(string text)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = text;
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}

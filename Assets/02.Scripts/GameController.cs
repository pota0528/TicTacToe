using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[System.Serializable]
public class Player
{
    public Image panel;
    public TMP_Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public TMP_Text[] buttonList;
    public GameObject gameOverPanel; // 게임 오버 패널
    public TMP_Text gameOverText; // 게임 오버 패널의 텍스트
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    
    private string playerside;
    private string computerside; // 컴퓨터
    public bool playerMove;
    public float delay;
    private int value;
    
    
    private int moveCount; // 이동 횟수
    
    private void Awake()
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0; // 이동 횟수 0으로 설정
        restartButton.SetActive(false); // 재시작 버튼 비활성화
        playerMove = true;
    }

    private void Update()
    {
        if (playerMove == false)
        {
            delay += delay * Time.deltaTime;
            if (delay >= 100)
            {
                value = Random.Range(0, 8);
                if (buttonList[value].GetComponentInParent<Button>().interactable == true)
                {
                    buttonList[value].text = GetComputerSide();
                    buttonList[value].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                }
            }
        }
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide) // 시작 도형선택
    {
        playerside = startingSide;
        if (playerside == "X") // 만약 플레이어쪽이 X라면
        {
            computerside = "O"; // 컴퓨터는 O
            SetPlayerColors(playerX, playerO); // X, O 순서로 플레이어 색 설정
        }
        else
        {
            computerside = "X"; // 플레이어가 O면 컴퓨터는 X
            SetPlayerColors(playerO, playerX); // O, X 순서로 플레이어 색 설정
        }
        
        StartGame();
        
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false); // 시작 시 플레이어 선택 버튼 비활성화
        startInfo.SetActive(false); // 시작 시 안내문구 비활성화
    }
    
    public string GetPlayerSide()
    {
        return playerside; // 플레이어로 반환
    }
    
    public string GetComputerSide()
    {
        return computerside; // 컴퓨터로 반환
    }

    public void EndTurn()
    {
        moveCount++;
        
        // 행과 열 확인 후 3개의 공간이 플레이어 선택과 일치하는지 확인
        if (buttonList[0].text == playerside && buttonList[1].text == playerside && buttonList[2].text == playerside)
        {
            // 왼쪽 상단부터 3개가 모두 플레이어 선택과 같으면
            GameOver(playerside); // 게임종료
        }
        
        else if (buttonList[0].text == playerside && buttonList[1].text == playerside && buttonList[2].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[3].text == playerside && buttonList[4].text == playerside && buttonList[5].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[6].text == playerside && buttonList[7].text == playerside && buttonList[8].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[0].text == playerside && buttonList[3].text == playerside && buttonList[6].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[1].text == playerside && buttonList[4].text == playerside && buttonList[7].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[2].text == playerside && buttonList[5].text == playerside && buttonList[8].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[0].text == playerside && buttonList[4].text == playerside && buttonList[8].text == playerside)
        {
            GameOver(playerside); 
        }
        
        else if (buttonList[2].text == playerside && buttonList[4].text == playerside && buttonList[6].text == playerside)
        {
            GameOver(playerside); 
        }
        
        // 컴퓨터 부분 시작
        
        else if (buttonList[0].text == computerside && buttonList[1].text == computerside && buttonList[2].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[3].text == computerside && buttonList[4].text == computerside && buttonList[5].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[6].text == computerside && buttonList[7].text == computerside && buttonList[8].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[0].text == computerside && buttonList[3].text == computerside && buttonList[6].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[1].text == computerside && buttonList[4].text == computerside && buttonList[7].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[2].text == computerside && buttonList[5].text == computerside && buttonList[8].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[0].text == computerside && buttonList[4].text == computerside && buttonList[8].text == computerside)
        {
            GameOver(computerside); 
        }
        
        else if (buttonList[2].text == computerside && buttonList[4].text == computerside && buttonList[6].text == computerside)
        {
            GameOver(computerside); 
        }
        
        // 컴퓨터 부분 끝
        
        else if (moveCount >= 9) // 만약 이동횟수가 9보다 크거나 같으면
        {
            GameOver("DRAW!");
        }
        else
        {
            ChangeSides();
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor; // 턴 차례인 플레이어 색상을 activePlayer 색상으로 패널색 변경
        newPlayer.text.color = activePlayerColor.textColor; // 턴 차례인 플레이어 패널의 텍스트도 변경
        oldPlayer.panel.color = inactivePlayerColor.panelColor; // 턴 끝난 플레이어 색상을 inactivePlayer 색상으로 패널색 변경
        oldPlayer.text.color = inactivePlayerColor.textColor; // 텍스트도 변경
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer == "draw")
        { 
            SetGameOverText("DRAW!");
            SetPlayerColorsInactive(); // 비겼을때도 어느 플레이어도 강조하지 않도록 비활성화 색으로 변경
        }
        else
        {
            SetGameOverText(playerside + " Wins!");
        }
        
        restartButton.SetActive(true); // 게임이 끝났으므로 재시작 버튼 활성화
    }

    void ChangeSides() // 팀변경
    {
        //playerside = (playerside == "X") ? "O" : "X";
        playerMove = (playerMove == true) ? false : true;

        //if (playerside == "X") // 만약 플레이어가 X라면
        if (playerMove == true)
        {
            SetPlayerColors(playerX, playerO); // X와 O순서로 색깔 설정
        }
        else // O라면
        {
            SetPlayerColors(playerO, playerX); // O와 X순서로 색깔 설정
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false); // 재시작시 게임 오버 패널 비활성화
        restartButton.SetActive(false); // 재시작하면 재시작 버튼 비활성화
        SetPlayerButtons(true); // 재시작시 플레이어 선택 버튼 활성화
        SetPlayerColorsInactive();
        startInfo.SetActive(true); // 재시작시 안내문구 활성화
        playerMove = true;
        delay = 10;
        
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive() // 플레이어 턴이 넘어가 비활성화 될때 변경될 색 설정
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}

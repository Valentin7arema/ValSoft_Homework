using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Image playerOne;
    public Image playerTwo;
    public Sprite[] playerSprites;
    public Text manageText;

    private int gameStage = 0;
    private float timer = 0;
    private float bangTimer = 0;
    private bool isPlayerOneShot = false;
    private bool isPlayerTwoShot = false;


    private bool _isComputer = false;
    private float compShot = 0.5f;

    private int recordOne = 0;
    private int recordTwo = 0;
    private int maxRounds = 10;

    public InputField _input;
    public GameObject _panel;
    public GameObject StartButton;
    public GameObject ExitButton;
    public Text recOne;
    public Text recTwo;

    public GameObject PlayerTwoShotButton;

    public void StartGame() {
        manageText.text = "ready";
        playerOne.sprite = playerSprites[1];
        playerTwo.sprite = playerSprites[1];
        gameStage = 1;
        compShot = UnityEngine.Random.Range(0.5f, 1f);
        if (_isComputer) PlayerTwoShotButton.SetActive(false);
        maxRounds = Convert.ToInt32(_input.text);
    }

    public void Update() {
        recOne.text = "Player One: " + recordOne.ToString();
        recTwo.text = "Player Two: " + recordTwo.ToString();
        if (gameStage == 1) {
            if (timer >= 1.0f) {
                manageText.text = "steady";
                bangTimer = UnityEngine.Random.Range(1.0f, 3.0f);
                gameStage = 2;
                timer = 0;
            } else {
                timer += Time.deltaTime;
            }
        } else if (gameStage == 2) {
            if (timer >= bangTimer) {
                manageText.text = "!bang!";
                bangTimer = 0;
                gameStage = 3;
                timer = 0;
            } else {
                timer += Time.deltaTime;
            }
        }

        if (gameStage == 3 && _isComputer)
        {
            compShot -= Time.deltaTime;
            if (compShot <= 0) Shot(2);
        }

        if (maxRounds==recordOne || maxRounds == recordTwo)
        {
            recordOne = 0;
            recordTwo = 0;
            StartButton.SetActive(false);
            _panel.SetActive(true);
        }

    }

    public void Shot(int player) {
        if (gameStage == 0) return;
            Image activePlayer = (player == 1) ? playerOne : playerTwo;
        if (gameStage == 3) {
            Image enemyPlayer = (player == 2) ? playerOne : playerTwo;
            activePlayer.sprite = playerSprites[2];
            enemyPlayer.sprite = playerSprites[3];
            manageText.text = "player " + player + " win!";
            if (player == 1) { recordOne++; } else { recordTwo++; }

            gameStage = 0;
            isPlayerOneShot = false;
            isPlayerTwoShot = false;
        } else {
            if (player == 1) isPlayerOneShot = true;
            if (player == 2) isPlayerTwoShot = true;
            if (isPlayerOneShot && isPlayerTwoShot) {
                manageText.text = "Too fast, guys!";
                bangTimer = 0;
                gameStage = 0;
                timer = 0;
                isPlayerOneShot = false;
                isPlayerTwoShot = false;
            }
            activePlayer.sprite = playerSprites[4];
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Computer()
    {
        _isComputer = true;
    }

    public void DeComputer()
    {
        _isComputer = false;
    }
}

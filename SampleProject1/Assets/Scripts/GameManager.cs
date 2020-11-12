using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    int totalNumBalls { get; set; }
    

    public GameObject gameOverObject;
    public GameObject stageCompleteObject;
    public GameObject tryAgain;

    public static GameManager Instance;
    private int counter = 1;
    private GameObject currentBall;
    private int powerUpCounter = 0;
    public GameObject[] boxes;

    void Awake()
    {
        Instance = this;
        totalNumBalls = 3;
        StartGame();
    }

    public void StartGame()
    {
        if(counter <= totalNumBalls)
        {
            if (CheckIfWon())
            {
                StageComplete();
                return;
            }  
            currentBall = Instantiate(ball);
            currentBall.SetActive(true);
            counter++;
        }
        else
        {
            if (CheckIfWon())
            {
                StageComplete();
                return;
            }
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverObject.SetActive(true);
        tryAgain.SetActive(true);
    }

    public void StageComplete()
    {
        stageCompleteObject.SetActive(true);
        tryAgain.SetActive(true);
    }

    public void PowerUp()
    {
        if(powerUpCounter < 1)
        {
            currentBall.transform.GetChild(0).gameObject.SetActive(false);
            currentBall.transform.GetChild(1).gameObject.SetActive(true);
            powerUpCounter++;
        }
    }

    public bool CheckIfWon()
    {
        int counter = 0;
        bool result = false;
        for (int i =0; i<boxes.Length; i++)
        {
            if (boxes[i].GetComponent<BoxHitting>().status)
            {
                counter++;
            }
            if (counter == boxes.Length)
                result = true;
        }
        return result;
    }
    public void TryAgainFunc()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

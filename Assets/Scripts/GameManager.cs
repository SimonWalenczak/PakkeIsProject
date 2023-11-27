using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int totalPlayers;
    private int playersFinished = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already another GameManager in this scene !");
        }
    }

    private void Start()
    {
        totalPlayers = GameData.NumberOfPlayer;
    }

    public void PlayerFinished()
    {
        playersFinished++;

        if (playersFinished == totalPlayers)
        {
            LaunchFinalScene();
        }
    }

    void LaunchFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }
}

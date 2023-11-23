using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void ValidPreset(int index)
    {
        GameData.NumberOfPlayer = index;
        Debug.Log("Number of players : " + index);
        SceneManager.LoadScene("PlayerSetup");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void Quit()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Button _readyButton;

    private float ignoreInputTime = 0.5f;
    private bool inputEnabled;

    
    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        _titleText.SetText("Player " + (pi + 1));
        ignoreInputTime += Time.time;
    }

    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    // public void SetColor(Material color)
    // {
    //     if (!inputEnabled)
    //     {
    //         return;
    //     }
    //     
    //     PlayerConfigurationManager.Instance.SetPlayerColor(PlayerIndex, color);
    //     _readyPanel.SetActive(true);
    //     _readyButton.Select();
    //     _menuPanel.SetActive(false);
    // }

    public void ReadyPlayer()
    {
        if (!inputEnabled)
        {
            return;
        }
        
        PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
        _readyButton.gameObject.SetActive(false);
    }
}

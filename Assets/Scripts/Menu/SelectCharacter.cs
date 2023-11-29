using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private int PlayerIndex;
    
    public Image characterImage;
    public Sprite[] characterSprites;

    [SerializeField] private int selectedCharacterIndex = 0;
    
    [Header("Selection")] 
    [SerializeField] private bool IsSelected;
    
    [SerializeField] private Image _titlePlayer;
    [SerializeField] private List<Sprite> _titlePlayerSprites;
    [SerializeField] private List<GameObject> _meshCharacterPlayers;
    [SerializeField] private GameObject _actualMesh;
    [SerializeField] private GameObject _readyPanel;
    [SerializeField] private GameObject _menuPanel;

    private float ignoreInputTime = 0.5f;

    private void Start()
    {
        _actualMesh = _meshCharacterPlayers[0];
    }

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        _titlePlayer.sprite = _titlePlayerSprites[PlayerIndex];
        ignoreInputTime += Time.time;
    }

    public void SelectNextCharacter()
    {
        if (selectedCharacterIndex < characterSprites.Length - 1)
            selectedCharacterIndex = selectedCharacterIndex + 1;
        else
            selectedCharacterIndex = 0;

        UpdateCharacterSprite();
    }

    public void SelectPreviousCharacter()
    {
        if (selectedCharacterIndex > 0)
            selectedCharacterIndex = selectedCharacterIndex - 1;
        else
            selectedCharacterIndex = characterSprites.Length - 1;

        UpdateCharacterSprite();
    }

    void UpdateCharacterSprite()
    {
        characterImage.sprite = characterSprites[selectedCharacterIndex];
        _actualMesh = _meshCharacterPlayers[selectedCharacterIndex];
    }

    public void SetMesh()
    {
        PlayerConfigurationManager.Instance.SetPlayerMesh(PlayerIndex, _actualMesh, selectedCharacterIndex);
        PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
        _menuPanel.SetActive(false);
        _readyPanel.SetActive(true);
    }
}
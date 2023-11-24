using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public Image characterImage;
    public Sprite[] characterSprites;

    [SerializeField] private int selectedCharacterIndex = 0;

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
    }
}
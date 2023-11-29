using System;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    public List<Transform> SpawnPlayer;

    public List<GameObject> CharacterPlayer;

    private void Start()
    {
        for (int i = 0; i < PlayerConfigurationManager.Instance.AllPlayersAtTheEnd.Count; i++)
        {
            foreach (var player in PlayerConfigurationManager.Instance.playerConfigs)
            {
                if (PlayerConfigurationManager.Instance.AllPlayersAtTheEnd[i].numPlayer == player.NumPlayer)
                {
                    Instantiate(CharacterPlayer[player.MeshIndex], SpawnPlayer[i].position, SpawnPlayer[i].rotation);
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Auto Remplissage")]
    public List<PlayerRank> FinishedPlayers;
    public List<PlayerRank> DisqualifiedPlayers;

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

    private void Update()
    {
        if (FinishedPlayers.Count + DisqualifiedPlayers.Count ==
            PlayerConfigurationManager.Instance.playerConfigs.Count)
        {
            LaunchFinalScene();
        }
    }

    void LaunchFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }
}

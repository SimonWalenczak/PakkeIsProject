using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<PlayerRank> AllPlayers;
    public List<PlayerRank> FinalClassment;
    
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
        AllPlayers.Sort(new PlayerComparer());
        
        if (FinishedPlayers.Count + DisqualifiedPlayers.Count ==
            PlayerConfigurationManager.Instance.playerConfigs.Count)
        {
            StartCoroutine(LaunchFinalScene());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("----Classement----");
            foreach (var t in AllPlayers)
            {
                Debug.Log(t.numPlayer);
            }
            Debug.Log("----Fin de Classement----");
        }
    }

    IEnumerator LaunchFinalScene()
    {
        PlayerConfigurationManager.Instance.AllPlayersAtTheEnd.AddRange(FinishedPlayers);
        DisqualifiedPlayers.Reverse();
        PlayerConfigurationManager.Instance.AllPlayersAtTheEnd.AddRange(DisqualifiedPlayers);

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ScenePodium");
    }
}

public class PlayerComparer : IComparer<PlayerRank>
{
    public int Compare(PlayerRank x, PlayerRank y)
    {
        // Compare by laps
        if (x.currentNbLap != y.currentNbLap)
            return y.currentNbLap.CompareTo(x.currentNbLap);

        // If laps are equal, compare by checkpoints
        if (x.currentCheckpointIndex != y.currentCheckpointIndex)
            return y.currentCheckpointIndex.CompareTo(x.currentCheckpointIndex);

        // If checkpoints are equal, compare by distance to next checkpoint
        return x.distanceToNextCheckpoint.CompareTo(y.distanceToNextCheckpoint);
    }
}

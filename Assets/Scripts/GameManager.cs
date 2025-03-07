using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<PlayerRank> AllPlayers;
    public List<PlayerRank> FinalClassment;
    public GameObject TransitionOutObject;
    [SerializeField] private bool LaunchFInalScene;

    public GameObject cimetery;
    
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
        foreach (var player in AllPlayers)
        {
            if (player.GetComponent<CameraCheckerPlayer>().IsDisqualified)
            {
                player.transform.position = cimetery.transform.position;
            }
        }
        
        AllPlayers.Sort(new PlayerComparer());
        
        if (FinishedPlayers.Count + DisqualifiedPlayers.Count ==
            PlayerConfigurationManager.Instance.playerConfigs.Count && LaunchFInalScene == false)
        {
            LaunchFInalScene = true;
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

        TransitionOutObject.SetActive(true);
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

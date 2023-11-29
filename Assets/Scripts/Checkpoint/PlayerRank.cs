using UnityEngine;

public class PlayerRank : MonoBehaviour
{
    public int numPlayer = 0;
    public int currentNbLap = 1;
    public int currentCheckpointIndex = 1;
    public float distanceToNextCheckpoint = 0;

    private void Awake()
    {
        GameManager.Instance.AllPlayers.Add(this);
    }

    private void Update()
    {
        distanceToNextCheckpoint = Vector3.Distance(transform.position,
            CheckpointManager.Instance.Checkpoints[currentCheckpointIndex].transform.position);
    }
}
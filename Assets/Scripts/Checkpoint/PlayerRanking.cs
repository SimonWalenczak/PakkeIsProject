using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    public int currentCheckpointIndex = 0;
    public LayerMask checkpointLayer;
    public float distanceToNextCheckpoint;
    public int currentRank;

    [SerializeField] private List<Transform> Checkpoints;

    public int currentLap = 1;
    public int totalLaps = 2;

    [SerializeField] private float decelerationRate = 1;

    private void Start()
    {
        Checkpoints = CheckpointManager.Instance.Checkpoints;

        if (Checkpoints.Count == 0)
        {
            Debug.LogError("No checkpoints found. Make sure they have the correct layer.");
        }
    }

    private void Update()
    {
        UpdateCheckpointDistance();
        UpdatePlayerRank();
    }

    void UpdateCheckpointDistance()
    {
        if (currentCheckpointIndex < Checkpoints.Count)
        {
            distanceToNextCheckpoint =
                Vector3.Distance(transform.position, Checkpoints[currentCheckpointIndex].position);
        }
        else
        {
            distanceToNextCheckpoint = 0f;

            if (currentCheckpointIndex == Checkpoints.Count)
            {
                // Start a new lap
                StartNewLap();
            }
        }
    }

    void UpdatePlayerRank()
    {
        int rank = 1;

        foreach (PlayerRanking otherPlayer in FindObjectsOfType<PlayerRanking>())
        {
            if (otherPlayer != this)
            {
                if (otherPlayer.currentLap > currentLap)
                {
                    rank++;
                }
                else if (otherPlayer.currentLap == currentLap)
                {
                    if (otherPlayer.currentCheckpointIndex > currentCheckpointIndex)
                    {
                        rank++;
                    }
                    else if (otherPlayer.currentCheckpointIndex == currentCheckpointIndex)
                    {
                        if (otherPlayer.distanceToNextCheckpoint < distanceToNextCheckpoint)
                        {
                            rank++;
                        }
                    }
                }
            }
        }

        currentRank = rank;
    }

    public void PassCheckpoint()
    {
        if (currentCheckpointIndex == Checkpoints.Count || currentCheckpointIndex == Checkpoints.Count - 1)
        {
            currentCheckpointIndex++;
        }
        else
        {
            Debug.LogWarning("Invalid checkpoint pass. Checkpoint order violation!");
        }
    }

    void StartNewLap()
    {
        if (currentLap < totalLaps)
        {
            currentLap++;
            currentCheckpointIndex = 0;
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 newVelocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.deltaTime);

            rb.velocity = newVelocity;
            print("This player finished this run !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(checkpointLayer, other.gameObject.layer))
        {
            PassCheckpoint();
        }
    }

    private static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
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

    private Transform[] Checkpoints;
    private void Start()
    {
        Checkpoints = FindCheckpoints();

        if (Checkpoints.Length == 0)
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
        if (currentCheckpointIndex < Checkpoints.Length)
        {
            distanceToNextCheckpoint =
                Vector3.Distance(transform.position, Checkpoints[currentCheckpointIndex].position);
        }
        else
        {
            distanceToNextCheckpoint = 0f;
        }
    }

    void UpdatePlayerRank()
    {
        int rank = 1;

        foreach (PlayerRanking otherPlayer in FindObjectsOfType<PlayerRanking>())
        {
            if (otherPlayer != this)
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

        currentRank = rank;
    }

    public void PassCheckpoint()
    {
        currentCheckpointIndex++;
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

    Transform[] FindCheckpoints()
    {
        return GameObject.FindObjectsOfType<Transform>().Where(obj => obj.gameObject.layer == checkpointLayer)
            .ToArray();
    }
}